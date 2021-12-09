using CodeCharacter.Core.Interfaces;
using CodeCharacter.CoreLibrary.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace CodeCharacter.Core.Controllers;

/// <inheritdoc />
public class NotificationController : NotificationApiController
{
    private readonly INotificationService _notificationService;

    /// <summary>
    ///     Constructor
    /// </summary>
    /// <param name="notificationService"></param>
    public NotificationController(INotificationService notificationService)
    {
        _notificationService = notificationService;
    }

    /// <inheritdoc />
    public override async Task<IActionResult> GetAllNotifications()
    {
        return await _notificationService.GetAllNotifications();
    }

    /// <inheritdoc />
    public override async Task<IActionResult> SaveNotificationReadStatus(Guid notificationId, bool body)
    {
        return await _notificationService.SaveNotificationReadStatus(notificationId, body);
    }
}