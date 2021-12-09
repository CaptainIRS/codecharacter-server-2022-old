using CodeCharacter.Core.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CodeCharacter.Core.Interfaces;

/// <summary>
///     Service for CRUD operations on the code table
/// </summary>
public interface ICodeService
{
    /// <summary>
    ///     Create code revision for user
    /// </summary>
    /// <param name="user"></param>
    /// <param name="code"></param>
    /// <param name="parentRevision"></param>
    /// <returns></returns>
    Task CreateCodeRevision(UserEntity user, string code, Guid? parentRevision);

    /// <summary>
    ///     Get code revision by ID for user
    /// </summary>
    /// <param name="user"></param>
    /// <param name="revisionId"></param>
    /// <returns></returns>
    Task<CodeRevisionEntity> GetCodeRevision(UserEntity user, Guid revisionId);

    /// <summary>
    ///     Get all code revisions for user
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    Task<List<CodeRevisionEntity>> GetAllCodeRevisions(UserEntity user);

    /// <summary>
    ///     Get latest code for user
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    Task<CodeEntity> GetLatestCode(UserEntity user);

    /// <summary>
    ///     Update latest code for user
    /// </summary>
    /// <param name="user"></param>
    /// <param name="code"></param>
    /// <returns></returns>
    Task UpdateLatestCode(UserEntity user, string code);
}