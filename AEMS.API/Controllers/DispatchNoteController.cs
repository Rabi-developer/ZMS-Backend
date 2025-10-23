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
/*using IMS.Domain.Migrations;
*/
using ZMS.Business.DTOs.Responses;
using Microsoft.AspNetCore.Authorization;
using ZMS.API.Middleware;

namespace ZMS.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class DispatchNoteController : BaseController<DispatchNoteController, IDispatchNoteService, DispatchNoteReq, DispatchNoteRes, DispatchNote>
{
    public DispatchNoteController(ILogger<DispatchNoteController> logger, IDispatchNoteService service) : base(logger, service)
    {

    }


    [HttpPost("History")]
    [Permission("Organization", "Read")]
    public async Task<IActionResult> getBySellerBuyer(HistoryDispatchNote HistoryDispatchNotes)
    {
        var data = await Service.getBySellerBuyer(HistoryDispatchNotes.Seller, HistoryDispatchNotes.Buyer);
        if (data != null)
        {
            return Ok(data);
        }
        return BadRequest();
    }
    [HttpPost("Status")]
    [Permission("Organization", "Update")]

    public async Task<IActionResult> UpdateStatus([FromBody] DispatchNoteStatus dispatchnotestatus)
    {
        try
        {
            var result = await Service.UpdateStatusAsync((Guid)dispatchnotestatus.Id, dispatchnotestatus.Status);
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
            return StatusCode(500, "An error occurred while updating the contract status.");
        }
    }



}