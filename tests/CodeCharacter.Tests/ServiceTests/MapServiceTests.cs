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
public class MapServiceTests : BaseServiceTests
{
    private UserEntity _user = new("user@test.com");
    private UserEntity _impostor = new("impostor@test.com");

    private async Task CreateUser(CodeCharacterDbContext context)
    {
        context.Users.Add(_user);
        context.Users.Add(_impostor);
        await context.SaveChangesAsync();
        _user = context.Users.First(u => u.Email == "user@test.com");
        _impostor = context.Users.First(u => u.Email == "impostor@test.com");
    }

    [Test]
    public async Task CreateMapRevision_ShouldCreateMapRevision()
    {
        await using var context = new CodeCharacterDbContext(DbContextOptions);
        await context.Database.EnsureCreatedAsync();
        await CreateUser(context);

        Assert.IsTrue(!context.MapRevisions.Any());

        const string map = "0000\n0000\n0000\n0000";
        Guid? parentRevisionId = null;

        var mapService = new MapService(context);
        await mapService.CreateMapRevision(_user, map, parentRevisionId);

        Assert.IsTrue(context.MapRevisions.Any());
        Assert.IsTrue(context.MapRevisions.First().Map == map);
        Assert.IsTrue(context.MapRevisions.First().ParentRevision == null);
    }

    [Test]
    public async Task CreateMapRevision_WithInvalidParentRevision_ShouldThrowException()
    {
        await using var context = new CodeCharacterDbContext(DbContextOptions);
        await context.Database.EnsureCreatedAsync();
        await CreateUser(context);

        Assert.IsTrue(!context.MapRevisions.Any());

        const string map = "0000\n0000\n0000\n0000";
        Guid? parentRevisionId = Guid.NewGuid();
        var mapService = new MapService(context);
        var exception =
            Assert.ThrowsAsync<GenericException>(async () =>
                await mapService.CreateMapRevision(_user, map, parentRevisionId));

        Assert.IsTrue(!context.MapRevisions.Any());
        Assert.That(exception, Is.Not.Null);
        Assert.IsTrue(exception?.Message.Contains("Parent revision not found"));
    }

    [Test]
    public async Task GetMapRevision_ShouldReturnMapRevision()
    {
        await using var context = new CodeCharacterDbContext(DbContextOptions);
        await context.Database.EnsureCreatedAsync();
        await CreateUser(context);

        const string map = "0000\n0000\n0000\n0000";
        await context.MapRevisions.AddAsync(new MapRevisionEntity
        {
            User = _user,
            Map = map,
            ParentRevision = null
        });
        await context.SaveChangesAsync();

        var mapService = new MapService(context);
        var mapRevision = await mapService.GetMapRevision(_user, context.MapRevisions.First().Id);

        Assert.IsTrue(mapRevision.Map == map);
        Assert.IsTrue(mapRevision.ParentRevision == null);
    }

    [Test]
    public async Task GetMapRevision_WithNonExistentMapRevision_ShouldThrowException()
    {
        await using var context = new CodeCharacterDbContext(DbContextOptions);
        await context.Database.EnsureCreatedAsync();
        await CreateUser(context);

        Assert.IsTrue(!context.MapRevisions.Any());

        const string map = "0000\n0000\n0000\n0000";
        await context.MapRevisions.AddAsync(new MapRevisionEntity
        {
            User = _user,
            Map = map,
            ParentRevision = null
        });
        await context.SaveChangesAsync();

        var mapService = new MapService(context);
        var exception =
            Assert.ThrowsAsync<GenericException>(async () => await mapService.GetMapRevision(_user, Guid.NewGuid()));

        Assert.That(exception, Is.Not.Null);
        Assert.IsTrue(exception?.Message.Contains("Map revision not found"));
    }

    [Test]
    public async Task GetMapRevision_WithNonOwner_ShouldThrowException()
    {
        await using var context = new CodeCharacterDbContext(DbContextOptions);
        await context.Database.EnsureCreatedAsync();
        await CreateUser(context);

        const string map = "0000\n0000\n0000\n0000";
        await context.MapRevisions.AddAsync(new MapRevisionEntity
        {
            User = _user,
            Map = map,
            ParentRevision = null
        });
        await context.SaveChangesAsync();

        var mapService = new MapService(context);
        var exception = Assert.ThrowsAsync<GenericException>(async () =>
            await mapService.GetMapRevision(_impostor, context.MapRevisions.First().Id));

        Assert.That(exception, Is.Not.Null);
        Assert.IsTrue(exception?.Message.Contains("Map revision not found"));
    }

    [Test]
    public async Task GetAllMapRevisions_ShouldReturnAllMapRevisions()
    {
        await using var context = new CodeCharacterDbContext(DbContextOptions);
        await context.Database.EnsureCreatedAsync();
        await CreateUser(context);

        const string map = "0000\n0000\n0000\n0000";
        await context.MapRevisions.AddAsync(new MapRevisionEntity
        {
            User = _user,
            Map = map,
            ParentRevision = null
        });
        await context.SaveChangesAsync();
        var parentRevision = context.MapRevisions.First();

        const string map2 = "0000\n0000\n0000\n0000";
        await context.MapRevisions.AddAsync(new MapRevisionEntity
        {
            User = _user,
            Map = map2,
            ParentRevision = parentRevision
        });
        await context.SaveChangesAsync();

        var mapService = new MapService(context);
        var mapRevisions = await mapService.GetAllMapRevisions(_user);

        Assert.IsTrue(mapRevisions.Count() == 2);
        Assert.IsTrue(mapRevisions.First().Map == map);
        Assert.IsTrue(mapRevisions.First().ParentRevision == null);
        Assert.IsTrue(mapRevisions.Last().Map == map2);
        Assert.IsTrue(mapRevisions.Last().ParentRevision?.Id == parentRevision.Id);
    }

    [Test]
    public async Task GetLatestMap_ShouldReturnLatestMap()
    {
        await using var context = new CodeCharacterDbContext(DbContextOptions);
        await context.Database.EnsureCreatedAsync();
        await CreateUser(context);

        const string map = "0000\n0000\n0000\n0000";
        await context.Maps.AddAsync(new MapEntity
        {
            UserId = _user.Id,
            Map = map
        });
        await context.SaveChangesAsync();

        var mapService = new MapService(context);
        var mapEntity = await mapService.GetLatestMap(_user);

        Assert.IsTrue(mapEntity.Map == map);
        Assert.IsTrue(mapEntity.UserId == _user.Id);
    }

    [Test]
    public async Task UpdateLatestMap_ShouldUpdateLatestMap()
    {
        await using var context = new CodeCharacterDbContext(DbContextOptions);
        await context.Database.EnsureCreatedAsync();
        await CreateUser(context);

        const string map1 = "0000\n0000\n0000\n0000";
        await context.Maps.AddAsync(new MapEntity
        {
            UserId = _user.Id,
            Map = map1,
            LastSavedAt = Instant.FromUtc(2020, 1, 1, 0, 0, 0)
        });
        await context.SaveChangesAsync();

        var mapEntity = await context.Maps.FirstAsync();
        Assert.IsTrue(mapEntity.Map == map1);
        Assert.IsTrue(mapEntity.UserId == _user.Id);

        const string map2 = "0000\n0000\n0000\n1111";

        var mapService = new MapService(context);
        await mapService.UpdateLatestMap(_user, map2);

        mapEntity = await context.Maps.FirstAsync();
        Assert.IsTrue(mapEntity.Map == map2);
        Assert.IsTrue(mapEntity.UserId == _user.Id);
        Assert.IsTrue(mapEntity.LastSavedAt > Instant.FromUtc(2020, 1, 1, 0, 0, 0));
    }
}