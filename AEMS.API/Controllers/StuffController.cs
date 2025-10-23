using IMS.API.Utilities.Auth;
using IMS.API.Base;
using IMS.Business.DTOs.Requests;
using IMS.Business.DTOs.Responses;
using IMS.Business.Services;
using IMS.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace IMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class StuffController : BaseController<StuffController, IStuffService, StuffReq, StuffRes, Stuff>
    {
        public StuffController(ILogger<StuffController> logger,  IStuffService service) : base(logger, service)
        {
        }
    }
}