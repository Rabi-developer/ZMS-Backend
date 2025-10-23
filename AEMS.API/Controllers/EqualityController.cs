using IMS.API.Utilities.Auth;
using IMS.API.Base;
using IMS.Business.DTOs.Requests;
using IMS.Business.DTOs.Responses;
using IMS.Business.Services;
using IMS.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using ZMS.API.Middleware;

namespace IMS.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class EqualityController : BaseController<EqualityController, IEqualityService, EqualityReq, EqualityRes, Equality>
{
    /// <inheritdoc />
    public EqualityController(ILogger<EqualityController> logger, IEqualityService service) : base(logger, service)
    {
    }
    [HttpGet("Parent/{ParentId}")]
    [Permission("Organization", "Read")]

    public async Task<IActionResult> getByParentId(Guid ParentId)
    {
        var data = await Service.GetByParent(ParentId);
        if (data != null)
        {
            return Ok(data);
        }
        return BadRequest();
    }
}