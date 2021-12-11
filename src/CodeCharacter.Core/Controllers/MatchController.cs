using AutoMapper;
using CodeCharacter.Core.Entities;
using CodeCharacter.Core.Interfaces;
using CodeCharacter.CoreLibrary.Controllers;
using CodeCharacter.CoreLibrary.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CodeCharacter.Core.Controllers;

/// <inheritdoc />
public class MatchController : MatchApiController
{
    private readonly IMapper _mapper;
    private readonly IMatchService _matchService;
    private readonly UserManager<UserEntity> _userManager;

    /// <summary>
    ///     Constructor
    /// </summary>
    /// <param name="matchService"></param>
    /// <param name="mapper"></param>
    /// <param name="userManager"></param>
    public MatchController(IMatchService matchService, IMapper mapper, UserManager<UserEntity> userManager)
    {
        _matchService = matchService;
        _mapper = mapper;
        _userManager = userManager;
    }

    /// <inheritdoc />
    public override async Task<IActionResult> GetTopMatches()
    {
        var matchEntities = await _matchService.GetTopMatches();
        var matchDtos = _mapper.Map<IEnumerable<MatchDto>>(matchEntities);
        return Ok(matchDtos);
    }

    /// <inheritdoc />
    public override async Task<IActionResult> GetUserMatches()
    {
        var user = await _userManager.GetUserAsync(HttpContext.User)!;
        var matchEntities = await _matchService.GetUserMatches(user);
        var matchDtos = _mapper.Map<IEnumerable<MatchDto>>(matchEntities);
        return Ok(matchDtos);
    }
}