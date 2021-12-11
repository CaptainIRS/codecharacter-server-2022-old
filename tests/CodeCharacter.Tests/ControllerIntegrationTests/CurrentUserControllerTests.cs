using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CodeCharacter.Core.Controllers;
using CodeCharacter.Core.Entities;
using CodeCharacter.Core.Interfaces;
using CodeCharacter.Core.Mappers;
using CodeCharacter.CoreLibrary.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;

namespace CodeCharacter.Tests.ControllerIntegrationTests;

public class FakeCurrentUserService : ICurrentUserService
{
    private readonly PublicUserEntity _publicUser = new()
    {
        Name = "User",
        College = "Test",
        Country = "Test",
        AvatarId = 1
    };

    private readonly UserStatsEntity _userStats = new()
    {
        Wins = 0,
        Losses = 0,
        Ties = 0,
        Rating = 1000,
        CurrentLevel = 1
    };

    private UserEntity _user = new("user@test.com");

    public Task<(PublicUserEntity, UserStatsEntity)> GetCurrentUser(UserEntity user)
    {
        return Task.FromResult((_publicUser, _userStats));
    }

    public Task UpdateCurrentUser(UserEntity user, string? name, string? college, string? country, int? avatarId)
    {
        return Task.CompletedTask;
    }
}

[TestFixture]
public class CurrentUserControllerTests : BaseControllerTests
{
    [Test]
    public async Task GetCurrentUser_ReturnsUserProfile()
    {
        var client = GetClientForService<ICurrentUserService, FakeCurrentUserService>(true);
        var response = await client.GetAsync("/user");
        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        var currentUserProfile = await response.Content.ReadFromJsonAsync<CurrentUserProfileDto>();
        Assert.That(currentUserProfile, Is.Not.Null);
    }

    [Test]
    public async Task UpdateCurrentUser_UpdatesUserProfile()
    {
        var client = GetClientForService<ICurrentUserService, FakeCurrentUserService>(true);
        var response = await client.PatchAsync("/user", new StringContent(new UpdateCurrentUserProfileDto
        {
            Name = "New Name",
            College = "New College",
            Country = "New Country",
            AvatarId = 2
        }.ToJson(), Encoding.UTF8, "application/json"));
        Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);
    }

    [Test]
    public async Task UpdatePassword_UpdatesUserPassword()
    {
        // TODO: Investigate ways to make this an end-to-end integration test

        Mock<UserEntity> user = new(TestConstants.Email);
        Mock<IUserStore<UserEntity>> userStore = new();
        Mock<UserManager<UserEntity>> userManager = new(userStore.Object, null, null, null, null, null, null, null,
            null);
        userManager.Setup(um => um.GetUserAsync(It.IsAny<ClaimsPrincipal?>())).ReturnsAsync(user.Object);
        userManager.Setup(um => um.ChangePasswordAsync(user.Object, It.IsAny<string>(), It.IsAny<string>()))
            .ReturnsAsync(IdentityResult.Success);
        var controller = new CurrentUserController(new FakeCurrentUserService(),
            new Mapper(new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>())), userManager.Object);

        var response = await controller.UpdatePassword(new UpdatePasswordRequestDto
        {
            OldPassword = "Passw0rd!",
            Password = "Passw0rd!!",
            PasswordConfirmation = "Passw0rd!!"
        });

        var result = response as NoContentResult;
        Assert.That(result, Is.Not.Null);
    }

    [Test]
    public async Task UpdatePassword_WithInvalidData_ReturnsBadRequest()
    {
        // TODO: Investigate ways to make this an end-to-end integration test

        Mock<UserEntity> user = new(TestConstants.Email);
        Mock<IUserStore<UserEntity>> userStore = new();
        Mock<UserManager<UserEntity>> userManager = new(userStore.Object, null, null, null, null, null, null, null,
            null);
        userManager.Setup(um => um.GetUserAsync(It.IsAny<ClaimsPrincipal?>())).ReturnsAsync(user.Object);
        userManager.Setup(um => um.ChangePasswordAsync(user.Object, It.IsAny<string>(), It.IsAny<string>()))
            .ReturnsAsync(
                IdentityResult.Failed(new IdentityError { Code = "TestCode", Description = "TestDescription" }));
        var controller = new CurrentUserController(new FakeCurrentUserService(),
            new Mapper(new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>())), userManager.Object);

        var response = await controller.UpdatePassword(new UpdatePasswordRequestDto
        {
            OldPassword = "Passw0rd!",
            Password = "Passw0rd!!",
            PasswordConfirmation = "Passw0rd!!"
        });

        var result = response as BadRequestObjectResult;
        Assert.That(result, Is.Not.Null);

        var value = result?.Value;
        Assert.That(value, Is.TypeOf<GenericErrorDto>());

        var error = value as GenericErrorDto;
        Assert.That(error?.Message, Is.Not.Null);
        Assert.That(error?.Message, Is.EqualTo("TestDescription"));
    }

    [Test]
    public async Task UpdatePassword_WithNonMatchingPassword_ReturnsBadRequest()
    {
        // TODO: Investigate ways to make this an end-to-end integration test

        Mock<UserEntity> user = new(TestConstants.Email);
        Mock<IUserStore<UserEntity>> userStore = new();
        Mock<UserManager<UserEntity>> userManager = new(userStore.Object, null, null, null, null, null, null, null,
            null);
        userManager.Setup(um => um.GetUserAsync(It.IsAny<ClaimsPrincipal?>())).ReturnsAsync(user.Object);
        var controller = new CurrentUserController(new FakeCurrentUserService(),
            new Mapper(new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>())), userManager.Object);

        var response = await controller.UpdatePassword(new UpdatePasswordRequestDto
        {
            OldPassword = "Passw0rd!",
            Password = "Passw0rd!!",
            PasswordConfirmation = "Passw0rd!"
        });

        var result = response as BadRequestObjectResult;
        Assert.That(result, Is.Not.Null);

        var value = result?.Value;
        Assert.That(value, Is.TypeOf<GenericErrorDto>());

        var error = value as GenericErrorDto;
        Assert.That(error?.Message, Is.EqualTo("Passwords do not match"));
    }
}