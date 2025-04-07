using IMS.API.Utilities.Auth;
using IMS.API.Base;
using IMS.Business.DTOs.Requests;
using IMS.Business.DTOs.Responses;
using IMS.Business.Services;
using IMS.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using IMS.Domain.Migrations.Entities;

namespace IMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AuthorizeAnyPolicy("AllAll", "AllOrganization", "ManageOrganization", "CreateOrganization")]
    public class StuffController : BaseController<StuffController, IStuffService, StuffReq, StuffRes, Stuff>
    {
        public StuffController(ILogger<StuffController> logger,  IStuffService service) : base(logger, service)
        {
        }
    }
}