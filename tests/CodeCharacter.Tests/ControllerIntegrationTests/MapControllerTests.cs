using System;
using System.Collections.Generic;
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

public class FakeMapService : IMapService
{
    private readonly MapEntity _mapEntity = new()
    {
        Map = "print('hello, world')",
        LastSavedAt = Instant.FromUtc(2020, 1, 1, 0, 0),
        UserId = 1
    };

    private readonly List<MapRevisionEntity> _mapRevisions = new()
    {
        new MapRevisionEntity
        {
            Id = Guid.NewGuid(),
            Map = "print('hello, world')",
            ParentRevision = null,
            User = new UserEntity("test@test.com")
        }
    };

    public Task CreateMapRevision(UserEntity user, string map, Guid? parentRevision)
    {
        return map == "pass" ? Task.CompletedTask : throw new GenericException("Parent revision not found");
    }

    public Task<MapRevisionEntity> GetMapRevision(UserEntity user, Guid revisionId)
    {
        return revisionId.ToString().Contains("000")
            ? Task.FromResult(_mapRevisions[0])
            : throw new GenericException("Map revision not found");
    }

    public Task<List<MapRevisionEntity>> GetAllMapRevisions(UserEntity user)
    {
        return Task.FromResult(_mapRevisions);
    }

    public Task<MapEntity> GetLatestMap(UserEntity user)
    {
        return Task.FromResult(_mapEntity);
    }

    public Task UpdateLatestMap(UserEntity user, string map)
    {
        return Task.CompletedTask;
    }
}

[TestFixture]
public class MapControllerTests : BaseControllerTests
{
    [Test]
    public async Task CreateMapRevision_ShouldCreateMapRevision()
    {
        var client = GetClientForService<IMapService, FakeMapService>(true);
        var response =
            await client.PostAsJsonAsync("/user/map/revisions", new CreateMapRevisionRequestDto { Map = "pass" });
        Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
    }

    [Test]
    public async Task CreateMapRevision_WithInvalidParent_ShouldReturnBadRequest()
    {
        var client = GetClientForService<IMapService, FakeMapService>(true);
        var response =
            await client.PostAsJsonAsync("/user/map/revisions", new CreateMapRevisionRequestDto { Map = "fail" });
        Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        var error = await response.Content.ReadFromJsonAsync<GenericErrorDto>();
        Assert.AreEqual("Parent revision not found", error?.Message);
    }

    [Test]
    public async Task GetMapRevision_ShouldReturnMapRevision()
    {
        var client = GetClientForService<IMapService, FakeMapService>(true);
        var response = await client.GetAsync("/user/map/revisions/123e4567-e89b-12d3-a456-426614174000");
        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        var mapRevision = await response.Content.ReadFromJsonAsync<MapRevisionDto>();
        Assert.That(mapRevision, Is.Not.Null);
    }

    [Test]
    public async Task GetMapRevision_WithInvalidId_ShouldReturnNotFound()
    {
        var client = GetClientForService<IMapService, FakeMapService>(true);
        var response = await client.GetAsync("/user/map/revisions/123e4567-e89b-12d3-a456-426614174999");
        Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Test]
    public async Task GetAllMapRevisions_ShouldReturnAllMapRevisions()
    {
        var client = GetClientForService<IMapService, FakeMapService>(true);
        var response = await client.GetAsync("/user/map/revisions");
        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        var mapRevisions = await response.Content.ReadFromJsonAsync<List<MapRevisionDto>>();
        Assert.That(mapRevisions, Is.Not.Null);
        Assert.AreEqual(1, mapRevisions?.Count);
    }

    [Test]
    public async Task GetLatestMap_ShouldReturnLatestMap()
    {
        var client = GetClientForService<IMapService, FakeMapService>(true);
        var response = await client.GetAsync("/user/map/latest");
        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        var map = await response.Content.ReadFromJsonAsync<MapDto>();
        Assert.That(map, Is.Not.Null);
        Assert.AreEqual("print('hello, world')", map?.Map);
    }

    [Test]
    public async Task UpdateLatestMap_ShouldUpdateLatestMap()
    {
        var client = GetClientForService<IMapService, FakeMapService>(true);
        var response = await client.PostAsJsonAsync("/user/map/latest",
            new UpdateLatestMapRequestDto { Map = "print('hello, world')" });
        Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);
    }
}