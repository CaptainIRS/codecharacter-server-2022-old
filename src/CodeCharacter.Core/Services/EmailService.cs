using CodeCharacter.Core.Entities;
using CodeCharacter.Core.Interfaces;

namespace CodeCharacter.Core.Services;

/// <inheritdoc />
public class EmailService : IEmailService
{
    /// <inheritdoc />
    public bool SendActivationEmail(UserEntity user, string token)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public bool SendResetPasswordEmail(UserEntity user, string token)
    {
        throw new NotImplementedException();
    }
}