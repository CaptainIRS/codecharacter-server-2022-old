using CodeCharacter.Core.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CodeCharacter.Core.Interfaces;

/// <summary>
///     Service for CRUD operations on map and map revision
/// </summary>
public interface IMapService
{
    /// <summary>
    ///     Create map revision for user
    /// </summary>
    /// <param name="user"></param>
    /// <param name="map"></param>
    /// <param name="parentRevision"></param>
    /// <returns></returns>
    Task CreateMapRevision(UserEntity user, string map, Guid? parentRevision);

    /// <summary>
    ///     Get map revision by ID for user
    /// </summary>
    /// <param name="user"></param>
    /// <param name="revisionId"></param>
    /// <returns></returns>
    Task<MapRevisionEntity> GetMapRevision(UserEntity user, Guid revisionId);

    /// <summary>
    ///     Get all map revisions for user
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    Task<List<MapRevisionEntity>> GetAllMapRevisions(UserEntity user);

    /// <summary>
    ///     Get latest map for user
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    Task<MapEntity> GetLatestMap(UserEntity user);

    /// <summary>
    ///     Update latest map for user
    /// </summary>
    /// <param name="user"></param>
    /// <param name="map"></param>
    /// <returns></returns>
    Task UpdateLatestMap(UserEntity user, string map);
}