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
    Task Login(string email, string password);

    /// <summary>
    ///     Forgot Password
    /// </summary>
    /// <param name="email">Email of the user</param>
    Task ForgotPassword(string email);

    /// <summary>
    ///     Reset Password
    /// </summary>
    /// <param name="email">Email of the user</param>
    /// <param name="password">Password of the user</param>
    /// <param name="token">Token received from email</param>
    Task ResetPassword(string email, string password, string token);
}