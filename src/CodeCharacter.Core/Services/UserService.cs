using CodeCharacter.Core.Data;
using CodeCharacter.Core.Entities;
using CodeCharacter.Core.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CodeCharacter.Core.Services;

/// <inheritdoc />
public class UserService : IUserService
{
    private readonly IConfiguration _config;
    private readonly CodeCharacterDbContext _dbContext;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly SignInManager<UserEntity> _signInManager;
    private readonly UserManager<UserEntity> _userManager;

    /// <summary>
    ///     Constructor
    /// </summary>
    /// <param name="config"></param>
    /// <param name="dbContext"></param>
    /// <param name="httpContextAccessor"></param>
    /// <param name="userManager"></param>
    /// <param name="signInManager"></param>
    public UserService(IConfiguration config, CodeCharacterDbContext dbContext,
        IHttpContextAccessor httpContextAccessor, UserManager<UserEntity> userManager,
        SignInManager<UserEntity> signInManager)
    {
        _config = config;
        _dbContext = dbContext;
        _httpContextAccessor = httpContextAccessor;
        _userManager = userManager;
        _signInManager = signInManager;
    }

    public async Task<IActionResult> ActivateUser(int userId, string token)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public async Task<IActionResult> GetRatingHistory(int userId)
    {
        var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == userId);
        if (user == null) return new NotFoundResult();

        var ratingHistories = _dbContext.RatingHistories.Where(x => x.UserId == userId);
        return new OkObjectResult(ratingHistories);
    }

    /// <inheritdoc />
    public async Task<IActionResult> Register(UserEntity user, PublicUserEntity publicUser, string password)
    {
        var result = await _userManager.CreateAsync(user, password);
        if (!result.Succeeded)
        {
            var errors = result.Errors.Aggregate("", (current, error) => current + error.Description + " ");
            return new BadRequestObjectResult(errors);
        }

        publicUser.UserId = user.Id;
        _dbContext.PublicUsers.Add(publicUser);
        _dbContext.UserStats.Add(new UserStatsEntity(user.Id));
        _dbContext.RatingHistories.Add(new RatingHistoryEntity(user.Id));
        await _dbContext.SaveChangesAsync();
        return new OkResult();
    }
}