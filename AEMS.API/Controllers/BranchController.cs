using IMS.API.Utilities.Auth;
using IMS.API.Base;
using IMS.API.Utilities.Auth;
using IMS.Business.DTOs.Requests;
using IMS.Business.DTOs.Responses;
using IMS.Business.Services;
using IMS.Domain.Entities;
using IMS.Domain.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using ZMS.API.Middleware;

namespace IMS.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class BranchController : BaseController<BranchController, IBranchService, BranchReq, BranchRes, Branch>
{
    /// <inheritdoc />
    public BranchController(ILogger<BranchController> logger, IBranchService service) : base(logger, service)
    {
    }

    [HttpGet]
    [Permission("Organization", "Read")]
    public async override Task<IActionResult> Get([FromQuery] Pagination pagination)
    {
        bool isSuperAdmin = User.IsInRole("SuperAdmin");
        var userId = User.GetUserId();
        pagination.RefId = userId.ToString();
        var result = await Service.GetAll(pagination, !isSuperAdmin);
        if (result.StatusCode == HttpStatusCode.OK || result.StatusCode == HttpStatusCode.Created)
        {
            return Ok(result);
        }
        else
        {
            return BadRequest(result);
        }
    }

    #region BranchSettings
    [HttpPost("Setting")]
    [Permission("Organization", "Update")]
    public async Task<IActionResult> UpdateSetting([FromForm] BranchSettingReq req)
    {
        var result = await Service.SaveSetting(req);
        if (result.StatusCode == HttpStatusCode.OK || result.StatusCode == HttpStatusCode.Created)
        {
            return Ok(result);
        }
        else
        {
            return BadRequest(result);
        }
    }

    [HttpGet("Setting/{id:guid}")]
    [Permission("Organization", "Read")]
    public async Task<IActionResult> GetSetting(Guid id)
    {
        var result = await Service.GetSetting(id);
        if (result.StatusCode == HttpStatusCode.OK)
        {
            return Ok(result);
        }
        else
        {
            return BadRequest(result);
        }
    }

    [HttpGet("logo/{id:guid}")]
    [AllowAnonymous]
    [Produces("image/jpeg")]
    [Permission("Organization", "Read")]
    public async Task<MemoryStream> Get(Guid id)
    {
        MemoryStream s = new MemoryStream();
        s = await Service.GetLogo(id);
        s.Position = 0;
        return s;
    }
    #endregion
}