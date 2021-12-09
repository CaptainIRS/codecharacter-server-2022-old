using Microsoft.AspNetCore.Mvc;

namespace CodeCharacter.Core.Interfaces;

/// <summary>
///     Service for authentication operations
/// </summary>
public interface IAuthService
{
    /// <summary>
    ///     Login User
    /// </summary>
    /// <param name="email">Email of the user</param>
    /// <param name="password">Password of the user</param>
    /// <returns>Task</returns>
    Task<IActionResult> Login(string email, string password);

    /// <summary>
    ///     Forgot Password
    /// </summary>
    /// <param name="email">Email of the user</param>
    /// <returns>Task</returns>
    Task<IActionResult> ForgotPassword(string email);

    /// <summary>
    ///     Reset Password
    /// </summary>
    /// <param name="email">Email of the user</param>
    /// <param name="password">Password of the user</param>
    /// <param name="token">Token received from email</param>
    /// <returns></returns>
    Task<IActionResult> ResetPassword(string email, string password, string token);
}