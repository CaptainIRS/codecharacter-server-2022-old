using CodeCharacter.Core.Entities;

namespace CodeCharacter.Core.Interfaces;

/// <summary>
///     Service for current user operations
/// </summary>
public interface ICurrentUserService
{
    /// <summary>
    ///     Get current user
    /// </summary>
    /// <returns></returns>
    Task<(PublicUserEntity, UserStatsEntity)> GetCurrentUser(UserEntity user);

    /// <summary>
    ///     Update current user
    /// </summary>
    /// <param name="user"></param>
    /// <param name="name"></param>
    /// <param name="college"></param>
    /// <param name="country"></param>
    /// <param name="avatarId"></param>
    /// <returns></returns>
    Task UpdateCurrentUser(UserEntity user, string? name, string? college, string? country, int? avatarId);
}