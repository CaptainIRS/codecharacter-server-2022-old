using CodeCharacter.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CodeCharacter.Core.Services;

/// <inheritdoc />
public class NotificationService : INotificationService
{
    /// <inheritdoc />
    public async Task<IActionResult> GetAllNotifications()
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public async Task<IActionResult> SaveNotificationReadStatus(Guid notificationId, bool readStatus)
    {
        throw new NotImplementedException();
    }
}