using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;
using CodeCharacter.Core.Entities;
using CodeCharacter.Core.Exceptions;
using CodeCharacter.Core.Interfaces;
using CodeCharacter.CoreLibrary.Models;
using NodaTime;
using NUnit.Framework;

namespace CodeCharacter.Tests.ControllerIntegrationTests;

public class FakeUserService : IUserService
{
    public Task ActivateUser(int userId, string token)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<RatingHistoryEntity>> GetRatingHistory(int userId)
    {
        return Task.FromResult(new List<RatingHistoryEntity>
        {
            new()
            {
                Id = 1,
                UserId = 1,
                Rating = 1,
                RatingDeviation = 1,
                ValidFrom = Instant.FromUtc(2020, 1, 1, 0, 0)
            },
            new()
            {
                Id = 2,
                UserId = 1,
                Rating = 1,
                RatingDeviation = 1,
                ValidFrom = Instant.FromUtc(2020, 1, 1, 0, 0)
            }
        }.AsEnumerable());
    }

    public Task Register(UserEntity user, PublicUserEntity publicUser, string password)
    {
        if (password.Contains("pass")) return Task.CompletedTask;

        throw new GenericException("TestException");
    }
}

[TestFixture]
public class UserControllerTests : BaseControllerTests
{
    [Test]
    public async Task RegisterUser_ShouldRegisterUser()
    {
        var client = GetClientForService<IUserService, FakeUserService>();

        var response = await client.PostAsJsonAsync("/users", new RegisterUserRequestDto
        {
            Username = "Test",
            Name = "Test",
            AvatarId = 1,
            College = "Test",
            Country = "Test",
            Email = "user@test.com",
            Password = "Passw0rd!pass",
            PasswordConfirmation = "Passw0rd!pass"
        });

        Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
    }

    [Test]
    public async Task RegisterUser_WithFailedRegistration_ShouldThrowException()
    {
        var client = GetClientForService<IUserService, FakeUserService>();

        var response = await client.PostAsJsonAsync("/users", new RegisterUserRequestDto
        {
            Username = "Test",
            Name = "Test",
            AvatarId = 1,
            College = "Test",
            Country = "Test",
            Email = "user@test.com",
            Password = "Passw0rd!fail",
            PasswordConfirmation = "Passw0rd!fail"
        });

        Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Test]
    public async Task RegisterUser_WithNonMatchingPasswords_ShouldThrowException()
    {
        var client = GetClientForService<IUserService, FakeUserService>();

        var response = await client.PostAsJsonAsync("/users", new RegisterUserRequestDto
        {
            Username = "Test",
            Name = "Test",
            AvatarId = 1,
            College = "Test",
            Country = "Test",
            Email = "user@test.com",
            Password = "Passw0rd!pass",
            PasswordConfirmation = "Passw0rd!fail"
        });

        Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        var exception = await response.Content.ReadFromJsonAsync<GenericErrorDto>();
        Assert.That(exception, Is.Not.Null);
        Assert.AreEqual("Passwords do not match", exception?.Message);
    }

    [Test]
    public async Task GetRatingHistory_ShouldReturnRatingHistory()
    {
        var client = GetClientForService<IUserService, FakeUserService>(true);

        var response = await client.GetAsync("/users/1/ratingHistory");

        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        var ratingHistory = await response.Content.ReadFromJsonAsync<List<RatingHistoryDto>>();
        Assert.That(ratingHistory, Is.Not.Null);
        Assert.AreEqual(2, ratingHistory?.Count);
    }
}