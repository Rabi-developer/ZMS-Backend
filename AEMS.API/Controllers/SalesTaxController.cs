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
public class SalesTaxController : BaseController<SalesTaxController, ISalesTaxService, SalesTaxReq, SalesTaxRes, SalesTax>
{
    public SalesTaxController(ILogger<SalesTaxController> logger, ISalesTaxService service) : base(logger, service)
    {

    }
}