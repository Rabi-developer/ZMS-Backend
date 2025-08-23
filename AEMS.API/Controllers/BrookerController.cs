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
/*using ZMS.Domain.Migrations;*/

namespace ZMS.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[AuthorizeAnyPolicy("AllAll", "AllOrganization", "ManageOrganization", "CreateOrganization")]
public class BrookerController : BaseController<BrookerController, IBrookerService, BrookerReq, BrookerRes, Brooker>
{
    public BrookerController(ILogger<BrookerController> logger, IBrookerService service) : base(logger, service)
    {

    }
}