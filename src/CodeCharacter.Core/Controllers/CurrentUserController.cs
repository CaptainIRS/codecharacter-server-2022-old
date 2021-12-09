using AutoMapper;
using CodeCharacter.Core.Entities;
using CodeCharacter.Core.Interfaces;
using CodeCharacter.CoreLibrary.Controllers;
using CodeCharacter.CoreLibrary.Models;
using Microsoft.AspNetCore.Mvc;

namespace CodeCharacter.Core.Controllers;

/// <inheritdoc />
public class CurrentUserController : CurrentUserApiController
{
    private readonly ICurrentUserService _currentUserService;
    private readonly IMapper _mapper;

    /// <summary>
    ///     Constructor
    /// </summary>
    /// <param name="currentUserService"></param>
    /// <param name="mapper"></param>
    public CurrentUserController(ICurrentUserService currentUserService, IMapper mapper)
    {
        _currentUserService = currentUserService;
        _mapper = mapper;
    }

    /// <inheritdoc />
    public override async Task<IActionResult> GetCurrentUser()
    {
        var currentUser = await _currentUserService.GetCurrentUser();
        var currentUserDto = _mapper.Map<CurrentUserProfileDto>(currentUser);
        return Ok(currentUserDto);
    }

    /// <inheritdoc />
    public override async Task<IActionResult> UpdateCurrentUser(
        UpdateCurrentUserProfileDto updateCurrentUserProfileDto)
    {
        var newUser = _mapper.Map<PublicUserEntity>(updateCurrentUserProfileDto);
        return await _currentUserService.UpdateCurrentUser(newUser);
    }

    /// <inheritdoc />
    public override async Task<IActionResult> UpdatePassword(UpdatePasswordRequestDto updatePasswordRequestDto)
    {
        if (updatePasswordRequestDto.Password != updatePasswordRequestDto.PasswordConfirmation)
            return BadRequest("Passwords do not match");

        return await _currentUserService.UpdatePassword(updatePasswordRequestDto.OldPassword,
            updatePasswordRequestDto.Password);
    }
}