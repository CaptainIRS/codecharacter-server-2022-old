using System;
using System.Linq;
using System.Threading.Tasks;
using CodeCharacter.Core.Data;
using CodeCharacter.Core.Entities;
using CodeCharacter.Core.Exceptions;
using CodeCharacter.Core.Services;
using Microsoft.EntityFrameworkCore;
using NodaTime;
using NUnit.Framework;

namespace CodeCharacter.Tests.ServiceTests;

[TestFixture]
public class NotificationServiceTest : BaseServiceTests
{
    private UserEntity _user = new("user", "user@test.com");

    private async Task CreateUser(CodeCharacterDbContext context)
    {
        context.Users.Add(_user);
        await context.SaveChangesAsync();
        _user = context.Users.First(u => u.UserName == "user");
    }

    [Test]
    public async Task GetAllNotifications_ShouldReturnAllNotifications()
    {
        await using var context = new CodeCharacterDbContext(DbContextOptions);
        await context.Database.EnsureCreatedAsync();
        await CreateUser(context);

        var notification = new NotificationEntity
        {
            Title = "Test",
            Content = "Test",
            Read = false,
            CreatedAt = Instant.FromUtc(2020, 1, 1, 0, 0),
            User = _user
        };
        context.Notifications.Add(notification);
        await context.SaveChangesAsync();

        var notificationService = new NotificationService(context);
        var notifications = await notificationService.GetAllNotifications(_user);

        var notificationEntities = notifications.ToList();
        Assert.IsTrue(notificationEntities.Count == 1);
        Assert.IsTrue(notificationEntities.First().Title == notification.Title);
        Assert.IsTrue(notificationEntities.First().Content == notification.Content);
        Assert.IsTrue(notificationEntities.First().Read == notification.Read);
        Assert.IsTrue(notificationEntities.First().CreatedAt == notification.CreatedAt);
        Assert.IsTrue(notificationEntities.First().User.Id == _user.Id);
    }

    [Test]
    public async Task SaveNotificationReadStatus_UpdatesNotificationReadStatus()
    {
        await using var context = new CodeCharacterDbContext(DbContextOptions);
        await context.Database.EnsureCreatedAsync();
        await CreateUser(context);

        var notification = new NotificationEntity
        {
            Title = "Test",
            Content = "Test",
            Read = false,
            CreatedAt = Instant.FromUtc(2020, 1, 1, 0, 0),
            User = _user
        };
        context.Notifications.Add(notification);
        await context.SaveChangesAsync();

        var notificationService = new NotificationService(context);
        await notificationService.SaveNotificationReadStatus(_user, notification.Id, true);

        var updatedNotification = await context.Notifications.FirstAsync(n => n.Id == notification.Id);
        Assert.IsTrue(updatedNotification.Read);
    }

    [Test]
    public async Task SaveNotificationReadStatus_WithNonExistentNotification_ThrowsException()
    {
        await using var context = new CodeCharacterDbContext(DbContextOptions);
        await context.Database.EnsureCreatedAsync();
        await CreateUser(context);

        var notification = new NotificationEntity
        {
            Title = "Test",
            Content = "Test",
            Read = false,
            CreatedAt = Instant.FromUtc(2020, 1, 1, 0, 0),
            User = _user
        };
        context.Notifications.Add(notification);
        await context.SaveChangesAsync();

        var notificationService = new NotificationService(context);
        var exception = Assert.ThrowsAsync<GenericException>(async () =>
            await notificationService.SaveNotificationReadStatus(_user, Guid.NewGuid(), true));
        Assert.That(exception, Is.Not.Null);
        Assert.IsTrue(exception?.Message == "Notification not found");
    }
}