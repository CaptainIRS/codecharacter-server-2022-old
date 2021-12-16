using CodeCharacter.Core.Data;
using CodeCharacter.Core.Entities;
using CodeCharacter.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CodeCharacter.Core.Services;

/// <inheritdoc />
public class LeaderboardService : ILeaderboardService
{
    private readonly CodeCharacterDbContext _context;

    /// <summary>
    ///     Constructor
    /// </summary>
    /// <param name="context"></param>
    public LeaderboardService(CodeCharacterDbContext context)
    {
        _context = context;
    }

    /// <inheritdoc />
    public async Task<IEnumerable<(PublicUserEntity, UserStatsEntity)>> GetLeaderboard(int? page, int? size)
    {
        var userStatsJoin = await _context.UserStats
            .Join(
                _context.PublicUsers,
                userStats => userStats.UserId,
                publicUser => publicUser.UserId,
                (userStats, publicUser) => new { userStats.Rating, User = publicUser, Stats = userStats }
            ).ToListAsync();
        var leaderboard = userStatsJoin
            .OrderByDescending(u => u.Rating)
            .Select(u => (u.User, u.Stats));
        if (page != null && size != null)
            return leaderboard
                .Skip((page.Value - 1) * size.Value)
                .Take(size.Value);

        return leaderboard;
    }
}