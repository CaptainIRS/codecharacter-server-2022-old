using CodeCharacter.Core.Entities;
using CodeCharacter.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CodeCharacter.Core.Services;

/// <inheritdoc />
public class MapService : IMapService
{
    /// <inheritdoc />
    public async Task<IActionResult> CreateMapRevision(MapRevisionEntity mapRevision)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public async Task<IActionResult> GetMapRevision(Guid revisionId)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public async Task<IActionResult> GetAllMapRevisions()
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public async Task<IActionResult> GetLatestMap()
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public async Task<IActionResult> UpdateLatestMap(MapRevisionEntity mapRevision)
    {
        throw new NotImplementedException();
    }
}