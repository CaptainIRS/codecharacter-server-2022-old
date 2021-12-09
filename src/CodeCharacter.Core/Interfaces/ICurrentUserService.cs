using CodeCharacter.Core.Entities;
using Microsoft.AspNetCore.Mvc;

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
    Task<IActionResult> GetCurrentUser();

    /// <summary>
    ///     Update current user
    /// </summary>
    /// <param name="newUser"></param>
    /// <returns></returns>
    Task<IActionResult> UpdateCurrentUser(PublicUserEntity newUser);

    /// <summary>
    ///     Update user password
    /// </summary>
    /// <param name="oldPassword"></param>
    /// <param name="newPassword"></param>
    /// <returns></returns>
    Task<IActionResult> UpdatePassword(string oldPassword, string newPassword);
}