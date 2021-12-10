using CodeCharacter.Core.Entities;

namespace CodeCharacter.Core.Interfaces;

/// <summary>
///     Service for notification operations
/// </summary>
public interface INotificationService
{
    /// <summary>
    ///     Get all notifications for a user
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    Task<IEnumerable<NotificationEntity>> GetAllNotifications(UserEntity user);

    /// <summary>
    ///     Save notification read status
    /// </summary>
    /// <param name="user"></param>
    /// <param name="notificationId"></param>
    /// <param name="readStatus"></param>
    /// <returns></returns>
    Task SaveNotificationReadStatus(UserEntity user, Guid notificationId, bool readStatus);
}