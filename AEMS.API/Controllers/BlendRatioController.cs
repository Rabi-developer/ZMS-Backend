using IMS.API.Utilities.Auth;
using IMS.API.Base;
using IMS.Business.DTOs.Requests;
using IMS.Business.DTOs.Responses;
using IMS.Business.Services;
using IMS.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using IMS.Domain.Migrations.Entities;

namespace IMS.API.Controllers;

[Route("api/[controller]")]
[AllowAnonymous]
public class BlendRatioController : BaseController<BlendRatioController, IBlendRatioService, BlendRatioReq, BlendRatioRes, BlendRatio>
{
    /// <inheritdoc />
    public BlendRatioController(ILogger<BlendRatioController> logger, IBlendRatioService service) : base(logger, service)
    {
    }


}