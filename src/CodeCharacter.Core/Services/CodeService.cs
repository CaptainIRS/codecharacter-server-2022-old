using CodeCharacter.Core.Data;
using CodeCharacter.Core.Entities;
using CodeCharacter.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CodeCharacter.Core.Services;

/// <inheritdoc />
public class CodeService : ICodeService
{
    private readonly CodeCharacterDbContext _context;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="context"></param>
    public CodeService(CodeCharacterDbContext context)
    {
        _context = context;
    }
    /// <inheritdoc />
    public async Task CreateCodeRevision(UserEntity user, string code, Guid? parentRevision)
    {
        CodeRevisionEntity? parentCodeRevision = null;
        if (parentRevision != null)
        {
            parentCodeRevision = await _context.CodeRevisions.FindAsync(parentRevision);
            if (parentCodeRevision == null)
            {
                throw new Exception("Parent revision not found");
            }
        }
        await _context.CodeRevisions.AddAsync(new CodeRevisionEntity
        {
            Code = code,
            User = user,
            ParentRevision = parentCodeRevision
        });
        await _context.SaveChangesAsync();
    }

    /// <inheritdoc />
    public async Task<CodeRevisionEntity> GetCodeRevision(UserEntity user, Guid revisionId)
    {
        var codeRevision = await _context.CodeRevisions.FindAsync(revisionId);
        if (codeRevision == null || codeRevision.User.Id != user.Id)
        {
            throw new Exception("Code revision not found");
        }
        return codeRevision;
    }

    /// <inheritdoc />
    public async Task<List<CodeRevisionEntity>> GetAllCodeRevisions(UserEntity user)
    {
        return await _context.CodeRevisions.Where(x => x.User.Id == user.Id).ToListAsync();
    }

    /// <inheritdoc />
    public async Task<CodeEntity> GetLatestCode(UserEntity user)
    {
        var latestCode = await _context.Codes.FirstAsync(x => x.UserId == user.Id);
        return latestCode;
    }

    /// <inheritdoc />
    public async Task UpdateLatestCode(UserEntity user, string code)
    {
        var latestCode = await _context.Codes.FirstAsync(x => x.UserId == user.Id);
        latestCode.Code = code;
        await _context.SaveChangesAsync();
    }
}