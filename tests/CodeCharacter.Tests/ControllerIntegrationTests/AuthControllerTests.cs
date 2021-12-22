using System;
using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;
using CodeCharacter.Core.Exceptions;
using CodeCharacter.Core.Interfaces;
using CodeCharacter.CoreLibrary.Models;
using NUnit.Framework;

namespace CodeCharacter.Tests.ControllerIntegrationTests;

public class FakeAuthService : IAuthService
{
    public Task Login(string email, string password)
    {
        if (password.Contains("invalid")) throw new GenericException("Invalid username or password");
        return Task.CompletedTask;
    }

    public Task ForgotPassword(string email)
    {
        throw new NotImplementedException();
    }

    public Task ResetPassword(string email, string password, string token)
    {
        throw new NotImplementedException();
    }
}

[TestFixture]
public class AuthControllerTests : BaseControllerTests
{
    [Test]
    public async Task Login_WithCorrectCredentials_ShouldReturnOk()
    {
        var client = GetClientForService<IAuthService, FakeAuthService>();

        var response = await client.PostAsJsonAsync("/auth/login/password",
            new PasswordLoginRequestDto { Email = TestConstants.Email, Password = "Passw0rd!" });

        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
    }

    [Test]
    public async Task Login_WithIncorrectCredentials_ShouldReturnUnauthorized()
    {
        var client = GetClientForService<IAuthService, FakeAuthService>();

        var response = await client.PostAsJsonAsync("/auth/login/password",
            new PasswordLoginRequestDto { Email = TestConstants.Email, Password = "Passw0rd!invalid" });

        Assert.AreEqual(HttpStatusCode.Unauthorized, response.StatusCode);
    }
}