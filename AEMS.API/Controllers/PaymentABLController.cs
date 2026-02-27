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
public class PaymentABLController : BaseController<PaymentABLController, IPaymentABLService, PaymentABLReq, PaymentABLRes, PaymentABL>
{
    public PaymentABLController(ILogger<PaymentABLController> logger, IPaymentABLService service) : base(logger, service)
    {

    }

    [HttpPost("status")]
    [Permission("Organization", "Update")]
    public async Task<IActionResult> UpdateStatus([FromBody] PaymentAblStatus contractstatus)
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
    [HttpGet("History")]
    [Permission("Organization", "Read")]

    public async Task<IActionResult> GetHistory(string VehicleNo, string OrderNo, string Charges, bool IsOpeningBalance = false) 
    {
        var result = await Service.HistoryPayment(VehicleNo , OrderNo, Charges, IsOpeningBalance);
       
            return Ok(result);
       
    }

   
}