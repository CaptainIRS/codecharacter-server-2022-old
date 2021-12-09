using AutoMapper;
using CodeCharacter.Core.Entities;
using CodeCharacter.Core.Interfaces;
using CodeCharacter.CoreLibrary.Controllers;
using CodeCharacter.CoreLibrary.Models;
using Microsoft.AspNetCore.Mvc;

namespace CodeCharacter.Core.Controllers;

/// <inheritdoc />
public class MapController : MapApiController
{
    private readonly IMapper _mapper;
    private readonly IMapService _mapService;

    /// <summary>
    ///     Constructor
    /// </summary>
    /// <param name="mapService"></param>
    /// <param name="mapper"></param>
    public MapController(IMapService mapService, IMapper mapper)
    {
        _mapService = mapService;
        _mapper = mapper;
    }

    /// <inheritdoc />
    public override async Task<IActionResult> CreateMapRevision(
        CreateMapRevisionRequestDto createMapRevisionRequestDto)
    {
        var mapRevision = _mapper.Map<MapRevisionEntity>(createMapRevisionRequestDto);
        return await _mapService.CreateMapRevision(mapRevision);
    }

    /// <inheritdoc />
    public override async Task<IActionResult> GetMapRevisionById(Guid revisionId)
    {
        return await _mapService.GetMapRevision(revisionId);
    }

    /// <inheritdoc />
    public override async Task<IActionResult> GetMapRevisions()
    {
        return await _mapService.GetAllMapRevisions();
    }

    /// <inheritdoc />
    public override async Task<IActionResult> GetLatestMap()
    {
        return await _mapService.GetLatestMap();
    }

    /// <inheritdoc />
    public override async Task<IActionResult> UpdateLatestMap(
        UpdateLatestMapRequestDto updateLatestMapRequestDto)
    {
        var mapRevision = _mapper.Map<MapRevisionEntity>(updateLatestMapRequestDto);
        return await _mapService.UpdateLatestMap(mapRevision);
    }
}