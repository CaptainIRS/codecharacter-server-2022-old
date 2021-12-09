using CodeCharacter.Core.Entities;
using Microsoft.AspNetCore.Mvc;

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
    Task<IActionResult> ActivateUser(int userId, string token);

    /// <summary>
    ///     Get Rating History
    /// </summary>
    /// <param name="userId">ID of the user</param>
    /// <returns></returns>
    Task<IActionResult> GetRatingHistory(int userId);

    /// <summary>
    ///     Register User
    /// </summary>
    /// <param name="user">User entity</param>
    /// <param name="publicUser">Public user entity</param>
    /// <param name="password">Password string</param>
    /// <returns>Task</returns>
    Task<IActionResult> Register(UserEntity user, PublicUserEntity publicUser, string password);
}