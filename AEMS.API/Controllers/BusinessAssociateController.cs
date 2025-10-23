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
/*using IMS.Domain.Migrations;
*/
namespace ZMS.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class BusinessAssociateController : BaseController<BusinessAssociateController, IBusinessAssociateService, BusinessAssociateReq, BusinessAssociateRes, BusinessAssociate>
{
    public BusinessAssociateController(ILogger<BusinessAssociateController> logger, IBusinessAssociateService service) : base(logger, service)
    {

    }
}