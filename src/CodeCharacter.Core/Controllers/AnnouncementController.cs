using AutoMapper;
using CodeCharacter.Core.Entities;
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
        return Ok();
    }

    /// <inheritdoc />
    public override async Task<IActionResult> DeleteAnnouncementById(int announcementId)
    {
        await _announcementService.DeleteAnnouncement(announcementId);
        return Ok();
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
        var announcement = await _announcementService.GetAnnouncement(announcementId);
        var announcementDto = _mapper.Map<AnnouncementDto>(announcement);
        return Ok(announcementDto);
    }

    /// <inheritdoc />
    public override async Task<IActionResult> UpdateAnnouncementById(int announcementId,
        UpdateAnnouncementRequestDto updateAnnouncementRequestDto)
    {
        await _announcementService.UpdateAnnouncement(announcementId, updateAnnouncementRequestDto.Message);
        return Ok();
    }
}