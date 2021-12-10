using CodeCharacter.Core.Entities;
using CodeCharacter.Core.Interfaces;
using CodeCharacter.CoreLibrary.Controllers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CodeCharacter.Core.Controllers;

/// <inheritdoc />
public class MatchController : MatchApiController
{
    private readonly IMatchService _matchService;
    private readonly UserManager<UserEntity> _userManager;

    /// <summary>
    ///     Constructor
    /// </summary>
    /// <param name="matchService"></param>
    /// <param name="userManager"></param>
    public MatchController(IMatchService matchService, UserManager<UserEntity> userManager)
    {
        _matchService = matchService;
        _userManager = userManager;
    }

    /// <inheritdoc />
    public override async Task<IActionResult> GetTopMatches()
    {
        var matches = await _matchService.GetTopMatches();
        return Ok(matches);
    }

    /// <inheritdoc />
    public override async Task<IActionResult> GetUserMatches()
    {
        var user = await _userManager.GetUserAsync(HttpContext.User)!;
        var matches = await _matchService.GetUserMatches(user);
        return Ok(matches);
    }
}