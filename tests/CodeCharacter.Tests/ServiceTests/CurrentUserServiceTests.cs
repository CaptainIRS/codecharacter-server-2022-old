using System.Linq;
using System.Threading.Tasks;
using CodeCharacter.Core.Data;
using CodeCharacter.Core.Entities;
using CodeCharacter.Core.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace CodeCharacter.Tests.ServiceTests;

[TestFixture]
public class CurrentUserServiceTest : BaseServiceTests
{
    private UserEntity _user = new("user@test.com");
    private const string OldPassword = "OldPassword";

    private PublicUserEntity _publicUser = new()
    {
        Name = "User",
        College = "Test",
        Country = "Test",
        AvatarId = 1
    };

    private UserStatsEntity _userStats = new()
    {
        Wins = 0,
        Losses = 0,
        Ties = 0,
        Rating = 1000,
        CurrentLevel = 1
    };

    private async Task CreateUser(CodeCharacterDbContext context)
    {
        context.Users.Add(_user);
        await context.SaveChangesAsync();
        _user.PasswordHash = new PasswordHasher<UserEntity>().HashPassword(_user, OldPassword);
        await context.SaveChangesAsync();
        _user = context.Users.First(u => u.Email == "user@test.com");

        _publicUser.UserId = _user.Id;
        _publicUser.UserName = $"Test{_user.Id}";
        context.PublicUsers.Add(_publicUser);
        await context.SaveChangesAsync();
        _publicUser = context.PublicUsers.First(u => u.UserId == _user.Id);

        _userStats.UserId = _user.Id;
        context.UserStats.Add(_userStats);
        await context.SaveChangesAsync();
        _userStats = context.UserStats.First(u => u.UserId == _user.Id);
    }

    [Test]
    public async Task GetCurrentUser_ShouldReturnCorrectUser()
    {
        await using var context = new CodeCharacterDbContext(DbContextOptions);
        await context.Database.EnsureCreatedAsync();
        await CreateUser(context);

        var currentUserService = new CurrentUserService(context);
        var (currentPublicUser, currentStatsUser) = await currentUserService.GetCurrentUser(_user);

        Assert.AreEqual(_publicUser.Name, currentPublicUser.Name);
        Assert.AreEqual(_publicUser.College, currentPublicUser.College);
        Assert.AreEqual(_publicUser.Country, currentPublicUser.Country);
        Assert.AreEqual(_publicUser.AvatarId, currentPublicUser.AvatarId);
        Assert.AreEqual(_publicUser.UserId, currentPublicUser.UserId);
        Assert.AreEqual(_userStats.Wins, currentStatsUser.Wins);
        Assert.AreEqual(_userStats.Losses, currentStatsUser.Losses);
        Assert.AreEqual(_userStats.Ties, currentStatsUser.Ties);
        Assert.AreEqual(_userStats.Rating, currentStatsUser.Rating);
        Assert.AreEqual(_userStats.CurrentLevel, currentStatsUser.CurrentLevel);
        Assert.AreEqual(_userStats.UserId, currentStatsUser.UserId);
    }

    [Test]
    public async Task UpdateCurrentUser_ShouldUpdateUser()
    {
        await using var context = new CodeCharacterDbContext(DbContextOptions);
        await context.Database.EnsureCreatedAsync();
        await CreateUser(context);

        const string newCollege = "NewCollege";

        var currentUserService = new CurrentUserService(context);
        await currentUserService.UpdateCurrentUser(_user, null, newCollege, null, null);

        var updatedUser = await context.PublicUsers.FirstAsync(u => u.UserId == _user.Id);

        Assert.AreEqual(_publicUser.Name, updatedUser.Name);
        Assert.AreEqual(newCollege, updatedUser.College);
        Assert.AreEqual(_publicUser.Country, updatedUser.Country);
        Assert.AreEqual(_publicUser.AvatarId, updatedUser.AvatarId);
        Assert.AreEqual(_publicUser.UserId, updatedUser.UserId);
    }
}