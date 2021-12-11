using CodeCharacter.Core.Entities;

namespace CodeCharacter.Core.Interfaces;

/// <summary>
///     Service for leaderboard operations
/// </summary>
public interface ILeaderboardService
{
    /// <summary>
    ///     Get leaderboard
    /// </summary>
    /// <param name="page"></param>
    /// <param name="size"></param>
    /// <returns></returns>
    Task<IEnumerable<(PublicUserEntity, UserStatsEntity)>> GetLeaderboard(int? page, int? size);
}