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
using IMS.Business.Utitlity;

namespace ZMS.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[AuthorizeAnyPolicy("AllAll", "AllOrganization", "ManageOrganization", "CreateOrganization")]
public class BookingOrderController : BaseController<BookingOrderController, IBookingOrderService, BookingOrderReq, BookingOrderRes, BookingOrder>
{
    public BookingOrderController(ILogger<BookingOrderController> logger, IBookingOrderService service) : base(logger, service)
    {
    }

    [HttpPost("status")]
    public async Task<IActionResult> UpdateStatus([FromBody] BookingOrderStatus contractstatus)
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

    [HttpGet("{bookingOrderId}/consignments")]
    public async Task<IActionResult> GetConsignmentsByBookingOrderId(Guid bookingOrderId)
    {
        try
        {
            var result = await Service.GetConsignmentsByBookingOrderIdAsync(bookingOrderId);
            return StatusCode((int)result.StatusCode, result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new Response<IList<RelatedConsignmentRes>>
            {
                StatusMessage = ex.InnerException != null ? ex.InnerException.Message : ex.Message,
                StatusCode = System.Net.HttpStatusCode.InternalServerError
            });
        }
    }

    [HttpPost("{bookingOrderId}/consignments")]
    public async Task<IActionResult> AddConsignment(Guid bookingOrderId, [FromBody] RelatedConsignmentReq reqModel)
    {
        try
        {
            var result = await Service.AddConsignmentAsync(bookingOrderId, reqModel);
            return StatusCode((int)result.StatusCode, result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new Response<Guid>
            {
                StatusMessage = ex.InnerException != null ? ex.InnerException.Message : ex.Message,
                StatusCode = System.Net.HttpStatusCode.InternalServerError
            });
        }
    }

    [HttpPut("{bookingOrderId}/consignments/{consignmentId}")]
    public async Task<IActionResult> UpdateConsignment(Guid bookingOrderId, Guid consignmentId, [FromBody] RelatedConsignmentReq reqModel)
    {
        try
        {
            var result = await Service.UpdateConsignmentAsync(bookingOrderId, consignmentId, reqModel);
            return StatusCode((int)result.StatusCode, result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new Response<Guid>
            {
                StatusMessage = ex.InnerException != null ? ex.InnerException.Message : ex.Message,
                StatusCode = System.Net.HttpStatusCode.InternalServerError
            });
        }
    }

    [HttpDelete("{bookingOrderId}/consignments/{consignmentId}")]
    public async Task<IActionResult> DeleteConsignment(Guid bookingOrderId, Guid consignmentId)
    {
        try
        {
            var result = await Service.DeleteConsignmentAsync(bookingOrderId, consignmentId);
            return StatusCode((int)result.StatusCode, result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new Response<bool>
            {
                StatusMessage = ex.InnerException != null ? ex.InnerException.Message : ex.Message,
                StatusCode = System.Net.HttpStatusCode.InternalServerError
            });
        }
    }
}