using CodeCharacter.Core.Entities;
using CodeCharacter.Core.Interfaces;
using CodeCharacter.CoreLibrary.Controllers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CodeCharacter.Core.Controllers;

/// <inheritdoc />
public class NotificationController : NotificationApiController
{
    private readonly INotificationService _notificationService;
    private readonly UserManager<UserEntity> _userManager;

    /// <summary>
    ///     Constructor
    /// </summary>
    /// <param name="notificationService"></param>
    /// <param name="userManager"></param>
    public NotificationController(INotificationService notificationService, UserManager<UserEntity> userManager)
    {
        _notificationService = notificationService;
        _userManager = userManager;
    }

    /// <inheritdoc />
    public override async Task<IActionResult> GetAllNotifications()
    {
        var user = await _userManager.GetUserAsync(HttpContext.User)!;
        await _notificationService.GetAllNotifications(user);
        return Ok();
    }

    /// <inheritdoc />
    public override async Task<IActionResult> SaveNotificationReadStatus(Guid notificationId, bool readStatus)
    {
        var user = await _userManager.GetUserAsync(HttpContext.User)!;
        await _notificationService.SaveNotificationReadStatus(user, notificationId, readStatus);
        return Ok();
    }
}