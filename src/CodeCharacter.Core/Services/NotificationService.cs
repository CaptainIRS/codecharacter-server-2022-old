using CodeCharacter.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CodeCharacter.Core.Services;

/// <inheritdoc />
public class NotificationService : INotificationService
{
    /// <inheritdoc />
    public Task<IActionResult> GetAllNotifications()
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public Task<IActionResult> SaveNotificationReadStatus(Guid notificationId, bool readStatus)
    {
        throw new NotImplementedException();
    }
}