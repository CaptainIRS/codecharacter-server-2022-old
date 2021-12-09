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
    /// <param name="codeRevision"></param>
    /// <returns></returns>
    Task<IActionResult> CreateCodeRevision(CodeRevisionEntity codeRevision);

    /// <summary>
    ///     Get code revision by ID for user
    /// </summary>
    /// <param name="revisionId"></param>
    /// <returns></returns>
    Task<IActionResult> GetCodeRevision(Guid revisionId);

    /// <summary>
    ///     Get all code revisions for user
    /// </summary>
    /// <returns></returns>
    Task<IActionResult> GetAllCodeRevisions();

    /// <summary>
    ///     Get latest code for user
    /// </summary>
    /// <returns></returns>
    Task<IActionResult> GetLatestCode();

    /// <summary>
    ///     Update latest code for user
    /// </summary>
    /// <param name="codeRevision"></param>
    /// <returns></returns>
    Task<IActionResult> UpdateLatestCode(CodeRevisionEntity codeRevision);
}