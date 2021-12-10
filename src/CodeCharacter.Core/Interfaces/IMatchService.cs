using CodeCharacter.Core.Entities;

namespace CodeCharacter.Core.Interfaces;

/// <summary>
///     Service for match operations
/// </summary>
public interface IMatchService
{
    /// <summary>
    ///     Get top matches
    /// </summary>
    /// <returns></returns>
    Task<IEnumerable<MatchEntity>> GetTopMatches();

    /// <summary>
    ///     Get matches for user
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    Task<IEnumerable<MatchEntity>> GetUserMatches(UserEntity user);
}