/*
 * CodeCharacter API
 *
 * Specification of the CodeCharacter API
 *
 * The version of the OpenAPI document: 2022.0.1
 * Contact: delta@nitt.edu
 * Generated by: https://openapi-generator.tech
 */

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

using CodeCharacter.CoreLibrary.Attributes;
using CodeCharacter.CoreLibrary.Models;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CodeCharacter.CoreLibrary.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [ApiController]
    public abstract class UserApiController : ControllerBase
    {
        /// <summary>
        /// Activate user
        /// </summary>
        /// <remarks>Activate user by using the token sent via email</remarks>
        /// <param name="userId">Username of the user</param>
        /// <param name="activateUserRequestDto"></param>
        /// <response code="200">OK</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="404">Not Found</response>
        /// <response code="422">Unprocessable Entity</response>
        [HttpPost]
        [Route("/users/{userId}/activate")]
        [Consumes("application/json")]
        [ValidateModelState]
        public abstract Task<IActionResult> ActivateUser([FromRoute(Name = "userId")][Required][StringLength(32, MinimumLength = 5)] string userId, [FromBody] ActivateUserRequestDto activateUserRequestDto);

        /// <summary>
        /// Get user rating history
        /// </summary>
        /// <remarks>Get user rating history</remarks>
        /// <param name="userId">Username of the user</param>
        /// <response code="200">OK</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">Not Found</response>
        [HttpGet]
        [Route("/users/{userId}/ratingHistory")]
        [Authorize]
        [ValidateModelState]
        [ProducesResponseType(statusCode: 200, type: typeof(List<RatingHistoryDto>))]
        public abstract Task<IActionResult> GetRatingHistory([FromRoute(Name = "userId")][Required][StringLength(32, MinimumLength = 5)] string userId);

        /// <summary>
        /// Register user
        /// </summary>
        /// <remarks>Register user</remarks>
        /// <param name="registerUserRequestDto"></param>
        /// <response code="201">Created</response>
        /// <response code="422">Unprocessable Entity</response>
        [HttpPost]
        [Route("/users")]
        [Consumes("application/json")]
        [ValidateModelState]
        public abstract Task<IActionResult> Register([FromBody] RegisterUserRequestDto registerUserRequestDto);
    }
}
