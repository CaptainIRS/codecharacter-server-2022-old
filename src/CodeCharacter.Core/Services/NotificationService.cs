using CodeCharacter.Core.Data;
using CodeCharacter.Core.Entities;
using CodeCharacter.Core.Exceptions;
using CodeCharacter.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CodeCharacter.Core.Services;

/// <inheritdoc />
public class NotificationService : INotificationService
{
    private readonly CodeCharacterDbContext _context;

    /// <summary>
    ///     Constructor
    /// </summary>
    /// <param name="context"></param>
    public NotificationService(CodeCharacterDbContext context)
    {
        _context = context;
    }

    /// <inheritdoc />
    public async Task<IEnumerable<NotificationEntity>> GetAllNotifications(UserEntity user)
    {
        return await _context.Notifications.Where(n => n.User.Id == user.Id).ToListAsync();
    }

    /// <inheritdoc />
    public async Task SaveNotificationReadStatus(UserEntity user, Guid notificationId, bool readStatus)
    {
        var notification = await _context.Notifications.FindAsync(notificationId);
        if (notification == null || notification.User.Id != user.Id)
            throw new GenericException("Notification not found");
        notification.Read = readStatus;
        await _context.SaveChangesAsync();
    }
}