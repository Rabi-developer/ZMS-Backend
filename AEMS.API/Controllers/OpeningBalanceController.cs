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
using Microsoft.AspNetCore.Authorization;
using ZMS.API.Middleware;
/*using IMS.Domain.Migrations;
*/
namespace ZMS.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class OpeningBalanceController : BaseController<OpeningBalanceController, IOpeningBalanceService, OpeningBalanceReq, OpeningBalanceRes, OpeningBalance>
{
    public OpeningBalanceController(ILogger<OpeningBalanceController> logger, IOpeningBalanceService service) : base(logger, service)
    {

    }


}