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
/*using IMS.Domain.Migrations;
*/
using ZMS.Business.DTOs.Requests;
using Microsoft.AspNetCore.Authorization;

namespace ZMS.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class MunshyanaController : BaseController<MunshyanaController, IMunshyanaService, MunshyanaReq, MunshyanaRes, Munshyana>
{
    public MunshyanaController(ILogger<MunshyanaController> logger, IMunshyanaService service) : base(logger, service)
    {

    }
}