using System;
using System.Net;
using System.Threading.Tasks;
using CodeCharacter.Core.Interfaces;
using NUnit.Framework;

namespace CodeCharacter.Tests.ControllerIntegrationTests;

public class FakeGameService : IGameService
{
    public Task<string> GetGameLogByGameId(Guid gameId)
    {
        return Task.FromResult("1\n2\n3\n4");
    }
}

[TestFixture]
public class GameControllerTests : BaseControllerTests
{
    [Test]
    public async Task GetGameLogByGameId_ReturnsGameLog()
    {
        var client = GetClientForService<IGameService, FakeGameService>(true);

        var response = await client.GetAsync("/games/123e4567-e89b-12d3-a456-426614174000/logs");
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));

        var responseString = await response.Content.ReadAsStringAsync();
        Assert.That(responseString, Is.EqualTo("1\n2\n3\n4"));
    }
}