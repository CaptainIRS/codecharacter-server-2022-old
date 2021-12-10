using AutoMapper;
using CodeCharacter.Core.Exceptions;
using CodeCharacter.Core.Interfaces;
using CodeCharacter.CoreLibrary.Controllers;
using CodeCharacter.CoreLibrary.Models;
using Microsoft.AspNetCore.Mvc;

namespace CodeCharacter.Core.Controllers;

/// <inheritdoc />
public class AnnouncementController : AnnouncementApiController
{
    private readonly IAnnouncementService _announcementService;
    private readonly IMapper _mapper;

    /// <summary>
    ///     Constructor
    /// </summary>
    /// <param name="announcementService"></param>
    /// <param name="mapper"></param>
    public AnnouncementController(IAnnouncementService announcementService, IMapper mapper)
    {
        _announcementService = announcementService;
        _mapper = mapper;
    }

    /// <inheritdoc />
    public override async Task<IActionResult> CreateAnnouncement(
        CreateAnnouncementRequestDto createAnnouncementRequestDto)
    {
        await _announcementService.CreateAnnouncement(createAnnouncementRequestDto.Message);
        return Created("", null);
    }

    /// <inheritdoc />
    public override async Task<IActionResult> DeleteAnnouncementById(int announcementId)
    {
        try
        {
            await _announcementService.DeleteAnnouncement(announcementId);
            return Ok();
        }
        catch (GenericException e)
        {
            var error = _mapper.Map<GenericErrorDto>(e);
            return NotFound(error);
        }
    }

    /// <inheritdoc />
    public override async Task<IActionResult> GetAllAnnouncements()
    {
        var announcements = await _announcementService.GetAllAnnouncements();
        var announcementDtos = _mapper.Map<IEnumerable<AnnouncementDto>>(announcements);
        return Ok(announcementDtos);
    }

    /// <inheritdoc />
    public override async Task<IActionResult> GetAnnouncementById(int announcementId)
    {
        try
        {
            var announcement = await _announcementService.GetAnnouncement(announcementId);
            var announcementDto = _mapper.Map<AnnouncementDto>(announcement);
            return Ok(announcementDto);
        }
        catch (GenericException e)
        {
            var error = _mapper.Map<GenericErrorDto>(e);
            return NotFound(error);
        }
    }

    /// <inheritdoc />
    public override async Task<IActionResult> UpdateAnnouncementById(int announcementId,
        UpdateAnnouncementRequestDto updateAnnouncementRequestDto)
    {
        try
        {
            await _announcementService.UpdateAnnouncement(announcementId, updateAnnouncementRequestDto.Message);
            return Ok();
        }
        catch (GenericException e)
        {
            var error = _mapper.Map<GenericErrorDto>(e);
            return NotFound(error);
        }
    }
}