using AutoMapper;
using CodeCharacter.Core.Entities;
using CodeCharacter.Core.Interfaces;
using CodeCharacter.CoreLibrary.Controllers;
using CodeCharacter.CoreLibrary.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CodeCharacter.Core.Controllers;

/// <inheritdoc />
public class CurrentUserController : CurrentUserApiController
{
    private readonly ICurrentUserService _currentUserService;
    private readonly IMapper _mapper;
    private readonly UserManager<UserEntity> _userManager;

    /// <summary>
    ///     Constructor
    /// </summary>
    /// <param name="currentUserService"></param>
    /// <param name="mapper"></param>
    /// <param name="userManager"></param>
    public CurrentUserController(ICurrentUserService currentUserService, IMapper mapper,
        UserManager<UserEntity> userManager)
    {
        _currentUserService = currentUserService;
        _mapper = mapper;
        _userManager = userManager;
    }

    /// <inheritdoc />
    public override async Task<IActionResult> GetCurrentUser()
    {
        var user = await _userManager.GetUserAsync(HttpContext.User)!;
        var currentUser = await _currentUserService.GetCurrentUser(user);
        var currentUserDto = _mapper.Map<CurrentUserProfileDto>(currentUser);
        return Ok(currentUserDto);
    }

    /// <inheritdoc />
    public override async Task<IActionResult> UpdateCurrentUser(
        UpdateCurrentUserProfileDto updateCurrentUserProfileDto)
    {
        var user = await _userManager.GetUserAsync(HttpContext.User)!;
        var newUser = _mapper.Map<PublicUserEntity>(updateCurrentUserProfileDto);
        await _currentUserService.UpdateCurrentUser(user, newUser);
        return Ok();
    }

    /// <inheritdoc />
    public override async Task<IActionResult> UpdatePassword(UpdatePasswordRequestDto updatePasswordRequestDto)
    {
        if (updatePasswordRequestDto.Password != updatePasswordRequestDto.PasswordConfirmation)
            return BadRequest("Passwords do not match");

        var user = await _userManager.GetUserAsync(HttpContext.User)!;
        await _currentUserService.UpdatePassword(user, updatePasswordRequestDto.OldPassword,
            updatePasswordRequestDto.Password);
        return Ok();
    }
}