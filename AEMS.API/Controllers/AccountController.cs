using IMS.API.Base;
using ZMS.API.Middleware;
using IMS.Business.DTOs.Requests;
using IMS.Business.DTOs.Responses;
using IMS.Business.Services;
using IMS.Domain.Utilities;
using IMS.API.Base;
using IMS.Business.DTOs.Requests;
using IMS.Business.Services;
using IMS.Domain.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using ZMS.API.Middleware;

namespace IMS.API.Controllers;

[Route("api/[controller]")]
[ApiController]


public class AccountController : BaseMinController<AccountController, IAccountService>
{
    private readonly IRoleService _roleService;

    public AccountController(
        ILogger<AccountController> logger,
        IAccountService service,
        IRoleService roleService) : base(logger, service)
    {
        _roleService = roleService;
    }

    [HttpPost("Login")]
    public async Task<IActionResult> Login([FromBody] LoginReq login)
    {
        var result = await Service.Login(login);
        if (result.StatusCode == HttpStatusCode.OK || result.StatusCode == HttpStatusCode.Created)
        {
            Response.Cookies.Append("AuthToken", result.Data.Token, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                Expires = DateTimeOffset.UtcNow.AddHours(1),
                SameSite = SameSiteMode.Strict
            });
            return Ok(result);
        }
        else if (result.StatusCode == HttpStatusCode.Unauthorized)
        {
            return Unauthorized(result);
        }
        else
        {
            return BadRequest(result);
        }
    }

    [HttpPost("Signup")]
    public async Task<IActionResult> Signup([FromBody] SignUpReq signup)
    {
        var result = await Service.Register(signup);
        if (result.StatusCode == HttpStatusCode.OK || result.StatusCode == HttpStatusCode.Created)
        {
            return Ok(result);
        }
        else
        {
            return BadRequest(result);
        }
    }


    [HttpPost("CreateUserWithRole")]
    [Authorize]
    [Permission("User", "Create")]
    public async Task<IActionResult> CreateUserWithRole([FromBody] CreateUserWithRoleReq request)
    {
        var result = await Service.CreateWithRole(request);
        return StatusCode((int)result.StatusCode, result);
    }

    [HttpPut("UpdateUser")]
    [Authorize]
    [Permission("User", "Update")]
    public async Task<IActionResult> UpdateUser([FromBody] UpdateUserReq request)
    {
        var result = await Service.UpdateApplicationUser(request);
        return StatusCode((int)result.StatusCode, result);
    }

    [HttpGet("CheckPermission/{userId}/{resource}/{action}")]
    [AllowAnonymous]
    public async Task<IActionResult> CheckPermission(string userId, string resource, string action)
    {
        try
        {
            var hasPermission = await _roleService.UserHasPermission(userId, resource, action);
            return Ok(new { HasPermission = hasPermission });
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { Message = ex.Message });
        }
    }

    [HttpGet]
    [Authorize]
    [Permission("User", "Read")]
    public async Task<IActionResult> GetAll([FromQuery] Pagination pagination)
    {
        var result = await Service.GetAll(pagination);
        if (result.StatusCode == HttpStatusCode.OK || result.StatusCode == HttpStatusCode.Created)
        {
            return Ok(result);
        }
        else
        {
            return BadRequest(result);
        }
    }
}

