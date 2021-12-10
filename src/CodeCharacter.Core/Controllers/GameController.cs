using CodeCharacter.Core.Interfaces;
using CodeCharacter.CoreLibrary.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace CodeCharacter.Core.Controllers;

/// <inheritdoc />
public class GameController : GameApiController
{
    private readonly IGameService _gameService;

    /// <summary>
    ///     Constructor
    /// </summary>
    /// <param name="gameService"></param>
    public GameController(IGameService gameService)
    {
        _gameService = gameService;
    }

    /// <inheritdoc />
    public override async Task<IActionResult> GetGameLogsByGameId(Guid gameId)
    {
        var gameLogs = await _gameService.GetGameLogsByGameId(gameId);
        return Ok(gameLogs);
    }
}