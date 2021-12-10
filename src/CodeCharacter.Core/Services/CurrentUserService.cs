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
    public async Task UpdateCurrentUser(UserEntity user, string? name, string? college, string? country, int? avatarId)
    {
        var currentUser = await _context.PublicUsers.FirstAsync(u => u.UserId == user.Id);
        currentUser.Name = name ?? currentUser.Name;
        currentUser.College = college ?? currentUser.College;
        currentUser.Country = country ?? currentUser.Country;
        currentUser.AvatarId = avatarId ?? currentUser.AvatarId;
        await _context.SaveChangesAsync();
    }
}