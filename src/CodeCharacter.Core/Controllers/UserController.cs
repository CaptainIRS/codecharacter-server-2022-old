using System.Diagnostics.CodeAnalysis;
using AutoMapper;
using CodeCharacter.Core.Entities;
using CodeCharacter.Core.Exceptions;
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
    [ExcludeFromCodeCoverage(Justification = "Remove once implemented")]
    public override Task<IActionResult> ActivateUser(int userId,
        ActivateUserRequestDto activateUserRequestDto)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public override async Task<IActionResult> GetRatingHistory(int userId)
    {
        var ratingHistories = await _userService.GetRatingHistory(userId);
        var ratingHistoryDtos = _mapper.Map<IEnumerable<RatingHistoryDto>>(ratingHistories);
        return Ok(ratingHistoryDtos);
    }

    /// <inheritdoc />
    public override async Task<IActionResult> Register(RegisterUserRequestDto registerUserRequestDto)
    {
        if (registerUserRequestDto.Password != registerUserRequestDto.PasswordConfirmation)
            return BadRequest(new GenericErrorDto { Message = "Passwords do not match" });
        var user = new UserEntity(registerUserRequestDto.Email);
        var publicUser = _mapper.Map<PublicUserEntity>(registerUserRequestDto);
        try
        {
            await _userService.Register(user, publicUser, registerUserRequestDto.Password);
        }
        catch (GenericException e)
        {
            var error = _mapper.Map<GenericErrorDto>(e);
            return BadRequest(error);
        }

        return Created("", null);
    }
}