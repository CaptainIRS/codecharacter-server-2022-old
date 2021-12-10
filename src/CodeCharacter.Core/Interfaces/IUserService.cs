using CodeCharacter.Core.Entities;

namespace CodeCharacter.Core.Interfaces;

/// <summary>
///     Service for user related operations
/// </summary>
public interface IUserService
{
    /// <summary>
    ///     Activate User
    /// </summary>
    /// <param name="userId">ID of the user</param>
    /// <param name="token">Activation token</param>
    /// <returns></returns>
    Task ActivateUser(int userId, string token);

    /// <summary>
    ///     Get Rating History
    /// </summary>
    /// <param name="userId">ID of the user</param>
    /// <returns></returns>
    Task<IEnumerable<RatingHistoryEntity>> GetRatingHistory(int userId);

    /// <summary>
    ///     Register User
    /// </summary>
    /// <param name="user">User entity</param>
    /// <param name="publicUser">Public user entity</param>
    /// <param name="password">Password string</param>
    /// <returns>Task</returns>
    Task Register(UserEntity user, PublicUserEntity publicUser, string password);
}