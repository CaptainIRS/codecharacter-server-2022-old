using CodeCharacter.Core.Data;
using CodeCharacter.Core.Entities;
using CodeCharacter.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using NodaTime;

namespace CodeCharacter.Core.Services;

/// <inheritdoc />
public class AnnouncementService : IAnnouncementService
{
    private readonly CodeCharacterDbContext _context;

    /// <summary>
    ///     Constructor
    /// </summary>
    /// <param name="context"></param>
    public AnnouncementService(CodeCharacterDbContext context)
    {
        _context = context;
    }

    /// <inheritdoc />
    public async Task<List<AnnouncementEntity>> GetAllAnnouncements()
    {
        return await _context.Announcements.ToListAsync();
    }

    /// <inheritdoc />
    public async Task<AnnouncementEntity> GetAnnouncement(int id)
    {
        var announcement = await _context.Announcements.FindAsync(id);
        if (announcement == null) throw new Exception("Announcement not found");

        return announcement;
    }

    /// <inheritdoc />
    public async Task CreateAnnouncement(string announcement)
    {
        await _context.Announcements.AddAsync(new AnnouncementEntity
        {
            Message = announcement,
            Timestamp = Instant.FromDateTimeUtc(DateTime.UtcNow)
        });
        await _context.SaveChangesAsync();
    }

    /// <inheritdoc />
    public async Task UpdateAnnouncement(int announcementId, string announcement)
    {
        var announcementToUpdate = await _context.Announcements.FindAsync(announcementId);
        if (announcementToUpdate == null) throw new Exception("Announcement not found");

        announcementToUpdate.Message = announcement;
        announcementToUpdate.Timestamp = Instant.FromDateTimeUtc(DateTime.UtcNow);
        _context.Announcements.Update(announcementToUpdate);
        await _context.SaveChangesAsync();
    }

    /// <inheritdoc />
    public async Task DeleteAnnouncement(int id)
    {
        var announcement = await _context.Announcements.FindAsync(id);
        if (announcement == null) throw new Exception("Announcement not found");
        _context.Announcements.Remove(announcement);
        await _context.SaveChangesAsync();
    }
}