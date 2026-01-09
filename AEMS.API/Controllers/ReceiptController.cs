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
using System.Net;
/*using IMS.Domain.Migrations;
*/
namespace ZMS.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class ReceiptController : BaseController<ReceiptController, IReceiptService, ReceiptReq, ReceiptRes, Receipt>
{
    public ReceiptController(ILogger<ReceiptController> logger, IReceiptService service) : base(logger, service)
    {

    }

    [HttpPost("status")]
    [Permission("Organization", "Update")]

    public async Task<IActionResult> UpdateStatus([FromBody] ReceiptStatus contractstatus)
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

    [HttpGet("bilty-balance/{biltyNo}")]
    [Permission("Organization", "View")]
    public async Task<IActionResult> GetBiltyBalance(string biltyNo)
    {
        try
        {
            var result = await Service.GetBiltyBalance(biltyNo);
            if (result.StatusCode == HttpStatusCode.OK)
                return Ok(result.Data);
            if (result.StatusCode == HttpStatusCode.BadRequest)
                return BadRequest(result.StatusMessage);
            return StatusCode(500, result.StatusMessage);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
}