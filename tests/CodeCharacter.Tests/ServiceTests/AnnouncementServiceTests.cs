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
    private readonly Instant _timestamp = Instant.FromUtc(2020, 1, 1, 0, 0);

    [Test]
    public async Task GetAnnouncements_ShouldReturnAnnouncements()
    {
        await using var context = new CodeCharacterDbContext(DbContextOptions);

        await context.Database.EnsureCreatedAsync();
        context.Announcements.Add(new AnnouncementEntity
        {
            Message = Message,
            Timestamp = _timestamp
        });
        await context.SaveChangesAsync();

        var announcementService = new AnnouncementService(context);
        var announcements = await announcementService.GetAllAnnouncements();

        Assert.That(announcements, Is.Not.Null);
        Assert.That(announcements.Count, Is.EqualTo(1));
    }

    [Test]
    public async Task GetOneAnnouncement_ShouldReturnAnnouncement()
    {
        await using var context = new CodeCharacterDbContext(DbContextOptions);

        await context.Database.EnsureCreatedAsync();
        context.Announcements.Add(new AnnouncementEntity
        {
            Message = Message,
            Timestamp = _timestamp
        });
        await context.SaveChangesAsync();

        var announcementService = new AnnouncementService(context);
        var announcement = await announcementService.GetAnnouncement(1);
        Assert.That(announcement.Id, Is.EqualTo(1));
        Assert.That(announcement.Message, Is.EqualTo(Message));
        Assert.That(announcement.Timestamp, Is.EqualTo(_timestamp));
    }

    [Test]
    public async Task GetOneAnnouncement_WithNonExistentId_ShouldThrowException()
    {
        await using var context = new CodeCharacterDbContext(DbContextOptions);

        await context.Database.EnsureCreatedAsync();
        context.Announcements.Add(new AnnouncementEntity
        {
            Message = Message,
            Timestamp = _timestamp
        });
        await context.SaveChangesAsync();

        var announcementService = new AnnouncementService(context);
        var exception =
            Assert.ThrowsAsync<GenericException>(async () => await announcementService.GetAnnouncement(300));
        Assert.That(exception, Is.Not.Null);
        Assert.That(exception?.Message, Is.EqualTo("Announcement not found"));
    }

    [Test]
    public async Task CreateAnnouncement_ShouldCreateSuccessfully()
    {
        await using var context = new CodeCharacterDbContext(DbContextOptions);

        await context.Database.EnsureCreatedAsync();
        Assert.That(context.Announcements.Any(), Is.False);

        var announcementService = new AnnouncementService(context);
        await announcementService.CreateAnnouncement(Message);

        Assert.That(context.Announcements.Any(), Is.True);
        Assert.That(context.Announcements.First().Message, Is.EqualTo(Message));
    }

    [Test]
    public async Task UpdateAnnouncement_ShouldUpdateSuccessfully()
    {
        await using var context = new CodeCharacterDbContext(DbContextOptions);

        await context.Database.EnsureCreatedAsync();
        context.Announcements.Add(new AnnouncementEntity
        {
            Message = Message,
            Timestamp = _timestamp
        });
        await context.SaveChangesAsync();

        var announcementService = new AnnouncementService(context);
        await announcementService.UpdateAnnouncement(1, Message);

        Assert.That(context.Announcements.Any(), Is.True);
        Assert.That(context.Announcements.First().Message, Is.EqualTo(Message));
    }

    [Test]
    public async Task UpdateAnnouncement_WithNonExistentId_ShouldThrowException()
    {
        await using var context = new CodeCharacterDbContext(DbContextOptions);

        await context.Database.EnsureCreatedAsync();
        context.Announcements.Add(new AnnouncementEntity
        {
            Message = Message,
            Timestamp = _timestamp
        });
        await context.SaveChangesAsync();

        var announcementService = new AnnouncementService(context);
        var exception =
            Assert.ThrowsAsync<GenericException>(async () =>
                await announcementService.UpdateAnnouncement(300, Message));
        Assert.That(exception, Is.Not.Null);
        Assert.That(exception?.Message, Is.EqualTo("Announcement not found"));
    }

    [Test]
    public async Task DeleteAnnouncement_ShouldDeleteSuccessfully()
    {
        await using var context = new CodeCharacterDbContext(DbContextOptions);

        await context.Database.EnsureCreatedAsync();
        context.Announcements.Add(new AnnouncementEntity
        {
            Message = Message,
            Timestamp = _timestamp
        });
        await context.SaveChangesAsync();

        var announcementService = new AnnouncementService(context);
        await announcementService.DeleteAnnouncement(1);

        Assert.That(context.Announcements.Any(), Is.False);
    }

    [Test]
    public async Task DeleteAnnouncement_WithNonExistentId_ShouldThrowException()
    {
        await using var context = new CodeCharacterDbContext(DbContextOptions);

        await context.Database.EnsureCreatedAsync();
        context.Announcements.Add(new AnnouncementEntity
        {
            Message = Message,
            Timestamp = _timestamp
        });
        await context.SaveChangesAsync();

        var announcementService = new AnnouncementService(context);
        var exception =
            Assert.ThrowsAsync<GenericException>(async () => await announcementService.DeleteAnnouncement(300));
        Assert.That(exception, Is.Not.Null);
        Assert.That(exception?.Message, Is.EqualTo("Announcement not found"));
    }
}