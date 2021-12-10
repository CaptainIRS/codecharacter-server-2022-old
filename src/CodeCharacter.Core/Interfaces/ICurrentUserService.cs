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
    Task<PublicUserEntity> GetCurrentUser(UserEntity user);

    /// <summary>
    ///     Update current user
    /// </summary>
    /// <param name="user"></param>
    /// <param name="newUser"></param>
    /// <returns></returns>
    Task UpdateCurrentUser(UserEntity user, PublicUserEntity newUser);

    /// <summary>
    ///     Update user password
    /// </summary>
    /// <param name="user"></param>
    /// <param name="oldPassword"></param>
    /// <param name="newPassword"></param>
    /// <returns></returns>
    Task UpdatePassword(UserEntity user, string oldPassword, string newPassword);
}