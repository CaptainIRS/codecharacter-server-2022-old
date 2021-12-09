using System;
using System.Linq;
using System.Threading.Tasks;
using CodeCharacter.Core.Data;
using CodeCharacter.Core.Entities;
using CodeCharacter.Core.Services;
using NodaTime;
using NUnit.Framework;

namespace CodeCharacter.Tests.ServiceTests;

[TestFixture]
public class AnnouncementServiceTests : BaseServiceTests
{
    [Test]
    public async Task GetAnnouncements_ShouldReturnAnnouncements()
    {
        await using var context = new CodeCharacterDbContext(DbContextOptions);

        await context.Database.EnsureCreatedAsync();
        context.Announcements.Add(new AnnouncementEntity
        {
            Id = 1,
            Message = "Test Announcement",
            Timestamp = Instant.FromUtc(2022, 1, 1, 0, 0)
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
            Id = 1,
            Message = "Test Announcement",
            Timestamp = Instant.FromUtc(2022, 1, 1, 0, 0)
        });
        await context.SaveChangesAsync();

        var announcementService = new AnnouncementService(context);
        var exception = Assert.ThrowsAsync<Exception>(async () => await announcementService.GetAnnouncement(300));
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
        const string message = "Announcement Test 123";
        await announcementService.CreateAnnouncement(message);
        
        Assert.IsTrue(context.Announcements.Any());
        Assert.IsTrue(context.Announcements.First().Message == message);
    }
    
    [Test]
    public async Task UpdateAnnouncement_ShouldUpdateSuccessfully()
    {
        await using var context = new CodeCharacterDbContext(DbContextOptions);

        await context.Database.EnsureCreatedAsync();
        context.Announcements.Add(new AnnouncementEntity
        {
            Id = 1,
            Message = "Test Announcement",
            Timestamp = Instant.FromUtc(2022, 1, 1, 0, 0)
        });
        await context.SaveChangesAsync();
        
        var announcementService = new AnnouncementService(context);
        const string message = "Announcement Test 123";
        await announcementService.UpdateAnnouncement(1, message);
        
        Assert.IsTrue(context.Announcements.Any());
        Assert.IsTrue(context.Announcements.First().Message == message);
    }
    
    [Test]
    public async Task DeleteAnnouncement_ShouldDeleteSuccessfully()
    {
        await using var context = new CodeCharacterDbContext(DbContextOptions);

        await context.Database.EnsureCreatedAsync();
        context.Announcements.Add(new AnnouncementEntity
        {
            Id = 1,
            Message = "Test Announcement",
            Timestamp = Instant.FromUtc(2022, 1, 1, 0, 0)
        });
        await context.SaveChangesAsync();
        
        var announcementService = new AnnouncementService(context);
        await announcementService.DeleteAnnouncement(1);
        
        Assert.IsTrue(!context.Announcements.Any());
    }
}