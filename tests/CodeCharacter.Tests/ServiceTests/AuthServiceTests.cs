using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using CodeCharacter.Core.Data;
using CodeCharacter.Core.Entities;
using CodeCharacter.Core.Exceptions;
using CodeCharacter.Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Moq;
using NUnit.Framework;

namespace CodeCharacter.Tests.ServiceTests;

[TestFixture]
public class AuthServiceTests : BaseServiceTests
{
    private Mock<IConfiguration> _configuration = null!;
    private Mock<UserManager<UserEntity>> _userManager = null!;
    private Mock<SignInManager<UserEntity>> _signInManager = null!;
    private Mock<HttpContextAccessor> _httpContextAccessor = null!;
    private Mock<PasswordHasher<UserEntity>> _passwordHasher = null!;

    private AuthService CreateAuthServiceInstance(CodeCharacterDbContext context)
    {
        _configuration = new Mock<IConfiguration>();
        Mock<IUserStore<UserEntity>> userStore = new();
        _passwordHasher = new Mock<PasswordHasher<UserEntity>>(null);
        _userManager = new Mock<UserManager<UserEntity>>(userStore.Object, null, _passwordHasher.Object, null, null,
            null, null, null,
            null);
        _httpContextAccessor = new Mock<HttpContextAccessor>();
        Mock<IUserClaimsPrincipalFactory<UserEntity>> userClaimsPrincipalFactory = new();
        _signInManager = new Mock<SignInManager<UserEntity>>(_userManager.Object, _httpContextAccessor.Object,
            userClaimsPrincipalFactory.Object, null, null, null, null);
        return new AuthService(_configuration.Object, context, _userManager.Object, _signInManager.Object);
    }

    [Test]
    public async Task Login_WithExistingUser_ShouldSucceed()
    {
        await using var context = new CodeCharacterDbContext(DbContextOptions);
        await context.Database.EnsureCreatedAsync();

        var authService = CreateAuthServiceInstance(context);

        _userManager.Setup(x => x.FindByEmailAsync(It.IsAny<string>()))
            .ReturnsAsync(new UserEntity(TestConstants.Email));
        _passwordHasher.Setup(x =>
                x.VerifyHashedPassword(It.IsAny<UserEntity>(), It.IsAny<string>(), It.IsAny<string>()))
            .Returns(PasswordVerificationResult.Success);
        _signInManager.Setup(x =>
                x.SignInWithClaimsAsync(It.IsAny<UserEntity>(), It.IsAny<bool>(), It.IsAny<List<Claim>>()))
            .Returns(Task.CompletedTask);

        Assert.DoesNotThrowAsync(async () => await authService.Login(TestConstants.Email, "Password"));
    }

    [Test]
    public async Task Login_WithNonExistingUser_ShouldThrow()
    {
        await using var context = new CodeCharacterDbContext(DbContextOptions);
        await context.Database.EnsureCreatedAsync();

        var authService = CreateAuthServiceInstance(context);

        _userManager.Setup(x => x.FindByEmailAsync(It.IsAny<string>()))!.ReturnsAsync((UserEntity?)null);

        var exception =
            Assert.ThrowsAsync<GenericException>(async () => await authService.Login(TestConstants.Email, "Password"));
        Assert.That(exception?.Message, Is.EqualTo("Invalid email or password"));
    }

    [Test]
    public async Task Login_WithWrongPassword_ShouldThrow()
    {
        await using var context = new CodeCharacterDbContext(DbContextOptions);
        await context.Database.EnsureCreatedAsync();

        var authService = CreateAuthServiceInstance(context);

        _userManager.Setup(x => x.FindByEmailAsync(It.IsAny<string>()))!.ReturnsAsync(
            new UserEntity(TestConstants.Email));
        _passwordHasher.Setup(x =>
                x.VerifyHashedPassword(It.IsAny<UserEntity>(), It.IsAny<string>(), It.IsAny<string>()))
            .Returns(PasswordVerificationResult.Failed);

        var exception =
            Assert.ThrowsAsync<GenericException>(async () => await authService.Login(TestConstants.Email, "Password"));
        Assert.That(exception?.Message, Is.EqualTo("Invalid email or password"));
    }
}