using CodeCharacter.Core.Interfaces;
using CodeCharacter.CoreLibrary.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace CodeCharacter.Core.Controllers;

/// <inheritdoc />
public class MatchController : MatchApiController
{
    private readonly IMatchService _matchService;

    /// <summary>
    ///     Constructor
    /// </summary>
    /// <param name="matchService"></param>
    public MatchController(IMatchService matchService)
    {
        _matchService = matchService;
    }

    /// <inheritdoc />
    public override async Task<IActionResult> GetTopMatches()
    {
        return await _matchService.GetTopMatches();
    }

    /// <inheritdoc />
    public override async Task<IActionResult> GetUserMatches()
    {
        return await _matchService.GetUserMatches();
    }
}