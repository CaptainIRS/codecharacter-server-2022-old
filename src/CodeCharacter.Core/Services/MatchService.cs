using CodeCharacter.Core.Data;
using CodeCharacter.Core.Entities;
using CodeCharacter.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CodeCharacter.Core.Services;

/// <inheritdoc />
public class MatchService : IMatchService
{
    private readonly CodeCharacterDbContext _context;

    /// <summary>
    ///     Constructor
    /// </summary>
    /// <param name="context"></param>
    public MatchService(CodeCharacterDbContext context)
    {
        _context = context;
    }

    /// <inheritdoc />
    public async Task<IEnumerable<MatchEntity>> GetTopMatches()
    {
        return await _context.Matches
            .OrderByDescending(m => m.Games.Sum(g => g.Points1 + g.Points2))
            .ToListAsync();
    }

    /// <inheritdoc />
    public async Task<IEnumerable<MatchEntity>> GetUserMatches(UserEntity user)
    {
        return await _context.Matches
            .Where(m => m.User1.Id == user.Id || m.User2.Id == user.Id)
            .ToListAsync();
    }
}