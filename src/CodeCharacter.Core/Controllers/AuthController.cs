using System.Diagnostics.CodeAnalysis;
using AutoMapper;
using CodeCharacter.Core.Exceptions;
using CodeCharacter.Core.Interfaces;
using CodeCharacter.CoreLibrary.Controllers;
using CodeCharacter.CoreLibrary.Models;
using Microsoft.AspNetCore.Mvc;

namespace CodeCharacter.Core.Controllers;

/// <inheritdoc />
public class AuthController : AuthApiController
{
    private readonly IAuthService _authService;
    private readonly IMapper _mapper;

    /// <inheritdoc />
    public AuthController(IMapper mapper, IAuthService authService)
    {
        _mapper = mapper;
        _authService = authService;
    }

    /// <inheritdoc />
    [ExcludeFromCodeCoverage(Justification = "Remove once implemented")]
    public override Task<IActionResult> ForgotPassword(ForgotPasswordRequestDto forgotPasswordRequestDto)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public override async Task<IActionResult> Login(LoginRequestDto loginRequestDto)
    {
        try
        {
            await _authService.Login(loginRequestDto.Email, loginRequestDto.Password);
            return Ok();
        }
        catch (GenericException e)
        {
            return Unauthorized(_mapper.Map<GenericErrorDto>(e));
        }
    }

    /// <inheritdoc />
    [ExcludeFromCodeCoverage(Justification = "Remove once implemented")]
    public override Task<IActionResult> ResetPassword(ResetPasswordRequestDto resetPasswordRequestDto)
    {
        throw new NotImplementedException();
    }
}