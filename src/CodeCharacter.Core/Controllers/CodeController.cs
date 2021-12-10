using AutoMapper;
using CodeCharacter.Core.Entities;
using CodeCharacter.Core.Interfaces;
using CodeCharacter.CoreLibrary.Controllers;
using CodeCharacter.CoreLibrary.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CodeCharacter.Core.Controllers;

/// <inheritdoc />
public class CodeController : CodeApiController
{
    private readonly ICodeService _codeService;
    private readonly IMapper _mapper;
    private readonly UserManager<UserEntity> _userManager;

    /// <summary>
    ///     Constructor
    /// </summary>
    /// <param name="codeService"></param>
    /// <param name="userManager"></param>
    /// <param name="mapper"></param>
    public CodeController(ICodeService codeService, UserManager<UserEntity> userManager, IMapper mapper)
    {
        _codeService = codeService;
        _userManager = userManager;
        _mapper = mapper;
    }

    /// <inheritdoc />
    public override async Task<IActionResult> CreateCodeRevision(
        CreateCodeRevisionRequestDto createCodeRevisionRequestDto)
    {
        var user = await _userManager.GetUserAsync(HttpContext.User)!;
        await _codeService.CreateCodeRevision(user, createCodeRevisionRequestDto.Code, null);
        return Ok();
    }

    /// <inheritdoc />
    public override async Task<IActionResult> GetCodeRevisionById(Guid revisionId)
    {
        var user = await _userManager.GetUserAsync(HttpContext.User)!;
        await _codeService.GetCodeRevision(user, revisionId);
        return Ok();
    }

    /// <inheritdoc />
    public override async Task<IActionResult> GetCodeRevisions()
    {
        var user = await _userManager.GetUserAsync(HttpContext.User)!;
        await _codeService.GetAllCodeRevisions(user);
        return Ok();
    }

    /// <inheritdoc />
    public override async Task<IActionResult> GetLatestCode()
    {
        var user = await _userManager.GetUserAsync(HttpContext.User)!;
        await _codeService.GetLatestCode(user);
        return Ok();
    }

    /// <inheritdoc />
    public override async Task<IActionResult> UpdateLatestCode(
        UpdateLatestCodeRequestDto updateLatestCodeRequestDto)
    {
        var user = await _userManager.GetUserAsync(HttpContext.User)!;
        await _codeService.UpdateLatestCode(user, updateLatestCodeRequestDto.Code);
        return Ok();
    }
}