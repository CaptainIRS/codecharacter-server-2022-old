/*
 * CodeCharacter API
 *
 * Specification of the CodeCharacter API
 *
 * The version of the OpenAPI document: 2022.0.1
 * Contact: delta@nitt.edu
 * Generated by: https://openapi-generator.tech
 */

using System.Threading.Tasks;
using CodeCharacter.CoreLibrary.Attributes;
using CodeCharacter.CoreLibrary.Models;
using Microsoft.AspNetCore.Mvc;

namespace CodeCharacter.CoreLibrary.Controllers;

/// <summary>
/// </summary>
[ApiController]
public abstract class AuthApiController : ControllerBase
{
    /// <summary>
    ///     External Login
    /// </summary>
    /// <remarks>Redirect to challenge for the given external login provider</remarks>
    /// <param name="externalLoginRequestDto"></param>
    /// <response code="302">Found</response>
    /// <response code="400">Bad Request</response>
    [HttpPost]
    [Route("/auth/login/external")]
    [Consumes("application/json")]
    [ValidateModelState]
    [ProducesResponseType(statusCode: 400, type: typeof(GenericErrorDto))]
    public abstract Task<IActionResult> ExternalLogin([FromBody] ExternalLoginRequestDto externalLoginRequestDto);

    /// <summary>
    ///     External Login Callback
    /// </summary>
    /// <remarks>Callback after external login to redirect to the frontend with token information in cookie.</remarks>
    /// <response code="302">Found</response>
    /// <response code="401">Unauthorized</response>
    [HttpPost]
    [Route("/auth/login/external/callback")]
    [ValidateModelState]
    public abstract Task<IActionResult> ExternalLoginCallback();

    /// <summary>
    ///     Forgot password
    /// </summary>
    /// <remarks>Request password reset email to be sent when user forgot their password</remarks>
    /// <param name="forgotPasswordRequestDto"></param>
    /// <response code="202">Accepted</response>
    /// <response code="401">Unauthorized</response>
    /// <response code="400">Bad Request</response>
    [HttpPost]
    [Route("/auth/forgot-password")]
    [Consumes("application/json")]
    [ValidateModelState]
    [ProducesResponseType(statusCode: 400, type: typeof(GenericErrorDto))]
    public abstract Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordRequestDto forgotPasswordRequestDto);

    /// <summary>
    ///     Password Login
    /// </summary>
    /// <remarks>Login with email and password and get bearer token for authentication</remarks>
    /// <param name="passwordLoginRequestDto"></param>
    /// <response code="200">OK</response>
    /// <response code="401">Unauthorized</response>
    /// <response code="400">Bad Request</response>
    [HttpPost]
    [Route("/auth/login/password")]
    [Consumes("application/json")]
    [ValidateModelState]
    [ProducesResponseType(statusCode: 200, type: typeof(PasswordLoginResponseDto))]
    [ProducesResponseType(statusCode: 401, type: typeof(GenericErrorDto))]
    [ProducesResponseType(statusCode: 400, type: typeof(GenericErrorDto))]
    public abstract Task<IActionResult> PasswordLogin([FromBody] PasswordLoginRequestDto passwordLoginRequestDto);

    /// <summary>
    ///     Reset password
    /// </summary>
    /// <remarks>Reset password using the token from password reset email</remarks>
    /// <param name="resetPasswordRequestDto"></param>
    /// <response code="204">No Content</response>
    /// <response code="401">Unauthorized</response>
    /// <response code="400">Bad Request</response>
    [HttpPost]
    [Route("/auth/reset-password")]
    [Consumes("application/json")]
    [ValidateModelState]
    [ProducesResponseType(statusCode: 400, type: typeof(GenericErrorDto))]
    public abstract Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequestDto resetPasswordRequestDto);
}