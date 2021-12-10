using AutoMapper;
using CodeCharacter.Core.Entities;
using CodeCharacter.Core.Interfaces;
using CodeCharacter.CoreLibrary.Controllers;
using CodeCharacter.CoreLibrary.Models;
using Microsoft.AspNetCore.Mvc;

namespace CodeCharacter.Core.Controllers;

/// <inheritdoc />
public class UserController : UserApiController
{
    private readonly IMapper _mapper;
    private readonly IUserService _userService;

    /// <inheritdoc />
    public UserController(IMapper mapper, IUserService userService)
    {
        _mapper = mapper;
        _userService = userService;
    }

    /// <inheritdoc />
    public override async Task<IActionResult> ActivateUser(int userId,
        ActivateUserRequestDto activateUserRequestDto)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public override async Task<IActionResult> GetRatingHistory(int userId)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public override async Task<IActionResult> Register(RegisterUserRequestDto registerUserRequestDto)
    {
        if (registerUserRequestDto.Password != registerUserRequestDto.PasswordConfirmation)
            return BadRequest("Passwords do not match");
        var user = new UserEntity(registerUserRequestDto.Username, registerUserRequestDto.Email);
        var publicUser = _mapper.Map<PublicUserEntity>(registerUserRequestDto);
        await _userService.Register(user, publicUser, registerUserRequestDto.Password);
        return Ok();
    }
}