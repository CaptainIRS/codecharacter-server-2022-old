using CodeCharacter.Core.Data;
using CodeCharacter.Core.Entities;
using CodeCharacter.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CodeCharacter.Core.Services;

/// <inheritdoc />
public class MapService : IMapService
{
    private readonly CodeCharacterDbContext _context;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="context"></param>
    public MapService(CodeCharacterDbContext context)
    {
        _context = context;
    }
    /// <inheritdoc />
    public async Task CreateMapRevision(UserEntity user, string map, Guid? parentRevision)
    {
        MapRevisionEntity? parentMapRevision = null;
        if (parentRevision != null)
        {
            parentMapRevision = await _context.MapRevisions.FindAsync(parentRevision);
            if (parentMapRevision == null)
            {
                throw new Exception("Parent revision not found");
            }
        }
        await _context.MapRevisions.AddAsync(new MapRevisionEntity
        {
            Map = map,
            User = user,
            ParentRevision = parentMapRevision
        });
        await _context.SaveChangesAsync();
    }

    /// <inheritdoc />
    public async Task<MapRevisionEntity> GetMapRevision(UserEntity user, Guid revisionId)
    {
        var mapRevision = await _context.MapRevisions.FindAsync(revisionId);
        if (mapRevision == null || mapRevision.User.Id != user.Id)
        {
            throw new Exception("Map revision not found");
        }
        return mapRevision;
    }

    /// <inheritdoc />
    public async Task<List<MapRevisionEntity>> GetAllMapRevisions(UserEntity user)
    {
        return await _context.MapRevisions.Where(x => x.User.Id == user.Id).ToListAsync();
    }

    /// <inheritdoc />
    public async Task<MapEntity> GetLatestMap(UserEntity user)
    {
        var latestMap = await _context.Maps.FirstAsync(x => x.UserId == user.Id);
        return latestMap;
    }

    /// <inheritdoc />
    public async Task UpdateLatestMap(UserEntity user, string map)
    {
        var latestMap = await _context.Maps.FirstAsync(x => x.UserId == user.Id);
        latestMap.Map = map;
        await _context.SaveChangesAsync();
    }
}