using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodeCharacter.Core.Data;
using CodeCharacter.Core.Entities;
using CodeCharacter.Core.Services;
using CodeCharacter.CoreLibrary.Models;
using NodaTime;
using NUnit.Framework;

namespace CodeCharacter.Tests.ServiceTests;

[TestFixture]
public class MatchServiceTests : BaseServiceTests
{
    private UserEntity _user = new("user", "user@test.com");
    private UserEntity _otherUser = new("otherUser", "otherUser@test.com");

    private async Task CreateUser(CodeCharacterDbContext context)
    {
        context.Users.Add(_user);
        context.Users.Add(_otherUser);
        await context.SaveChangesAsync();
        _user = context.Users.First(u => u.UserName == "user");
        _otherUser = context.Users.First(u => u.UserName == "otherUser");
    }

    private async Task AddGamesAndMatches(CodeCharacterDbContext context)
    {
        var game1 = new GameEntity
        {
            Map = "0001",
            GameVerdict = GameDto.GameVerdictEnum.PLAYER1,
            Status = GameDto.StatusEnum.EXECUTED,
            Points1 = 20,
            Points2 = 10
        };
        var game2 = new GameEntity
        {
            Map = "0002",
            GameVerdict = GameDto.GameVerdictEnum.PLAYER2,
            Status = GameDto.StatusEnum.EXECUTED,
            Points1 = 30,
            Points2 = 40
        };
        var game3 = new GameEntity
        {
            Map = "0003",
            GameVerdict = GameDto.GameVerdictEnum.PLAYER1,
            Status = GameDto.StatusEnum.EXECUTED,
            Points1 = 50,
            Points2 = 60
        };

        await context.Games.AddAsync(game1);
        await context.Games.AddAsync(game2);
        await context.Games.AddAsync(game3);
        await context.SaveChangesAsync();

        game1 = context.Games.First(g => g.Map == "0001");
        game2 = context.Games.First(g => g.Map == "0002");
        game3 = context.Games.First(g => g.Map == "0003");

        var match1 = new MatchEntity
        {
            User1 = _user,
            User2 = _otherUser,
            CreatedAt = Instant.FromUtc(2020, 1, 1, 0, 0, 0),
            MatchMode = MatchDto.MatchModeEnum.AUTO,
            MatchVerdict = MatchDto.MatchVerdictEnum.PLAYER1,
            Games = new List<GameEntity> { game1 }
        };
        var match2 = new MatchEntity
        {
            User1 = _otherUser,
            User2 = _user,
            CreatedAt = Instant.FromUtc(2020, 1, 1, 0, 0, 0),
            MatchMode = MatchDto.MatchModeEnum.AUTO,
            MatchVerdict = MatchDto.MatchVerdictEnum.PLAYER2,
            Games = new List<GameEntity>
            {
                game2,
                game2
            }
        };
        var match3 = new MatchEntity
        {
            User1 = _otherUser,
            User2 = _otherUser,
            CreatedAt = Instant.FromUtc(2020, 1, 1, 0, 0, 0),
            MatchMode = MatchDto.MatchModeEnum.AUTO,
            MatchVerdict = MatchDto.MatchVerdictEnum.PLAYER1,
            Games = new List<GameEntity>
            {
                game3,
                game3
            }
        };

        await context.Matches.AddAsync(match1);
        await context.Matches.AddAsync(match2);
        await context.Matches.AddAsync(match3);
        await context.SaveChangesAsync();
    }

    [Test]
    public async Task GetTopMatches_ShouldReturnTopMatches()
    {
        await using var context = new CodeCharacterDbContext(DbContextOptions);
        await context.Database.EnsureCreatedAsync();

        await AddGamesAndMatches(context);

        var service = new MatchService(context);
        var matches = await service.GetTopMatches();

        var matchEntities = matches.ToList();
        Assert.AreEqual(3, matchEntities.Count);
        Assert.True(matchEntities[0].Games[0].Points1 + matchEntities[0].Games[0].Points2 == 110);
        Assert.True(matchEntities[1].Games[0].Points1 + matchEntities[1].Games[0].Points2 == 70);
        Assert.True(matchEntities[2].Games[0].Points1 + matchEntities[2].Games[0].Points2 == 30);

        Assert.True(matchEntities[0].Games[0].Map == "0003");
        Assert.True(matchEntities[1].Games[0].Map == "0002");
        Assert.True(matchEntities[2].Games[0].Map == "0001");

        Assert.True(matchEntities[0].Games[0].GameVerdict == GameDto.GameVerdictEnum.PLAYER1);
        Assert.True(matchEntities[1].Games[0].GameVerdict == GameDto.GameVerdictEnum.PLAYER2);
        Assert.True(matchEntities[2].Games[0].GameVerdict == GameDto.GameVerdictEnum.PLAYER1);

        Assert.True(matchEntities[0].Games[0].Status == GameDto.StatusEnum.EXECUTED);
        Assert.True(matchEntities[1].Games[0].Status == GameDto.StatusEnum.EXECUTED);
        Assert.True(matchEntities[2].Games[0].Status == GameDto.StatusEnum.EXECUTED);
    }

    [Test]
    public async Task GetUserMatches_ShouldReturnUserMatches()
    {
        await using var context = new CodeCharacterDbContext(DbContextOptions);
        await context.Database.EnsureCreatedAsync();

        await AddGamesAndMatches(context);

        var service = new MatchService(context);
        var matches = await service.GetUserMatches(_user);

        Assert.True(matches.Count() == 2);
    }
}