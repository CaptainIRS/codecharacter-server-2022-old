using AutoMapper;
using CodeCharacter.Core.Entities;
using CodeCharacter.Core.Exceptions;
using CodeCharacter.Core.Interfaces;
using CodeCharacter.CoreLibrary.Controllers;
using CodeCharacter.CoreLibrary.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CodeCharacter.Core.Controllers;

/// <inheritdoc />
public class NotificationController : NotificationApiController
{
    private readonly IMapper _mapper;
    private readonly INotificationService _notificationService;
    private readonly UserManager<UserEntity> _userManager;

    /// <summary>
    ///     Constructor
    /// </summary>
    /// <param name="notificationService"></param>
    /// <param name="mapper"></param>
    /// <param name="userManager"></param>
    public NotificationController(INotificationService notificationService, IMapper mapper,
        UserManager<UserEntity> userManager)
    {
        _notificationService = notificationService;
        _mapper = mapper;
        _userManager = userManager;
    }

    /// <inheritdoc />
    public override async Task<IActionResult> GetAllNotifications()
    {
        var user = await _userManager.GetUserAsync(HttpContext.User)!;
        var notificationEntities = await _notificationService.GetAllNotifications(user);
        var notificationDtos = _mapper.Map<IEnumerable<NotificationDto>>(notificationEntities);
        return Ok(notificationDtos);
    }

    /// <inheritdoc />
    public override async Task<IActionResult> SaveNotificationReadStatus(Guid notificationId, bool readStatus)
    {
        var user = await _userManager.GetUserAsync(HttpContext.User)!;
        try
        {
            await _notificationService.SaveNotificationReadStatus(user, notificationId, readStatus);
        }
        catch (GenericException e)
        {
            var error = _mapper.Map<GenericErrorDto>(e);
            return NotFound(error);
        }

        return NoContent();
    }
}