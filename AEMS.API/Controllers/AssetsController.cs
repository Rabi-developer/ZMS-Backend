using IMS.API.Utilities.Auth;
using IMS.API.Base;
using IMS.Business.DTOs.Requests;
using IMS.Business.DTOs.Responses;
using IMS.Business.Services;
using IMS.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using ZMS.API.Middleware;
using Microsoft.AspNetCore.Authorization;

namespace IMS.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class AssetsController : BaseController<AssetsController, IAssetsService, AssetsReq, AssetsRes, Assets>
{
    /// <inheritdoc />
    public AssetsController(ILogger<AssetsController> logger, IAssetsService service) : base(logger, service)
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