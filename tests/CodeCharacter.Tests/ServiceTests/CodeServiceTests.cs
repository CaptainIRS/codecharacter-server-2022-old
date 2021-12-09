using System;
using System.Linq;
using System.Threading.Tasks;
using CodeCharacter.Core.Data;
using CodeCharacter.Core.Entities;
using CodeCharacter.Core.Services;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace CodeCharacter.Tests.ServiceTests;

[TestFixture]
public class CodeServiceTests : BaseServiceTests
{
    private UserEntity _user = new("user", "user@test.com");
    private UserEntity _impostor = new("impostor", "impostor@test.com");
    private async Task CreateUser(CodeCharacterDbContext context)
    {
        context.Users.Add(_user);
        context.Users.Add(_impostor);
        await context.SaveChangesAsync();
        _user = context.Users.First(u => u.UserName == "user");
        _impostor = context.Users.First(u => u.UserName == "impostor");
    }
    
    [Test]
    public async Task CreateCodeRevision_ShouldCreateCodeRevision()
    {
        await using var context = new CodeCharacterDbContext(DbContextOptions);
        await context.Database.EnsureCreatedAsync();
        await CreateUser(context);

        Assert.IsTrue(!context.CodeRevisions.Any());
        
        const string code = "public class TestClass0 { }";
        Guid? parentRevisionId = null;
        
        var codeService = new CodeService(context);
        await codeService.CreateCodeRevision(_user, code, parentRevisionId);
        
        Assert.IsTrue(context.CodeRevisions.Any());
        Assert.IsTrue(context.CodeRevisions.First().Code == code);
        Assert.IsTrue(context.CodeRevisions.First().ParentRevision == null);
    }
    
    [Test]
    public async Task CreateCodeRevision_WithInvalidParentRevision_ShouldThrowException()
    {
        await using var context = new CodeCharacterDbContext(DbContextOptions);
        await context.Database.EnsureCreatedAsync();
        await CreateUser(context);

        Assert.IsTrue(!context.CodeRevisions.Any());
        
        const string code = "public class TestClass1 { }";
        Guid? parentRevisionId = Guid.NewGuid();
        var codeService = new CodeService(context);
        var exception = Assert.ThrowsAsync<Exception>(async () => await codeService.CreateCodeRevision(_user, code, parentRevisionId));
        
        Assert.IsTrue(!context.CodeRevisions.Any());
        Assert.That(exception, Is.Not.Null);
        Assert.IsTrue(exception?.Message.Contains("Parent revision not found"));
    }
    
    [Test]
    public async Task GetCodeRevision_ShouldReturnCodeRevision()
    {
        await using var context = new CodeCharacterDbContext(DbContextOptions);
        await context.Database.EnsureCreatedAsync();
        await CreateUser(context);

        const string code = "public class TestClass2 { }";
        await context.CodeRevisions.AddAsync(new CodeRevisionEntity{
            User = _user,
            Code = code,
            ParentRevision = null
        });
        await context.SaveChangesAsync();
        
        var codeService = new CodeService(context);
        var codeRevision = await codeService.GetCodeRevision(_user, context.CodeRevisions.First().Id);
        
        Assert.IsTrue(codeRevision.Code == code);
        Assert.IsTrue(codeRevision.ParentRevision == null);
    }
    
    [Test]
    public async Task GetCodeRevision_WithNonExistentCodeRevision_ShouldThrowException()
    {
        await using var context = new CodeCharacterDbContext(DbContextOptions);
        await context.Database.EnsureCreatedAsync();
        await CreateUser(context);

        Assert.IsTrue(!context.CodeRevisions.Any());
        
        const string code = "public class TestClass3 { }";
        await context.CodeRevisions.AddAsync(new CodeRevisionEntity{
            User = _user,
            Code = code,
            ParentRevision = null
        });
        await context.SaveChangesAsync();

        var codeService = new CodeService(context);
        var exception = Assert.ThrowsAsync<Exception>(async () => await codeService.GetCodeRevision(_user, Guid.NewGuid()));
        
        Assert.That(exception, Is.Not.Null);
        Assert.IsTrue(exception?.Message.Contains("Code revision not found"));
    }
    
    [Test]
    public async Task GetCodeRevision_WithNonOwner_ShouldThrowException()
    {
        await using var context = new CodeCharacterDbContext(DbContextOptions);
        await context.Database.EnsureCreatedAsync();
        await CreateUser(context);

        const string code = "public class TestClass4 { }";
        await context.CodeRevisions.AddAsync(new CodeRevisionEntity{
            User = _user,
            Code = code,
            ParentRevision = null
        });
        await context.SaveChangesAsync();

        var codeService = new CodeService(context);
        var exception = Assert.ThrowsAsync<Exception>(async () => await codeService.GetCodeRevision(_impostor, context.CodeRevisions.First().Id));
        
        Assert.That(exception, Is.Not.Null);
        Assert.IsTrue(exception?.Message.Contains("Code revision not found"));
    }
    
    [Test]
    public async Task GetAllCodeRevisions_ShouldReturnAllCodeRevisions()
    {
        await using var context = new CodeCharacterDbContext(DbContextOptions);
        await context.Database.EnsureCreatedAsync();
        await CreateUser(context);

        const string code = "public class TestClass5 { }";
        await context.CodeRevisions.AddAsync(new CodeRevisionEntity{
            User = _user,
            Code = code,
            ParentRevision = null
        });
        await context.SaveChangesAsync();
        var parentRevision = context.CodeRevisions.First();

        const string code2 = "public class TestClass6 { }";
        await context.CodeRevisions.AddAsync(new CodeRevisionEntity{
            User = _user,
            Code = code2,
            ParentRevision = parentRevision
        });
        await context.SaveChangesAsync();

        var codeService = new CodeService(context);
        var codeRevisions = await codeService.GetAllCodeRevisions(_user);
        
        Assert.IsTrue(codeRevisions.Count() == 2);
        Assert.IsTrue(codeRevisions.First().Code == code);
        Assert.IsTrue(codeRevisions.First().ParentRevision == null);
        Assert.IsTrue(codeRevisions.Last().Code == code2);
        Assert.IsTrue(codeRevisions.Last().ParentRevision?.Id == parentRevision.Id);
    }
    
    [Test]
    public async Task GetLatestCode_ShouldReturnLatestCode()
    {
        await using var context = new CodeCharacterDbContext(DbContextOptions);
        await context.Database.EnsureCreatedAsync();
        await CreateUser(context);

        const string code = "public class TestClass7 { }";
        await context.Codes.AddAsync(new CodeEntity {
            UserId = _user.Id,
            Code = code
        });
        await context.SaveChangesAsync();

        var codeService = new CodeService(context);
        var codeEntity = await codeService.GetLatestCode(_user);
        
        Assert.IsTrue(codeEntity.Code == code);
        Assert.IsTrue(codeEntity.UserId == _user.Id);
    }
    
    [Test]
    public async Task UpdateLatestCode_ShouldUpdateLatestCode()
    {
        await using var context = new CodeCharacterDbContext(DbContextOptions);
        await context.Database.EnsureCreatedAsync();
        await CreateUser(context);

        const string code1 = "public class TestClass8 { }";
        await context.Codes.AddAsync(new CodeEntity {
            UserId = _user.Id,
            Code = code1
        });
        await context.SaveChangesAsync();
        
        var codeEntity = await context.Codes.FirstAsync();
        Assert.IsTrue(codeEntity.Code == code1);
        Assert.IsTrue(codeEntity.UserId == _user.Id);

        const string code2 = "public class TestClass9 { }";
        var codeService = new CodeService(context);
        await codeService.UpdateLatestCode(_user, code2);
        
        codeEntity = await context.Codes.FirstAsync();
        Assert.IsTrue(codeEntity.Code == code2);
        Assert.IsTrue(codeEntity.UserId == _user.Id);
    }
    
}