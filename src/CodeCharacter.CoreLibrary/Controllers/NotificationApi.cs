/*
 * CodeCharacter API
 *
 * Specification of the CodeCharacter API
 *
 * The version of the OpenAPI document: 2022.0.1
 * Contact: delta@nitt.edu
 * Generated by: https://openapi-generator.tech
 */

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using CodeCharacter.CoreLibrary.Attributes;
using CodeCharacter.CoreLibrary.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CodeCharacter.CoreLibrary.Controllers;

/// <summary>
/// </summary>
[ApiController]
public abstract class NotificationApiController : ControllerBase
{
    /// <summary>
    ///     Get all notifications
    /// </summary>
    /// <remarks>Get all notifications</remarks>
    /// <response code="200">OK</response>
    [HttpGet]
    [Route("/user/notifications")]
    [Authorize]
    [ValidateModelState]
    [ProducesResponseType(statusCode: 200, type: typeof(List<NotificationDto>))]
    public abstract Task<IActionResult> GetAllNotifications();

    /// <summary>
    ///     Save notification read status
    /// </summary>
    /// <remarks>Save notification read status</remarks>
    /// <param name="notificationId">ID of the notification</param>
    /// <param name="body"></param>
    /// <response code="204">No Content</response>
    /// <response code="401">Unauthorized</response>
    /// <response code="400">Bad Request</response>
    [HttpPut]
    [Route("/user/notifications/{notificationId}/read")]
    [Authorize]
    [Consumes("application/json")]
    [ValidateModelState]
    [ProducesResponseType(statusCode: 400, type: typeof(GenericErrorDto))]
    public abstract Task<IActionResult> SaveNotificationReadStatus(
        [FromRoute(Name = "notificationId")] [Required] Guid notificationId, [FromBody] bool body);
}