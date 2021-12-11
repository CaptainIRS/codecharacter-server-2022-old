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
    public async Task<IEnumerable<(PublicUserEntity, PublicUserEntity, MatchEntity)>> GetTopMatches()
    {
        var matchObjects = await _context.Matches
            .OrderByDescending(m => m.Games.Sum(g => g.Points1 + g.Points2))
            .Take(10)
            .Join(
                _context.PublicUsers,
                match => match.User1.Id,
                publicUser => publicUser.UserId,
                (match, publicUser) => new { Match = match, User1 = publicUser }
            )
            .Join(
                _context.PublicUsers,
                combined => combined.Match.User2.Id,
                publicUser => publicUser.UserId,
                (combined, publicUser) => new { combined.Match, combined.User1, User2 = publicUser }
            )
            .ToListAsync();
        return matchObjects.Select(matchObject => (matchObject.User1, matchObject.User2, matchObject.Match));
    }

    /// <inheritdoc />
    public async Task<IEnumerable<(PublicUserEntity, PublicUserEntity, MatchEntity)>> GetUserMatches(UserEntity user)
    {
        var matchObjects = await _context.Matches
            .Where(m => m.User1.Id == user.Id || m.User2.Id == user.Id)
            .OrderByDescending(m => m.CreatedAt)
            .Join(
                _context.PublicUsers,
                match => match.User1.Id,
                publicUser => publicUser.UserId,
                (match, publicUser) => new { Match = match, User1 = publicUser }
            )
            .Join(
                _context.PublicUsers,
                combined => combined.Match.User2.Id,
                publicUser => publicUser.UserId,
                (combined, publicUser) => new { combined.Match, combined.User1, User2 = publicUser }
            )
            .ToListAsync();
        return matchObjects.Select(matchObject => (matchObject.User1, matchObject.User2, matchObject.Match));
    }
}