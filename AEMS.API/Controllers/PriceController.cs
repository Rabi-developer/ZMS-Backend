using IMS.API.Utilities.Auth;
using IMS.API.Base;
using IMS.Business.DTOs.Requests;
using IMS.Business.DTOs.Responses;
using IMS.Business.Services;
using IMS.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace IMS.API.Controllers;

[Route("api/[controller]")]
[AllowAnonymous]
public class PriceController : BaseController<PriceController, IPriceService, PriceReq, PriceRes, Price>
{
    /// <inheritdoc />
    public PriceController(ILogger<PriceController> logger, IPriceService service) : base(logger, service)
    {
    }


}