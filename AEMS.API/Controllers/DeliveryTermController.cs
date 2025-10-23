using IMS.API.Utilities.Auth;
using IMS.API.Base;
using IMS.Business.DTOs.Requests;
using IMS.Business.DTOs.Responses;
using IMS.Business.Services;
using IMS.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using ZMS.Business.DTOs.Requests;
using ZMS.Business.DTOs.Responses;
using ZMS.Domain.Entities;
using Microsoft.AspNetCore.Authorization;

namespace ZMS.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]

public class DeliveryTermController : BaseController<DeliveryTermController, IDeliveryTermService, DeliveryTermReq, DeliveryTermRes, DeliveryTerm>
{
    /// <inheritdoc />
    public DeliveryTermController(ILogger<DeliveryTermController> logger, IDeliveryTermService service) : base(logger, service)
    {
    }
}