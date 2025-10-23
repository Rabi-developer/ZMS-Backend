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
using System.Net;
using ZMS.API.Middleware;
using Microsoft.AspNetCore.Authorization;

namespace ZMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PaymentController : BaseController<PaymentController, IPaymentService, PaymentReq, PaymentRes, Payment>
    {
        public PaymentController(ILogger<PaymentController> logger, IPaymentService service) : base(logger, service)
        {
        }

        [HttpPost("History")]
        [Permission("Organization", "Read")]
        public async Task<IActionResult> GetBySellerBuyer(HistoryPayment historyPayment)
        {
            var data = await Service.GetBySellerBuyer(historyPayment.Seller, historyPayment.Buyer);
            if (data.StatusCode == HttpStatusCode.OK)
            {
                return Ok(data);
            }
            return StatusCode((int)data.StatusCode, data);
        }

        [HttpPost("Status")]
        [Permission("Organization", "Update")]

        public async Task<IActionResult> UpdateStatus([FromBody] PaymentStatus paymentStatus)
        {
            try
            {
                var result = await Service.UpdateStatusAsync((Guid)paymentStatus.Id, paymentStatus.Status);
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
                return StatusCode(500, "An error occurred while updating the payment status.");
            }
        }

        [HttpGet("PaymentNumbers")]
        [Permission("Organization", "Read")]
        public async Task<IActionResult> GetPaymentNumbers()
        {
            var result = await Service.GetPaymentNumbers();
            if (result.StatusCode == HttpStatusCode.OK)
            {
                return Ok(result);
            }
            return StatusCode((int)result.StatusCode, result);
        }

        [HttpGet("ChequeNumbers/{paymentId}")]
        [Permission("Organization", "Read")]
        public async Task<IActionResult> GetChequeNumbers(Guid paymentId)
        {
            var result = await Service.GetChequeNumbers(paymentId);
            if (result.StatusCode == HttpStatusCode.OK)
            {
                return Ok(result);
            }
            return StatusCode((int)result.StatusCode, result);
        }
    }
}