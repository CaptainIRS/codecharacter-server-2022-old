using Microsoft.AspNetCore.Mvc;

namespace CodeCharacter.Core.Interfaces;

/// <summary>
///     Service for notification operations
/// </summary>
public interface INotificationService
{
    /// <summary>
    ///     Get all notifications for a user
    /// </summary>
    /// <returns></returns>
    Task<IActionResult> GetAllNotifications();

    /// <summary>
    ///     Save notification read status
    /// </summary>
    /// <param name="notificationId"></param>
    /// <param name="readStatus"></param>
    /// <returns></returns>
    Task<IActionResult> SaveNotificationReadStatus(Guid notificationId, bool readStatus);
}