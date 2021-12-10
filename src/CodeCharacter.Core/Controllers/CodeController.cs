using AutoMapper;
using CodeCharacter.Core.Entities;
using CodeCharacter.Core.Exceptions;
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
        try
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            await _codeService.CreateCodeRevision(user, createCodeRevisionRequestDto.Code, null);
            return Created("", null);
        }
        catch (GenericException e)
        {
            var error = _mapper.Map<GenericErrorDto>(e);
            return BadRequest(error);
        }
    }

    /// <inheritdoc />
    public override async Task<IActionResult> GetCodeRevisionById(Guid revisionId)
    {
        try
        {
            var user = await _userManager.GetUserAsync(HttpContext.User)!;
            var codeRevisionEntity = await _codeService.GetCodeRevision(user, revisionId);
            var codeRevisionDto = _mapper.Map<CodeRevisionDto>(codeRevisionEntity);
            return Ok(codeRevisionDto);
        }
        catch (GenericException e)
        {
            var error = _mapper.Map<GenericErrorDto>(e);
            return NotFound(error);
        }
    }

    /// <inheritdoc />
    public override async Task<IActionResult> GetCodeRevisions()
    {
        var user = await _userManager.GetUserAsync(HttpContext.User)!;
        var codeRevisions = await _codeService.GetAllCodeRevisions(user);
        var codeRevisionsDto = _mapper.Map<IEnumerable<CodeRevisionDto>>(codeRevisions);
        return Ok(codeRevisionsDto);
    }

    /// <inheritdoc />
    public override async Task<IActionResult> GetLatestCode()
    {
        var user = await _userManager.GetUserAsync(HttpContext.User)!;
        var codeEntity = await _codeService.GetLatestCode(user);
        var codeDto = _mapper.Map<CodeDto>(codeEntity);
        return Ok(codeDto);
    }

    /// <inheritdoc />
    public override async Task<IActionResult> UpdateLatestCode(
        UpdateLatestCodeRequestDto updateLatestCodeRequestDto)
    {
        var user = await _userManager.GetUserAsync(HttpContext.User)!;
        await _codeService.UpdateLatestCode(user, updateLatestCodeRequestDto.Code);
        return NoContent();
    }
}