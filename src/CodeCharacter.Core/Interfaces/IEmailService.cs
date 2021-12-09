using CodeCharacter.Core.Entities;

namespace CodeCharacter.Core.Interfaces;

/// <summary>
///     Service for sending emails
/// </summary>
public interface IEmailService
{
    /// <summary>
    ///     Send activation email for password login
    /// </summary>
    /// <param name="user">User entity</param>
    /// <param name="token">Token</param>
    /// <returns>bool is successful</returns>
    bool SendActivationEmail(UserEntity user, string token);

    /// <summary>
    ///     Send email for resetting password
    /// </summary>
    /// <param name="user">User entity</param>
    /// <param name="token">Token</param>
    /// <returns>bool is successful</returns>
    bool SendResetPasswordEmail(UserEntity user, string token);
}