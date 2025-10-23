using IMS.API.Utilities.Auth;
using IMS.API.Base;
using IMS.Business.DTOs.Requests;
using IMS.Business.DTOs.Responses;
using IMS.Business.Services;
using IMS.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using ZMS.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using ZMS.API.Middleware;
/*using IMS.Domain.Migrations;
*/
namespace ZMS.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class InspectionNoteController : BaseController<InspectionNoteController, IInspectionNoteService, InspectionNoteReq, InspectionNoteRes, InspectionNote>
{
    public InspectionNoteController(ILogger<InspectionNoteController> logger, IInspectionNoteService service) : base(logger, service)
    {
    }

    [HttpPost("status")]
    [Permission("Organization", "Update")]

    public async Task<IActionResult> UpdateStatus([FromBody] InspectionNoteStatus InspectionNotestatus)
    {
        try
        {
            var result = await Service.UpdateStatusAsync((Guid)InspectionNotestatus.Id, InspectionNotestatus.Status);
            return Ok(result);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "An error occurred while updating the InspectionNote status.");
        }
    }
}