using System.Linq;
using System.Threading.Tasks;
using CodeCharacter.Core.Data;
using CodeCharacter.Core.Entities;
using CodeCharacter.Core.Services;
using NUnit.Framework;

namespace CodeCharacter.Tests.ServiceTests;

[TestFixture]
public class CurrentUserServiceTest : BaseServiceTests
{
    private UserEntity _user = new("user", "user@test.com");

    private PublicUserEntity _publicUser = new()
    {
        Name = "User",
        College = "Test",
        Country = "Test",
        AvatarId = 1
    };

    private async Task CreateUser(CodeCharacterDbContext context)
    {
        context.Users.Add(_user);
        await context.SaveChangesAsync();
        _user = context.Users.First(u => u.UserName == "user");

        _publicUser.UserId = _user.Id;
        context.PublicUsers.Add(_publicUser);
        await context.SaveChangesAsync();
        _publicUser = context.PublicUsers.First(u => u.UserId == _user.Id);
    }

    [Test]
    public async Task GetCurrentUser_ShouldReturnCorrectUser()
    {
        await using var context = new CodeCharacterDbContext(DbContextOptions);
        await context.Database.EnsureCreatedAsync();
        await CreateUser(context);

        var currentUserService = new CurrentUserService(context);
        var currentUser = await currentUserService.GetCurrentUser(_user);

        Assert.AreEqual(_publicUser.Name, currentUser.Name);
        Assert.AreEqual(_publicUser.College, currentUser.College);
        Assert.AreEqual(_publicUser.Country, currentUser.Country);
        Assert.AreEqual(_publicUser.AvatarId, currentUser.AvatarId);
        Assert.AreEqual(_publicUser.UserId, currentUser.UserId);
    }
}