using Microsoft.AspNetCore.Mvc;

namespace CodeCharacter.Core.Interfaces;

/// <summary>
///     Service for leaderboard operations
/// </summary>
public interface ILeaderboardService
{
    /// <summary>
    ///     Get leaderboard
    /// </summary>
    /// <returns></returns>
    Task<IActionResult> GetLeaderboard();
}