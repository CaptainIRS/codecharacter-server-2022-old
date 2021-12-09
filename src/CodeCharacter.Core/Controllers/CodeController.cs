using AutoMapper;
using CodeCharacter.Core.Entities;
using CodeCharacter.Core.Interfaces;
using CodeCharacter.CoreLibrary.Controllers;
using CodeCharacter.CoreLibrary.Models;
using Microsoft.AspNetCore.Mvc;

namespace CodeCharacter.Core.Controllers;

/// <inheritdoc />
public class CodeController : CodeApiController
{
    private readonly ICodeService _codeService;
    private readonly IMapper _mapper;

    /// <summary>
    ///     Constructor
    /// </summary>
    /// <param name="codeService"></param>
    /// <param name="mapper"></param>
    public CodeController(ICodeService codeService, IMapper mapper)
    {
        _codeService = codeService;
        _mapper = mapper;
    }

    /// <inheritdoc />
    public override async Task<IActionResult> CreateCodeRevision(
        CreateCodeRevisionRequestDto createCodeRevisionRequestDto)
    {
        var codeRevision = _mapper.Map<CodeRevisionEntity>(createCodeRevisionRequestDto);
        return await _codeService.CreateCodeRevision(codeRevision);
    }

    /// <inheritdoc />
    public override async Task<IActionResult> GetCodeRevisionById(Guid revisionId)
    {
        return await _codeService.GetCodeRevision(revisionId);
    }

    /// <inheritdoc />
    public override async Task<IActionResult> GetCodeRevisions()
    {
        return await _codeService.GetAllCodeRevisions();
    }

    /// <inheritdoc />
    public override async Task<IActionResult> GetLatestCode()
    {
        return await _codeService.GetLatestCode();
    }

    /// <inheritdoc />
    public override async Task<IActionResult> UpdateLatestCode(
        UpdateLatestCodeRequestDto updateLatestCodeRequestDto)
    {
        var codeRevision = _mapper.Map<CodeRevisionEntity>(updateLatestCodeRequestDto);
        return await _codeService.UpdateLatestCode(codeRevision);
    }
}