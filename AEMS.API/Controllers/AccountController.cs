using IMS.API.Base;
using IMS.Business.DTOs.Requests;
using IMS.Business.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace IMS.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[AllowAnonymous]
public class AccountController : BaseMinController<AccountController, IAccountService>
{
    /// <inheritdoc />
    public AccountController(ILogger<AccountController> logger, IAccountService service) : base(logger, service)
    {
    }

    [HttpPost("Login")]
    public async Task<IActionResult> Login([FromBody] LoginReq login)
    {
        var result = await Service.Login(login);
        if (result.StatusCode == HttpStatusCode.OK || result.StatusCode == HttpStatusCode.Created)
        {
            Response.Cookies.Append("AuthToken", result.Data.Token, new CookieOptions
            {
                HttpOnly = true,   // Ensures the cookie is not accessible via JavaScript
                Secure = true,     // Ensures the cookie is only sent over HTTPS
                Expires = DateTimeOffset.UtcNow.AddHours(1), // Expiration time of the cookie
                SameSite = SameSiteMode.Strict // SameSite cookie policy for security
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
}