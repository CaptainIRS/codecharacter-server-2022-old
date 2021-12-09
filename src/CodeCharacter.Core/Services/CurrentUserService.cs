using CodeCharacter.Core.Entities;
using CodeCharacter.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CodeCharacter.Core.Services;

/// <inheritdoc />
public class CurrentUserService : ICurrentUserService
{
    /// <inheritdoc />
    public async Task<IActionResult> GetCurrentUser()
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public async Task<IActionResult> UpdateCurrentUser(PublicUserEntity newUser)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public async Task<IActionResult> UpdatePassword(string oldPassword, string newPassword)
    {
        throw new NotImplementedException();
    }
}