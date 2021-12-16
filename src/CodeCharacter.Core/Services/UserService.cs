using System.Diagnostics.CodeAnalysis;
using CodeCharacter.Core.Data;
using CodeCharacter.Core.Entities;
using CodeCharacter.Core.Exceptions;
using CodeCharacter.Core.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CodeCharacter.Core.Services;

/// <inheritdoc />
public class UserService : IUserService
{
    private readonly IConfiguration _config;
    private readonly CodeCharacterDbContext _dbContext;
    private readonly SignInManager<UserEntity> _signInManager;
    private readonly UserManager<UserEntity> _userManager;

    /// <summary>
    ///     Constructor
    /// </summary>
    /// <param name="config"></param>
    /// <param name="dbContext"></param>
    /// <param name="userManager"></param>
    /// <param name="signInManager"></param>
    public UserService(IConfiguration config, CodeCharacterDbContext dbContext,
        UserManager<UserEntity> userManager,
        SignInManager<UserEntity> signInManager)
    {
        _config = config;
        _dbContext = dbContext;
        _userManager = userManager;
        _signInManager = signInManager;
    }

    /// <inheritdoc />
    [ExcludeFromCodeCoverage(Justification = "Remove once implemented")]
    public Task ActivateUser(int userId, string token)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public async Task<IEnumerable<RatingHistoryEntity>> GetRatingHistory(int userId)
    {
        return await _dbContext.RatingHistories.Where(x => x.UserId == userId).ToListAsync();
    }

    /// <inheritdoc />
    public async Task Register(UserEntity user, PublicUserEntity publicUser, string password)
    {
        var result = await _userManager.CreateAsync(user, password);
        if (!result.Succeeded)
        {
            var errors = string.Join(", ", result.Errors.Select(x => x.Description));
            throw new GenericException(errors);
        }

        publicUser.UserId = user.Id;
        _dbContext.PublicUsers.Add(publicUser);
        _dbContext.UserStats.Add(new UserStatsEntity(user.Id));
        _dbContext.RatingHistories.Add(new RatingHistoryEntity(user.Id));
        await _dbContext.SaveChangesAsync();
    }
}