using IMS.API.Utilities.Auth;
using IMS.API.Base;
using ZMS.Business.DTOs.Requests;
using ZMS.Business.DTOs.Responses;
using IMS.Business.Services;
using IMS.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using ZMS.Domain.Entities;
using ZMS.Business.DTOs.Requests;
using IMS.Business.Utitlity;
using Microsoft.AspNetCore.Authorization;
using ZMS.API.Middleware;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using System.Net;

namespace ZMS.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class BookingOrderController : BaseController<BookingOrderController, IBookingOrderService, BookingOrderReq, BookingOrderRes, BookingOrder>
{
    public BookingOrderController(ILogger<BookingOrderController> logger, IBookingOrderService service) : base(logger, service)
    {
    }

    [HttpPost("status")]
    [Permission("Organization", "Update")]
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
    [Permission("Organization", "Read")]
    public async Task<IActionResult> GetConsignmentsByBookingOrderId(Guid bookingOrderId, [FromQuery] bool includeDetails = false)
    {
        try
        {
            var result = await Service.GetConsignmentsByBookingOrderIdAsync(bookingOrderId, includeDetails);
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
    [Permission("Organization", "Create")]
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
    [Permission("Organization", "Update")]
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
    [Permission("Organization", "Delete")]
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

    /* [HttpPatch("{id}")]*/
    /*  public async Task<IActionResult> PartialUpdateBookingOrder(Guid id, [FromBody] JsonElement body)
      {
          try
          {
              // 1. Get current entity from DB
              var currentEntity = await _dbContext.BookingOrder.FindAsync(id);
              if (currentEntity == null)
                  return NotFound($"BookingOrder with ID {id} not found");

              // 2. Convert current entity → DTO
              var currentDto = currentEntity.Adapt<BookingOrderReq>();
              currentDto.Id = id;

              // 3. Apply only the fields from the PATCH body
              if (body.TryGetProperty("files", out var filesProp))
              {
                  currentDto.Files = filesProp.GetString();
              }
              // You can add more partial fields later:
              // if (body.TryGetProperty("status", out var statusProp)) currentDto.Status = ...

              // 4. Call your existing UpdateAsync (reuses all logic!)
              var result = await _bookingOrderService.UpdateAsync(id, currentDto);

              if (result.StatusCode != HttpStatusCode.OK)
                  return StatusCode((int)result.StatusCode, result);

              return Ok(new
              {
                  message = "Files updated successfully",
                  files = result.Data?.Files
              });
          }
          catch (Exception ex)
          {
              return StatusCode(500, new { message = "Failed to update files", error = ex.Message });
          }
      }

  [HttpPut]
public async Task<IActionResult> UpdateBookingOrder([FromBody] BookingOrderReq request)
{
  if (!ModelState.IsValid)
      return BadRequest(ModelState);

  var result = await _bookingOrderService.UpdateAsync(request.Id, request);
  return Ok(result);
}*/
   
    }