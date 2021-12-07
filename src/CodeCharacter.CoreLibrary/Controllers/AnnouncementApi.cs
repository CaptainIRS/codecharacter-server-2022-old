/*
 * CodeCharacter API
 *
 * Specification of the CodeCharacter API
 *
 * The version of the OpenAPI document: 2022.0.1
 * Contact: delta@nitt.edu
 * Generated by: https://openapi-generator.tech
 */

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

using CodeCharacter.CoreLibrary.Attributes;
using CodeCharacter.CoreLibrary.Models;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CodeCharacter.CoreLibrary.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [ApiController]
    public abstract class AnnouncementApiController : ControllerBase
    {
        /// <summary>
        /// Create announcement
        /// </summary>
        /// <remarks>Create announcement</remarks>
        /// <param name="createAnnouncementRequestDto"></param>
        /// <response code="201">Created</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">Forbidden</response>
        [HttpPost]
        [Route("/announcements")]
        [Authorize]
        [Consumes("application/json")]
        [ValidateModelState]
        public abstract Task<IActionResult> CreateAnnouncement([FromBody] CreateAnnouncementRequestDto createAnnouncementRequestDto);

        /// <summary>
        /// Delete announcement by ID
        /// </summary>
        /// <remarks>Delete announcement by ID</remarks>
        /// <param name="announcementId">ID of the announcement</param>
        /// <response code="204">No Content</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">Not Found</response>
        [HttpDelete]
        [Route("/announcements/{announcementId}")]
        [Authorize]
        [ValidateModelState]
        public abstract Task<IActionResult> DeleteAnnouncementById([FromRoute(Name = "announcementId")][Required] int announcementId);

        /// <summary>
        /// Get all announcements
        /// </summary>
        /// <remarks>Get all announcements</remarks>
        /// <response code="200">OK</response>
        /// <response code="401">Unauthorized</response>
        [HttpGet]
        [Route("/announcements")]
        [Authorize]
        [ValidateModelState]
        [ProducesResponseType(statusCode: 200, type: typeof(List<AnnouncementDto>))]
        public abstract Task<IActionResult> GetAllAnnouncements();

        /// <summary>
        /// Get announcement by ID
        /// </summary>
        /// <remarks>Get announcement by ID</remarks>
        /// <param name="announcementId">ID of the announcement</param>
        /// <response code="200">OK</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="404">Not Found</response>
        [HttpGet]
        [Route("/announcements/{announcementId}")]
        [Authorize]
        [ValidateModelState]
        [ProducesResponseType(statusCode: 200, type: typeof(AnnouncementDto))]
        public abstract Task<IActionResult> GetAnnouncementById([FromRoute(Name = "announcementId")][Required] int announcementId);

        /// <summary>
        /// Update announcement by ID
        /// </summary>
        /// <remarks>Update announcement by ID</remarks>
        /// <param name="announcementId">ID of the announcement</param>
        /// <param name="updateAnnouncementRequestDto"></param>
        /// <response code="204">No Content</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">Not Found</response>
        [HttpPatch]
        [Route("/announcements/{announcementId}")]
        [Authorize]
        [Consumes("application/json")]
        [ValidateModelState]
        public abstract Task<IActionResult> UpdateAnnouncementById([FromRoute(Name = "announcementId")][Required] int announcementId, [FromBody] UpdateAnnouncementRequestDto updateAnnouncementRequestDto);
    }
}
