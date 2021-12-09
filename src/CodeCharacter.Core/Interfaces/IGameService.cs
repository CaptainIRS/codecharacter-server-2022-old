using Microsoft.AspNetCore.Mvc;

namespace CodeCharacter.Core.Interfaces;

/// <summary>
///     Service for game operations
/// </summary>
public interface IGameService
{
    /// <summary>
    ///     Get game logs by game ID
    /// </summary>
    /// <param name="gameId"></param>
    /// <returns></returns>
    Task<IActionResult> GetGameLogsByGameId(Guid gameId);
}