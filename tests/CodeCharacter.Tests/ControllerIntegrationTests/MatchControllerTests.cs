using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Threading.Tasks;
using CodeCharacter.Core.Entities;
using CodeCharacter.Core.Interfaces;
using CodeCharacter.CoreLibrary.Models;
using NodaTime;
using NUnit.Framework;

namespace CodeCharacter.Tests.ControllerIntegrationTests;

public class FakeMatchService : IMatchService
{
    private readonly List<(PublicUserEntity, PublicUserEntity, MatchEntity)> _matches = new();
    private readonly UserEntity _otherUser = new("otherUser@test.com");
    private readonly UserEntity _user = new("user@test.com");

    public FakeMatchService()
    {
        var game = new GameEntity
        {
            Id = Guid.NewGuid(),
            Map = "0001",
            GameVerdict = GameDto.GameVerdictEnum.PLAYER1,
            Status = GameDto.StatusEnum.EXECUTED,
            Points1 = 20,
            Points2 = 10
        };
        _matches.Add((new PublicUserEntity
        {
            UserId = 1,
            Name = "User1",
            College = "Test",
            Country = "Test",
            AvatarId = 1
        }, new PublicUserEntity
        {
            UserId = 2,
            Name = "User2",
            College = "Test",
            Country = "Test",
            AvatarId = 1
        }, new MatchEntity
        {
            Id = Guid.NewGuid(),
            User1 = _user,
            User2 = _otherUser,
            CreatedAt = Instant.FromUtc(2020, 1, 1, 0, 0, 0),
            MatchMode = MatchDto.MatchModeEnum.AUTO,
            MatchVerdict = MatchDto.MatchVerdictEnum.PLAYER1,
            Games = new List<GameEntity> { game }
        }));
    }

    public Task<IEnumerable<(PublicUserEntity, PublicUserEntity, MatchEntity)>> GetTopMatches()
    {
        return Task.FromResult(_matches.AsEnumerable());
    }

    public Task<IEnumerable<(PublicUserEntity, PublicUserEntity, MatchEntity)>> GetUserMatches(UserEntity user)
    {
        return Task.FromResult(_matches.AsEnumerable());
    }
}

[TestFixture]
public class MatchControllerTests : BaseControllerTests
{
    [Test]
    public async Task GetTopMatches_ReturnsTopMatches()
    {
        var client = GetClientForService<IMatchService, FakeMatchService>(true);

        var response = await client.GetFromJsonAsync<List<MatchDto>>("/top-matches");

        Assert.That(response, Is.Not.Null);
        Assert.AreEqual(1, response?.Count);
    }

    [Test]
    public async Task GetUserMatches_ReturnsUserMatches()
    {
        var client = GetClientForService<IMatchService, FakeMatchService>(true);

        var response = await client.GetFromJsonAsync<List<MatchDto>>("/user/matches");

        Assert.That(response, Is.Not.Null);
        Assert.AreEqual(1, response?.Count);
    }
}