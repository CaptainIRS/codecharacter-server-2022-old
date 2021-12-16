using System;
using System.Linq;
using System.Threading.Tasks;
using CodeCharacter.Core.Data;
using CodeCharacter.Core.Entities;
using CodeCharacter.Core.Exceptions;
using CodeCharacter.Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Moq;
using NodaTime;
using NUnit.Framework;

namespace CodeCharacter.Tests.ServiceTests;

[TestFixture]
public class UserServiceTests : BaseServiceTests
{
    private Mock<UserManager<UserEntity>> _userManager = null!;
    private Mock<SignInManager<UserEntity>> _signInManager = null!;

    private UserService CreateUserServiceInstance(CodeCharacterDbContext context)
    {
        var config = new Mock<IConfiguration>();
        Mock<IUserStore<UserEntity>> userStore = new();
        _userManager = new Mock<UserManager<UserEntity>>(userStore.Object, null, null, null, null, null, null, null,
            null);
        Mock<HttpContextAccessor> httpContextAccessor = new();
        Mock<IUserClaimsPrincipalFactory<UserEntity>> userClaimsPrincipalFactory = new();
        _signInManager = new Mock<SignInManager<UserEntity>>(_userManager.Object, httpContextAccessor.Object,
            userClaimsPrincipalFactory.Object, null, null, null, null);
        return new UserService(config.Object, context, _userManager.Object, _signInManager.Object);
    }

    [Test]
    public async Task RegisterUser_ShouldRegisterUser()
    {
        await using var context = new CodeCharacterDbContext(DbContextOptions);
        await context.Database.EnsureCreatedAsync();

        Assert.AreEqual(0, context.Users.Count());
        Assert.AreEqual(0, context.PublicUsers.Count());
        Assert.AreEqual(0, context.UserStats.Count());
        Assert.AreEqual(0, context.RatingHistories.Count());

        var user = new UserEntity("user@test.com");
        await context.Users.AddAsync(user);
        await context.SaveChangesAsync();

        var userService = CreateUserServiceInstance(context);
        var publicUser = new PublicUserEntity
        {
            UserName = "Test",
            Name = "Test",
            College = "Test",
            Country = "Test",
            AvatarId = 1
        };
        const string password = "Password";

        _userManager.Setup(x => x.CreateAsync(It.IsAny<UserEntity>(), It.IsAny<string>()))
            .ReturnsAsync(IdentityResult.Success);
        await userService.Register(user, publicUser, password);

        Assert.AreEqual(1, context.Users.Count());
        Assert.AreEqual(1, context.PublicUsers.Count());
        Assert.AreEqual(1, context.UserStats.Count());
        Assert.AreEqual(1, context.RatingHistories.Count());

        var userStats = await context.UserStats.FirstAsync();
        Assert.AreEqual(0, userStats.Wins);
        Assert.AreEqual(0, userStats.Losses);
        Assert.AreEqual(0, userStats.Ties);
        Assert.AreEqual(0, userStats.Rating);
        Assert.AreEqual(1, userStats.CurrentLevel);

        var ratingHistory = await context.RatingHistories.FirstAsync();
        Assert.AreEqual(0, ratingHistory.Rating);
        Assert.AreEqual(0, ratingHistory.RatingDeviation);
        // greater than 5 minutes ago
        Assert.IsTrue(ratingHistory.ValidFrom >
                      Instant.FromUnixTimeSeconds(DateTimeOffset.UtcNow.ToUnixTimeSeconds() - 5 * 60));
    }

    [Test]
    public async Task RegisterUser_WithFailedRegistration_ShouldThrowException()
    {
        await using var context = new CodeCharacterDbContext(DbContextOptions);
        await context.Database.EnsureCreatedAsync();

        Assert.AreEqual(0, context.Users.Count());
        Assert.AreEqual(0, context.PublicUsers.Count());
        Assert.AreEqual(0, context.UserStats.Count());
        Assert.AreEqual(0, context.RatingHistories.Count());

        var user = new UserEntity("user@test.com");
        await context.Users.AddAsync(user);
        await context.SaveChangesAsync();

        var userService = CreateUserServiceInstance(context);
        var publicUser = new PublicUserEntity
        {
            UserName = "Test",
            Name = "Test",
            College = "Test",
            Country = "Test",
            AvatarId = 1
        };
        const string password = "Password";
        const string failureDescription = "TestDescription";

        _userManager.Setup(x => x.CreateAsync(It.IsAny<UserEntity>(), It.IsAny<string>())).ReturnsAsync(
            IdentityResult.Failed(new IdentityError { Code = "TestCode", Description = failureDescription }));

        var exception =
            Assert.ThrowsAsync<GenericException>(async () => await userService.Register(user, publicUser, password));
        Assert.AreEqual(failureDescription, exception?.Message);

        Assert.AreEqual(0, context.PublicUsers.Count());
        Assert.AreEqual(0, context.UserStats.Count());
        Assert.AreEqual(0, context.RatingHistories.Count());
    }

    [Test]
    public async Task GetRatingHistory_ShouldReturnRatingHistory()
    {
        await using var context = new CodeCharacterDbContext(DbContextOptions);
        await context.Database.EnsureCreatedAsync();

        var user = new UserEntity("user@test.com");
        await context.Users.AddAsync(user);
        await context.SaveChangesAsync();

        await context.RatingHistories.AddAsync(new RatingHistoryEntity(user.Id));
        await context.RatingHistories.AddAsync(new RatingHistoryEntity(user.Id));
        await context.RatingHistories.AddAsync(new RatingHistoryEntity(user.Id));
        await context.RatingHistories.AddAsync(new RatingHistoryEntity(user.Id));
        await context.SaveChangesAsync();

        var userService = CreateUserServiceInstance(context);
        var ratingHistoryEnumerable = await userService.GetRatingHistory(user.Id);
        var ratingHistory = ratingHistoryEnumerable.ToList();
        Assert.AreEqual(4, ratingHistory.Count);
        Assert.AreEqual(0, ratingHistory[0].Rating);
        Assert.AreEqual(0, ratingHistory[0].RatingDeviation);
        Assert.AreEqual(1, ratingHistory[0].Id);
        Assert.AreEqual(1, ratingHistory[0].UserId);
    }
}