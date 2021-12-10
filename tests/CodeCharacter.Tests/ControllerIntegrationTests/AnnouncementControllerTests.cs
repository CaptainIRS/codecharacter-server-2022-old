using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using CodeCharacter.Core.Entities;
using CodeCharacter.Core.Exceptions;
using CodeCharacter.Core.Interfaces;
using CodeCharacter.CoreLibrary.Models;
using NodaTime;
using NUnit.Framework;

namespace CodeCharacter.Tests.ControllerIntegrationTests;

public class FakeAnnouncementService : IAnnouncementService
{
    private readonly List<AnnouncementEntity> _announcements = new()
    {
        new AnnouncementEntity
        {
            Id = 1,
            Message = "Test Message 1",
            Timestamp = Instant.FromUtc(2020, 1, 1, 0, 0)
        }
    };

    public Task<List<AnnouncementEntity>> GetAllAnnouncements()
    {
        return Task.FromResult(_announcements);
    }

    public Task<AnnouncementEntity> GetAnnouncement(int id)
    {
        return Task.FromResult(id == 1 ? _announcements[0] : throw new GenericException("Announcement not found"));
    }

    public Task CreateAnnouncement(string announcement)
    {
        return Task.CompletedTask;
    }

    public Task UpdateAnnouncement(int announcementId, string announcement)
    {
        return announcementId == 1 ? Task.CompletedTask : throw new GenericException("Announcement not found");
    }

    public Task DeleteAnnouncement(int id)
    {
        return id == 1 ? Task.CompletedTask : throw new GenericException("Announcement not found");
    }
}

[TestFixture]
public class AnnouncementControllerTest : BaseControllerTests
{
    [Test]
    public async Task AllAnnouncementRoutes_AreGuarded()
    {
        var client = GetClientForService<IAnnouncementService, FakeAnnouncementService>();

        var response = await client.GetAsync("/announcements");
        Assert.AreEqual(response.StatusCode, HttpStatusCode.Unauthorized);

        response = await client.GetAsync("/announcements/1");
        Assert.AreEqual(response.StatusCode, HttpStatusCode.Unauthorized);

        response = await client.PostAsync("/announcements", null);
        Assert.AreEqual(response.StatusCode, HttpStatusCode.Unauthorized);

        response = await client.PatchAsync("/announcements/1", null);
        Assert.AreEqual(response.StatusCode, HttpStatusCode.Unauthorized);

        response = await client.DeleteAsync("/announcements/1");
        Assert.AreEqual(response.StatusCode, HttpStatusCode.Unauthorized);
    }

    [Test]
    public async Task GetAllAnnouncements_ReturnsAllAnnouncements()
    {
        var client = GetClientForService<IAnnouncementService, FakeAnnouncementService>(true);
        var response = await client.GetAsync("/announcements");
        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        var announcements = await response.Content.ReadFromJsonAsync<List<AnnouncementDto>>();
        Assert.AreEqual(1, announcements?.Count);
    }

    [Test]
    public async Task GetAnnouncement_ReturnsAnnouncement()
    {
        var client = GetClientForService<IAnnouncementService, FakeAnnouncementService>(true);
        var response = await client.GetAsync("/announcements/1");
        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        var announcement = await response.Content.ReadFromJsonAsync<AnnouncementDto>();
        Assert.AreEqual(1, announcement?.Id);
    }

    [Test]
    public async Task GetAnnouncement_WithInvalidId_ReturnsNotFound()
    {
        var client = GetClientForService<IAnnouncementService, FakeAnnouncementService>(true);
        var response = await client.GetAsync("/announcements/0");
        Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        var error = await response.Content.ReadFromJsonAsync<GenericErrorDto>();
        Assert.AreEqual("Announcement not found", error?.Message);
    }

    [Test]
    public async Task CreateAnnouncement_CreatesAnnouncement()
    {
        var client = GetClientForService<IAnnouncementService, FakeAnnouncementService>(true);
        var response = await client.PostAsJsonAsync("/announcements", new AnnouncementDto { Message = "Test Message" });
        Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
    }

    [Test]
    public async Task UpdateAnnouncement_UpdatesAnnouncement()
    {
        var client = GetClientForService<IAnnouncementService, FakeAnnouncementService>(true);
        var response = await client.PatchAsync("/announcements/1",
            new StringContent(new CreateAnnouncementRequestDto { Message = "Test Message" }.ToJson(), Encoding.UTF8,
                "application/json"));
        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
    }

    [Test]
    public async Task UpdateAnnouncement_WithInvalidId_ReturnsNotFound()
    {
        var client = GetClientForService<IAnnouncementService, FakeAnnouncementService>(true);
        var response = await client.PatchAsync("/announcements/0",
            new StringContent(new AnnouncementDto { Message = "Test Message" }.ToJson(), Encoding.UTF8,
                "application/json"));
        Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        var error = await response.Content.ReadFromJsonAsync<GenericErrorDto>();
        Assert.AreEqual("Announcement not found", error?.Message);
    }

    [Test]
    public async Task DeleteAnnouncement_DeletesAnnouncement()
    {
        var client = GetClientForService<IAnnouncementService, FakeAnnouncementService>(true);
        var response = await client.DeleteAsync("/announcements/1");
        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
    }

    [Test]
    public async Task DeleteAnnouncement_WithInvalidId_ReturnsNotFound()
    {
        var client = GetClientForService<IAnnouncementService, FakeAnnouncementService>(true);
        var response = await client.DeleteAsync("/announcements/0");
        Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        var error = await response.Content.ReadFromJsonAsync<GenericErrorDto>();
        Assert.AreEqual("Announcement not found", error?.Message);
    }
}