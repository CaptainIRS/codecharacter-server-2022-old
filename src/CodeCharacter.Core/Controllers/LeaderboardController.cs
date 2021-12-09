using CodeCharacter.Core.Interfaces;
using CodeCharacter.CoreLibrary.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace CodeCharacter.Core.Controllers;

/// <inheritdoc />
public class LeaderboardController : LeaderboardApiController
{
    private readonly ILeaderboardService _leaderboardService;

    /// <summary>
    ///     Constructor
    /// </summary>
    /// <param name="leaderboardService"></param>
    public LeaderboardController(ILeaderboardService leaderboardService)
    {
        _leaderboardService = leaderboardService;
    }

    /// <inheritdoc />
    public override async Task<IActionResult> GetLeaderboard(string? page, string? size)
    {
        return await _leaderboardService.GetLeaderboard();
    }
}