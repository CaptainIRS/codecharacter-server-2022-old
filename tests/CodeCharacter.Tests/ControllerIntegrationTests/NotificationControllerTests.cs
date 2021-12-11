using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;
using CodeCharacter.Core.Entities;
using CodeCharacter.Core.Exceptions;
using CodeCharacter.Core.Interfaces;
using CodeCharacter.CoreLibrary.Models;
using NodaTime;
using NUnit.Framework;

namespace CodeCharacter.Tests.ControllerIntegrationTests;

public class FakeNotificationService : INotificationService
{
    private readonly List<NotificationEntity> _notifications = new();
    private readonly UserEntity _user = new("user@test.com");

    public FakeNotificationService()
    {
        _notifications.Add(new NotificationEntity
        {
            Id = Guid.NewGuid(),
            Title = "Test",
            Content = "Test",
            CreatedAt = Instant.FromDateTimeUtc(DateTime.UtcNow),
            Read = false,
            User = _user
        });
        _notifications.Add(new NotificationEntity
        {
            Id = Guid.NewGuid(),
            Title = "Test",
            Content = "Test",
            CreatedAt = Instant.FromDateTimeUtc(DateTime.UtcNow),
            Read = false,
            User = _user
        });
    }

    public Task<IEnumerable<NotificationEntity>> GetAllNotifications(UserEntity user)
    {
        return Task.FromResult(_notifications.AsEnumerable());
    }

    public Task SaveNotificationReadStatus(UserEntity user, Guid notificationId, bool readStatus)
    {
        if (notificationId.ToString().Contains("000")) return Task.CompletedTask;
        throw new GenericException("Notification not found");
    }
}

[TestFixture]
public class NotificationControllerTests : BaseControllerTests
{
    [Test]
    public async Task GetAllNotifications_ReturnsNotifications()
    {
        var client = GetClientForService<INotificationService, FakeNotificationService>(true);

        var response = await client.GetFromJsonAsync<List<NotificationDto>>("/user/notifications");

        Assert.That(response, Is.Not.Null);
        Assert.AreEqual(2, response?.Count);
    }

    [Test]
    public async Task SaveNotificationReadStatus_UpdatesNotificationReadStatus()
    {
        var client = GetClientForService<INotificationService, FakeNotificationService>(true);

        var response =
            await client.PutAsJsonAsync("/user/notifications/123e4567-e89b-12d3-a456-426614174000/read", true);

        Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);
    }

    [Test]
    public async Task SaveNotificationReadStatus_WithNonExistentNotification_ThrowsException()
    {
        var client = GetClientForService<INotificationService, FakeNotificationService>(true);

        var response =
            await client.PutAsJsonAsync("/user/notifications/123e4567-e89b-12d3-a456-426614174999/read", true);

        Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
    }
}