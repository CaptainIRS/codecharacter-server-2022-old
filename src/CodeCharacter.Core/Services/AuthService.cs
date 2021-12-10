using System.Security.Claims;
using CodeCharacter.Core.Data;
using CodeCharacter.Core.Entities;
using CodeCharacter.Core.Exceptions;
using CodeCharacter.Core.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CodeCharacter.Core.Services;

/// <inheritdoc />
public class AuthService : IAuthService
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
    public AuthService(IConfiguration config, CodeCharacterDbContext dbContext,
        IHttpContextAccessor httpContextAccessor, UserManager<UserEntity> userManager,
        SignInManager<UserEntity> signInManager)
    {
        _config = config;
        _dbContext = dbContext;
        _httpContextAccessor = httpContextAccessor;
        _userManager = userManager;
        _signInManager = signInManager;
    }

    /// <inheritdoc />
    public async Task<IActionResult> Login(string email, string password)
    {
        var identityUser = await _userManager.FindByEmailAsync(email);

        if (identityUser == null) throw new GenericException("Invalid email or password");

        var result =
            _userManager.PasswordHasher.VerifyHashedPassword(identityUser, identityUser.PasswordHash,
                password);
        if (result == PasswordVerificationResult.Failed) throw new GenericException("Invalid username or password");
        var claims = new List<Claim>
        {
            new(ClaimTypes.Email, identityUser.Email),
            new(ClaimTypes.Name, identityUser.UserName)
        };
        await _signInManager.SignInWithClaimsAsync(identityUser, true, claims);
        return new OkResult();
    }

    /// <inheritdoc />
    public Task<IActionResult> ForgotPassword(string email)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public Task<IActionResult> ResetPassword(string email, string password, string token)
    {
        throw new NotImplementedException();
    }
}