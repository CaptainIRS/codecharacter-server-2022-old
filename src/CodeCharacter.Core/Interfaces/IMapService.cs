using CodeCharacter.Core.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CodeCharacter.Core.Interfaces;

/// <summary>
///     Service for CRUD operations on the map table
/// </summary>
public interface IMapService
{
    /// <summary>
    ///     Create map revision for user
    /// </summary>
    /// <param name="mapRevision"></param>
    /// <returns></returns>
    Task<IActionResult> CreateMapRevision(MapRevisionEntity mapRevision);

    /// <summary>
    ///     Get map revision by ID for user
    /// </summary>
    /// <param name="revisionId"></param>
    /// <returns></returns>
    Task<IActionResult> GetMapRevision(Guid revisionId);

    /// <summary>
    ///     Get all map revisions for user
    /// </summary>
    /// <returns></returns>
    Task<IActionResult> GetAllMapRevisions();

    /// <summary>
    ///     Get latest map for user
    /// </summary>
    /// <returns></returns>
    Task<IActionResult> GetLatestMap();

    /// <summary>
    ///     Update latest map for user
    /// </summary>
    /// <param name="mapRevision"></param>
    /// <returns></returns>
    Task<IActionResult> UpdateLatestMap(MapRevisionEntity mapRevision);
}