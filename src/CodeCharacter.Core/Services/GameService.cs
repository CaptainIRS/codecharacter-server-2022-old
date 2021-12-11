using CodeCharacter.Core.Data;
using CodeCharacter.Core.Exceptions;
using CodeCharacter.Core.Interfaces;

namespace CodeCharacter.Core.Services;

/// <inheritdoc />
public class GameService : IGameService
{
    private readonly CodeCharacterDbContext _context;

    /// <summary>
    ///     Constructor
    /// </summary>
    /// <param name="context"></param>
    public GameService(CodeCharacterDbContext context)
    {
        _context = context;
    }

    /// <inheritdoc />
    public async Task<string> GetGameLogByGameId(Guid gameId)
    {
        var game = await _context.Games.FindAsync(gameId);
        if (game == null) throw new GenericException("Game not found");

        var gameLog = await _context.GameLogs.FindAsync(game.Id);
        if (gameLog == null) throw new GenericException("Game log not found");

        return gameLog.GameLog;
    }
}