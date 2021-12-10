using CodeCharacter.Core.Data;
using CodeCharacter.Core.Entities;
using CodeCharacter.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CodeCharacter.Core.Services;

/// <inheritdoc />
public class CurrentUserService : ICurrentUserService
{
    private readonly CodeCharacterDbContext _context;

    /// <summary>
    ///     Constructor
    /// </summary>
    /// <param name="context"></param>
    public CurrentUserService(CodeCharacterDbContext context)
    {
        _context = context;
    }

    /// <inheritdoc />
    public async Task<PublicUserEntity> GetCurrentUser(UserEntity user)
    {
        var currentUser = await _context.PublicUsers.FirstAsync(x => x.UserId == user.Id);
        return currentUser;
    }

    /// <inheritdoc />
    public async Task UpdateCurrentUser(UserEntity user, PublicUserEntity newUser)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public async Task UpdatePassword(UserEntity user, string oldPassword, string newPassword)
    {
        throw new NotImplementedException();
    }
}