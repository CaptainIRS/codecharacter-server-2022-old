using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Threading.Tasks;
using CodeCharacter.Core.Entities;
using CodeCharacter.Core.Interfaces;
using CodeCharacter.CoreLibrary.Models;
using NUnit.Framework;

namespace CodeCharacter.Tests.ControllerIntegrationTests;

public class FakeLeaderboardService : ILeaderboardService
{
    private readonly List<(PublicUserEntity, UserStatsEntity)> _leaderboardEntries = new();

    public FakeLeaderboardService()
    {
        for (var i = 1; i <= 10; i++)
            _leaderboardEntries.Add((new PublicUserEntity
            {
                UserId = i,
                Name = $"User{i}",
                College = "Test",
                Country = "Test",
                AvatarId = 1
            }, new UserStatsEntity
            {
                Wins = 1,
                Losses = 1,
                Ties = 1,
                CurrentLevel = 1,
                Rating = i * 10
            }));
    }

    public Task<IEnumerable<(PublicUserEntity, UserStatsEntity)>> GetLeaderboard(int? page, int? size)
    {
        return Task.FromResult(_leaderboardEntries.AsEnumerable());
    }
}

[TestFixture]
public class LeaderboardControllerTests : BaseControllerTests
{
    [Test]
    public async Task GetLeaderboard_ReturnsLeaderboard()
    {
        var client = GetClientForService<ILeaderboardService, FakeLeaderboardService>(true);

        var response = await client.GetFromJsonAsync<List<LeaderboardEntryDto>>("/leaderboard?page=1&size=10");

        Assert.That(response, Is.Not.Null);
        Assert.AreEqual(10, response?.Count);
    }
}