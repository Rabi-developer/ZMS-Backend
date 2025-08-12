/*using IMS.API.Utilities.Auth;
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
using IMS.Domain.Migrations;
using ZMS.Business.DTOs.Responses;
using IMS.Domain.Utilities;

namespace ZMS.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[AuthorizeAnyPolicy("AllAll", "AllOrganization", "ManageOrganization", "CreateOrganization")]
public class CommisionInvoiceController : BaseController<CommisionInvoiceController, ICommisionInvoiceService, CommisionInvoiceReq, CommisionInvoiceRes, CommisionInvoice>
{
    public CommisionInvoiceController(ILogger<CommisionInvoiceController> logger, ICommisionInvoiceService service) : base(logger, service)
    {
    }

    [HttpPost("status")]
    public async Task<IActionResult> UpdateStatus([FromBody] CommisionInvoiceStatus CommisionInvoicestatus)
    {
        try
        {
            var result = await Service.UpdateStatusAsync((Guid)CommisionInvoicestatus.Id, CommisionInvoicestatus.Status);
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
            return StatusCode(500, "An error occurred while updating the CommisionInvoice status.");
        }
    }

    [HttpGet("history")]
    public async Task<IActionResult> GetHistoryData([FromQuery] string? seller, [FromQuery] string? buyer, [FromQuery] Pagination? pagination)
    {
        try
        {
            var result = await Service.GetHistoryData(seller, buyer, pagination);
            if (result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return Ok(result);
            }
            return StatusCode((int)result.StatusCode, result.StatusMessage);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "An error occurred while fetching history data: " + ex.Message);
        }
    }
}*/