using CodeCharacter.Core.Entities;
using CodeCharacter.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CodeCharacter.Core.Services;

/// <inheritdoc />
public class CodeService : ICodeService
{
    /// <inheritdoc />
    public async Task<IActionResult> CreateCodeRevision(CodeRevisionEntity codeRevision)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public async Task<IActionResult> GetCodeRevision(Guid revisionId)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public async Task<IActionResult> GetAllCodeRevisions()
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public async Task<IActionResult> GetLatestCode()
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public async Task<IActionResult> UpdateLatestCode(CodeRevisionEntity codeRevision)
    {
        throw new NotImplementedException();
    }
}