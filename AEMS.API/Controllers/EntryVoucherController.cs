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
using ZMS.Business.DTOs.Requests;
/*using IMS.Domain.Migrations;
*/
namespace ZMS.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[AuthorizeAnyPolicy("AllAll", "AllOrganization", "ManageOrganization", "CreateOrganization")]
public class EntryVoucherController : BaseController<EntryVoucherController, IEntryVoucherService, EntryVoucherReq, EntryVoucherRes, EntryVoucher>
{
    public EntryVoucherController(ILogger<EntryVoucherController> logger, IEntryVoucherService service) : base(logger, service)
    {

    }

    [HttpPost("status")]
    public async Task<IActionResult> UpdateStatus([FromBody] EntryVoucherStatus contractstatus)
    {
        try
        {
            var result = await Service.UpdateStatusAsync((Guid)contractstatus.Id, contractstatus.Status);
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