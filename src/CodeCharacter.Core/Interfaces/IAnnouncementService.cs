using CodeCharacter.Core.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CodeCharacter.Core.Interfaces;

/// <summary>
///     Service for CRUD operations for announcements
/// </summary>
public interface IAnnouncementService
{
    /// <summary>
    ///     Gets all announcements
    /// </summary>
    /// <returns></returns>
    Task<List<AnnouncementEntity>> GetAllAnnouncements();

    /// <summary>
    ///     Gets an announcement by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<AnnouncementEntity> GetAnnouncement(int id);

    /// <summary>
    ///     Creates an announcement
    /// </summary>
    /// <param name="announcement"></param>
    /// <returns></returns>
    Task CreateAnnouncement(string announcement);

    /// <summary>
    ///     Updates an announcement
    /// </summary>
    /// <param name="announcementId"></param>
    /// <param name="announcement"></param>
    /// <returns></returns>
    Task UpdateAnnouncement(int announcementId, string announcement);

    /// <summary>
    ///     Deletes an announcement
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task DeleteAnnouncement(int id);
}