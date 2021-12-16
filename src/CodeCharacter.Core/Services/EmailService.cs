using System.Diagnostics.CodeAnalysis;
using CodeCharacter.Core.Entities;
using CodeCharacter.Core.Interfaces;

namespace CodeCharacter.Core.Services;

/// <inheritdoc />
public class EmailService : IEmailService
{
    /// <inheritdoc />
    [ExcludeFromCodeCoverage(Justification = "Remove once implemented")]
    public bool SendActivationEmail(UserEntity user, string token)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    [ExcludeFromCodeCoverage(Justification = "Remove once implemented")]
    public bool SendResetPasswordEmail(UserEntity user, string token)
    {
        throw new NotImplementedException();
    }
}