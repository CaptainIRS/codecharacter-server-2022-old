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

public class FakeCodeService : ICodeService
{
    private readonly CodeEntity _codeEntity = new()
    {
        Code = "print('hello, world')",
        LastSavedAt = Instant.FromUtc(2020, 1, 1, 0, 0),
        UserId = 1
    };

    private readonly List<CodeRevisionEntity> _codeRevisions = new()
    {
        new CodeRevisionEntity
        {
            Id = Guid.NewGuid(),
            Code = "print('hello, world')",
            ParentRevision = null,
            User = new UserEntity("test", "test@test.com")
        }
    };

    public Task CreateCodeRevision(UserEntity user, string code, Guid? parentRevision)
    {
        return code == "pass" ? Task.CompletedTask : throw new GenericException("Parent revision not found");
    }

    public Task<CodeRevisionEntity> GetCodeRevision(UserEntity user, Guid revisionId)
    {
        return revisionId.ToString().Contains("000")
            ? Task.FromResult(_codeRevisions[0])
            : throw new GenericException("Code revision not found");
    }

    public Task<List<CodeRevisionEntity>> GetAllCodeRevisions(UserEntity user)
    {
        return Task.FromResult(_codeRevisions);
    }

    public Task<CodeEntity> GetLatestCode(UserEntity user)
    {
        return Task.FromResult(_codeEntity);
    }

    public Task UpdateLatestCode(UserEntity user, string code)
    {
        return Task.CompletedTask;
    }
}

[TestFixture]
public class CodeControllerTests : BaseControllerTests
{
    [Test]
    public async Task CreateCodeRevision_ShouldCreateCodeRevision()
    {
        var client = GetClientForService<ICodeService, FakeCodeService>(true);
        var response =
            await client.PostAsJsonAsync("/user/code/revisions", new CreateCodeRevisionRequestDto { Code = "pass" });
        Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
    }

    [Test]
    public async Task CreateCodeRevision_WithInvalidParent_ShouldReturnBadRequest()
    {
        var client = GetClientForService<ICodeService, FakeCodeService>(true);
        var response =
            await client.PostAsJsonAsync("/user/code/revisions", new CreateCodeRevisionRequestDto { Code = "fail" });
        Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        var error = await response.Content.ReadFromJsonAsync<GenericErrorDto>();
        Assert.AreEqual("Parent revision not found", error?.Message);
    }

    [Test]
    public async Task GetCodeRevision_ShouldReturnCodeRevision()
    {
        var client = GetClientForService<ICodeService, FakeCodeService>(true);
        var response = await client.GetAsync("/user/code/revisions/123e4567-e89b-12d3-a456-426614174000");
        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        var codeRevision = await response.Content.ReadFromJsonAsync<CodeRevisionDto>();
        Assert.That(codeRevision, Is.Not.Null);
    }

    [Test]
    public async Task GetCodeRevision_WithInvalidId_ShouldReturnNotFound()
    {
        var client = GetClientForService<ICodeService, FakeCodeService>(true);
        var response = await client.GetAsync("/user/code/revisions/123e4567-e89b-12d3-a456-426614174999");
        Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Test]
    public async Task GetAllCodeRevisions_ShouldReturnAllCodeRevisions()
    {
        var client = GetClientForService<ICodeService, FakeCodeService>(true);
        var response = await client.GetAsync("/user/code/revisions");
        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        var codeRevisions = await response.Content.ReadFromJsonAsync<List<CodeRevisionDto>>();
        Assert.That(codeRevisions, Is.Not.Null);
        Assert.AreEqual(1, codeRevisions?.Count);
    }

    [Test]
    public async Task GetLatestCode_ShouldReturnLatestCode()
    {
        var client = GetClientForService<ICodeService, FakeCodeService>(true);
        var response = await client.GetAsync("/user/code/latest");
        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        var code = await response.Content.ReadFromJsonAsync<CodeDto>();
        Assert.That(code, Is.Not.Null);
        Assert.AreEqual("print('hello, world')", code?.Code);
    }

    [Test]
    public async Task UpdateLatestCode_ShouldUpdateLatestCode()
    {
        var client = GetClientForService<ICodeService, FakeCodeService>(true);
        var response = await client.PostAsJsonAsync("/user/code/latest",
            new UpdateLatestCodeRequestDto { Code = "print('hello, world')" });
        Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);
    }
}