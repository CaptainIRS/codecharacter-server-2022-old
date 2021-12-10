using AutoMapper;
using CodeCharacter.Core.Entities;
using CodeCharacter.Core.Interfaces;
using CodeCharacter.CoreLibrary.Controllers;
using CodeCharacter.CoreLibrary.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CodeCharacter.Core.Controllers;

/// <inheritdoc />
public class MapController : MapApiController
{
    private readonly IMapper _mapper;
    private readonly IMapService _mapService;
    private readonly UserManager<UserEntity> _userManager;

    /// <summary>
    ///     Constructor
    /// </summary>
    /// <param name="mapService"></param>
    /// <param name="userManager"></param>
    /// <param name="mapper"></param>
    public MapController(IMapService mapService, UserManager<UserEntity> userManager, IMapper mapper)
    {
        _mapService = mapService;
        _userManager = userManager;
        _mapper = mapper;
    }

    /// <inheritdoc />
    public override async Task<IActionResult> CreateMapRevision(
        CreateMapRevisionRequestDto createMapRevisionRequestDto)
    {
        var user = await _userManager.GetUserAsync(HttpContext.User)!;
        await _mapService.CreateMapRevision(
            user,
            createMapRevisionRequestDto.Map,
            null);
        return Ok();
    }

    /// <inheritdoc />
    public override async Task<IActionResult> GetMapRevisionById(Guid revisionId)
    {
        var user = await _userManager.GetUserAsync(HttpContext.User)!;
        await _mapService.GetMapRevision(user, revisionId);
        return Ok();
    }

    /// <inheritdoc />
    public override async Task<IActionResult> GetMapRevisions()
    {
        var user = await _userManager.GetUserAsync(HttpContext.User)!;
        await _mapService.GetAllMapRevisions(user);
        return Ok();
    }

    /// <inheritdoc />
    public override async Task<IActionResult> GetLatestMap()
    {
        var user = await _userManager.GetUserAsync(HttpContext.User)!;
        await _mapService.GetLatestMap(user);
        return Ok();
    }

    /// <inheritdoc />
    public override async Task<IActionResult> UpdateLatestMap(
        UpdateLatestMapRequestDto updateLatestMapRequestDto)
    {
        var user = await _userManager.GetUserAsync(HttpContext.User)!;

        await _mapService.UpdateLatestMap(
            user,
            updateLatestMapRequestDto.Map);

        return Ok();
    }
}