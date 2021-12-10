using AutoMapper;
using CodeCharacter.Core.Entities;
using CodeCharacter.Core.Exceptions;
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
        try
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            await _mapService.CreateMapRevision(user, createMapRevisionRequestDto.Map, null);
            return Created("", null);
        }
        catch (GenericException e)
        {
            var error = _mapper.Map<GenericErrorDto>(e);
            return BadRequest(error);
        }
    }

    /// <inheritdoc />
    public override async Task<IActionResult> GetMapRevisionById(Guid revisionId)
    {
        try
        {
            var user = await _userManager.GetUserAsync(HttpContext.User)!;
            var mapRevisionEntity = await _mapService.GetMapRevision(user, revisionId);
            var mapRevisionDto = _mapper.Map<MapRevisionDto>(mapRevisionEntity);
            return Ok(mapRevisionDto);
        }
        catch (GenericException e)
        {
            var error = _mapper.Map<GenericErrorDto>(e);
            return NotFound(error);
        }
    }

    /// <inheritdoc />
    public override async Task<IActionResult> GetMapRevisions()
    {
        var user = await _userManager.GetUserAsync(HttpContext.User)!;
        var mapRevisions = await _mapService.GetAllMapRevisions(user);
        var mapRevisionsDto = _mapper.Map<IEnumerable<MapRevisionDto>>(mapRevisions);
        return Ok(mapRevisionsDto);
    }

    /// <inheritdoc />
    public override async Task<IActionResult> GetLatestMap()
    {
        var user = await _userManager.GetUserAsync(HttpContext.User)!;
        var mapEntity = await _mapService.GetLatestMap(user);
        var mapDto = _mapper.Map<MapDto>(mapEntity);
        return Ok(mapDto);
    }

    /// <inheritdoc />
    public override async Task<IActionResult> UpdateLatestMap(
        UpdateLatestMapRequestDto updateLatestMapRequestDto)
    {
        var user = await _userManager.GetUserAsync(HttpContext.User)!;
        await _mapService.UpdateLatestMap(user, updateLatestMapRequestDto.Map);
        return NoContent();
    }
}