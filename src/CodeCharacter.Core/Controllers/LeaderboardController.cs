using AutoMapper;
using CodeCharacter.Core.Interfaces;
using CodeCharacter.CoreLibrary.Controllers;
using CodeCharacter.CoreLibrary.Models;
using Microsoft.AspNetCore.Mvc;

namespace CodeCharacter.Core.Controllers;

/// <inheritdoc />
public class LeaderboardController : LeaderboardApiController
{
    private readonly ILeaderboardService _leaderboardService;
    private readonly IMapper _mapper;

    /// <summary>
    ///     Constructor
    /// </summary>
    /// <param name="leaderboardService"></param>
    /// <param name="mapper"></param>
    public LeaderboardController(ILeaderboardService leaderboardService, IMapper mapper)
    {
        _leaderboardService = leaderboardService;
        _mapper = mapper;
    }

    /// <inheritdoc />
    public override async Task<IActionResult> GetLeaderboard(int? page, int? size)
    {
        var userStats = await _leaderboardService.GetLeaderboard(page, size);
        var leaderboard = _mapper.Map<IEnumerable<LeaderboardEntryDto>>(userStats);
        return Ok(leaderboard);
    }
}