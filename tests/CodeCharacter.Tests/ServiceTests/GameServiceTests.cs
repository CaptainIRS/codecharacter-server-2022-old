using System;
using System.Threading.Tasks;
using CodeCharacter.Core.Data;
using CodeCharacter.Core.Entities;
using CodeCharacter.Core.Exceptions;
using CodeCharacter.Core.Services;
using CodeCharacter.CoreLibrary.Models;
using NUnit.Framework;

namespace CodeCharacter.Tests.ServiceTests;

[TestFixture]
public class GameServiceTest : BaseServiceTests
{
    [Test]
    public async Task GetGameLogs_ShouldReturnGameLogs()
    {
        await using var context = new CodeCharacterDbContext(DbContextOptions);
        await context.Database.EnsureCreatedAsync();

        var game = new GameEntity
        {
            Map = "0000\n0000\n0000\n0000",
            GameVerdict = GameDto.GameVerdictEnum.PLAYER1,
            Status = GameDto.StatusEnum.EXECUTED,
            Points1 = 200,
            Points2 = 100
        };
        await context.Games.AddAsync(game);
        await context.SaveChangesAsync();
        const string gameLogString = "LogLogLog";
        var gameLog = new GameLogEntity
        {
            GameId = game.Id,
            GameLog = gameLogString
        };
        await context.GameLogs.AddAsync(gameLog);
        await context.SaveChangesAsync();

        var gameService = new GameService(context);
        var gameLogs = await gameService.GetGameLogsByGameId(game.Id);

        Assert.IsNotNull(gameLogs);
        Assert.AreEqual(gameLogs, gameLogString);
    }

    [Test]
    public async Task GetGameLogs_WithNonExistentGameId_ThrowsException()
    {
        await using var context = new CodeCharacterDbContext(DbContextOptions);
        await context.Database.EnsureCreatedAsync();

        var gameService = new GameService(context);

        var exception =
            Assert.ThrowsAsync<GenericException>(async () => await gameService.GetGameLogsByGameId(Guid.NewGuid()));
        Assert.That(exception, Is.Not.Null);
        Assert.AreEqual(exception?.Message, "Game not found");
    }

    [Test]
    public async Task GetGameLogs_WithNonExistentGameLog_ThrowsException()
    {
        await using var context = new CodeCharacterDbContext(DbContextOptions);
        await context.Database.EnsureCreatedAsync();

        var game = new GameEntity
        {
            Map = "0000\n0000\n0000\n0000",
            GameVerdict = GameDto.GameVerdictEnum.PLAYER1,
            Status = GameDto.StatusEnum.EXECUTED,
            Points1 = 200,
            Points2 = 100
        };
        await context.Games.AddAsync(game);
        await context.SaveChangesAsync();

        var gameService = new GameService(context);
        var exception =
            Assert.ThrowsAsync<GenericException>(async () => await gameService.GetGameLogsByGameId(game.Id));

        Assert.That(exception, Is.Not.Null);
        Assert.AreEqual(exception?.Message, "Game log not found");
    }
}