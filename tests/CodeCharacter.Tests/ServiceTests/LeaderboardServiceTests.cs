using System.Linq;
using System.Threading.Tasks;
using CodeCharacter.Core.Data;
using CodeCharacter.Core.Entities;
using CodeCharacter.Core.Services;
using NUnit.Framework;

namespace CodeCharacter.Tests.ServiceTests;

[TestFixture]
public class LeaderboardServiceTests : BaseServiceTests
{
    private async Task GenerateDummyData(CodeCharacterDbContext context)
    {
        for (var i = 1; i <= 100; i++)
        {
            var user = new UserEntity($"user{i}@test.com");
            await context.Users.AddAsync(user);
            await context.SaveChangesAsync();

            var userStats = new UserStatsEntity
            {
                Wins = 1,
                Losses = 1,
                Ties = 1,
                CurrentLevel = 1,
                Rating = i * 10,
                UserId = user.Id
            };
            var publicUser = new PublicUserEntity
            {
                UserId = user.Id,
                UserName = $"User{i}",
                Name = $"User{i}",
                College = "Test",
                Country = "Test",
                AvatarId = 1
            };
            await context.UserStats.AddAsync(userStats);
            await context.PublicUsers.AddAsync(publicUser);
            await context.SaveChangesAsync();
        }
    }

    [Test]
    public async Task GetLeaderboard_ReturnsListOfUsers()
    {
        await using var context = new CodeCharacterDbContext(DbContextOptions);
        await context.Database.EnsureCreatedAsync();

        await GenerateDummyData(context);

        var leaderboardService = new LeaderboardService(context);
        var leaderboardEnumerable = await leaderboardService.GetLeaderboard(null, null);
        var leaderboard = leaderboardEnumerable.ToList();

        Assert.IsNotNull(leaderboard);
        Assert.AreEqual(100, leaderboard.Count);
        Assert.AreEqual(100, leaderboard.First().Item1.UserId);
        Assert.AreEqual(1, leaderboard.Last().Item1.UserId);
    }

    [Test]
    public async Task GetLeaderboard_WithPagination_ReturnsPageOfUsers()
    {
        await using var context = new CodeCharacterDbContext(DbContextOptions);
        await context.Database.EnsureCreatedAsync();

        await GenerateDummyData(context);

        var leaderboardService = new LeaderboardService(context);
        var leaderboardEnumerable = await leaderboardService.GetLeaderboard(2, 10);
        var leaderboard = leaderboardEnumerable.ToList();

        Assert.IsNotNull(leaderboard);
        Assert.AreEqual(10, leaderboard.Count);
        Assert.AreEqual(90, leaderboard.First().Item1.UserId);
        Assert.AreEqual(81, leaderboard.Last().Item1.UserId);
    }
}