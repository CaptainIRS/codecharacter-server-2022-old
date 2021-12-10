using System;
using System.Linq;
using System.Threading.Tasks;
using CodeCharacter.Core.Data;
using CodeCharacter.Core.Entities;
using CodeCharacter.Core.Exceptions;
using CodeCharacter.Core.Services;
using NodaTime;
using NUnit.Framework;

namespace CodeCharacter.Tests.ServiceTests;

[TestFixture]
public class AnnouncementServiceTests : BaseServiceTests
{
    private const string Message = "TestAnnouncement";
    private readonly Instant Timestamp = Instant.FromUtc(2020, 1, 1, 0, 0);

    [Test]
    public async Task GetAnnouncements_ShouldReturnAnnouncements()
    {
        await using var context = new CodeCharacterDbContext(DbContextOptions);

        await context.Database.EnsureCreatedAsync();
        context.Announcements.Add(new AnnouncementEntity
        {
            Message = Message,
            Timestamp = Timestamp
        });
        await context.SaveChangesAsync();

        var announcementService = new AnnouncementService(context);
        var announcements = await announcementService.GetAllAnnouncements();

        Assert.IsNotNull(announcements);
        Assert.IsTrue(announcements.Count == 1);
    }

    [Test]
    public async Task GetOneAnnouncement_ShouldReturnAnnouncement()
    {
        await using var context = new CodeCharacterDbContext(DbContextOptions);

        await context.Database.EnsureCreatedAsync();
        context.Announcements.Add(new AnnouncementEntity
        {
            Message = Message,
            Timestamp = Timestamp
        });
        await context.SaveChangesAsync();

        var announcementService = new AnnouncementService(context);
        var announcement = await announcementService.GetAnnouncement(1);
        Assert.IsTrue(announcement.Id == 1);
        Assert.IsTrue(announcement.Message == Message);
        Assert.IsTrue(announcement.Timestamp == Timestamp);
    }

    [Test]
    public async Task GetOneAnnouncement_WithNonExistentId_ShouldThrowException()
    {
        await using var context = new CodeCharacterDbContext(DbContextOptions);

        await context.Database.EnsureCreatedAsync();
        context.Announcements.Add(new AnnouncementEntity
        {
            Message = Message,
            Timestamp = Timestamp
        });
        await context.SaveChangesAsync();

        var announcementService = new AnnouncementService(context);
        var exception = Assert.ThrowsAsync<GenericException>(async () => await announcementService.GetAnnouncement(300));
        Assert.That(exception, Is.Not.Null);
        Assert.IsTrue(exception?.Message.Contains("Announcement not found"));
    }

    [Test]
    public async Task CreateAnnouncement_ShouldCreateSuccessfully()
    {
        await using var context = new CodeCharacterDbContext(DbContextOptions);

        await context.Database.EnsureCreatedAsync();
        Assert.IsTrue(!context.Announcements.Any());

        var announcementService = new AnnouncementService(context);
        await announcementService.CreateAnnouncement(Message);

        Assert.IsTrue(context.Announcements.Any());
        Assert.IsTrue(context.Announcements.First().Message == Message);
    }

    [Test]
    public async Task UpdateAnnouncement_ShouldUpdateSuccessfully()
    {
        await using var context = new CodeCharacterDbContext(DbContextOptions);

        await context.Database.EnsureCreatedAsync();
        context.Announcements.Add(new AnnouncementEntity
        {
            Message = Message,
            Timestamp = Timestamp
        });
        await context.SaveChangesAsync();

        var announcementService = new AnnouncementService(context);
        await announcementService.UpdateAnnouncement(1, Message);

        Assert.IsTrue(context.Announcements.Any());
        Assert.IsTrue(context.Announcements.First().Message == Message);
    }

    [Test]
    public async Task UpdateAnnouncement_WithNonExistentId_ShouldThrowException()
    {
        await using var context = new CodeCharacterDbContext(DbContextOptions);

        await context.Database.EnsureCreatedAsync();
        context.Announcements.Add(new AnnouncementEntity
        {
            Message = Message,
            Timestamp = Timestamp
        });
        await context.SaveChangesAsync();

        var announcementService = new AnnouncementService(context);
        var exception =
            Assert.ThrowsAsync<GenericException>(async () => await announcementService.UpdateAnnouncement(300, Message));
        Assert.That(exception, Is.Not.Null);
        Assert.IsTrue(exception?.Message.Contains("Announcement not found"));
    }

    [Test]
    public async Task DeleteAnnouncement_ShouldDeleteSuccessfully()
    {
        await using var context = new CodeCharacterDbContext(DbContextOptions);

        await context.Database.EnsureCreatedAsync();
        context.Announcements.Add(new AnnouncementEntity
        {
            Message = Message,
            Timestamp = Timestamp
        });
        await context.SaveChangesAsync();

        var announcementService = new AnnouncementService(context);
        await announcementService.DeleteAnnouncement(1);

        Assert.IsTrue(!context.Announcements.Any());
    }

    [Test]
    public async Task DeleteAnnouncement_WithNonExistentId_ShouldThrowException()
    {
        await using var context = new CodeCharacterDbContext(DbContextOptions);

        await context.Database.EnsureCreatedAsync();
        context.Announcements.Add(new AnnouncementEntity
        {
            Message = Message,
            Timestamp = Timestamp
        });
        await context.SaveChangesAsync();

        var announcementService = new AnnouncementService(context);
        var exception = Assert.ThrowsAsync<GenericException>(async () => await announcementService.DeleteAnnouncement(300));
        Assert.That(exception, Is.Not.Null);
        Assert.IsTrue(exception?.Message.Contains("Announcement not found"));
    }
}