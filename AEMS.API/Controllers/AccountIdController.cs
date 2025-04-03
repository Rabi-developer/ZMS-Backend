using IMS.API.Utilities.Auth;
using IMS.API.Base;
using IMS.Business.DTOs.Requests;
using IMS.Business.DTOs.Responses;
using IMS.Business.Services;
using IMS.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace IMS.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[AuthorizeAnyPolicy("AllAll", "AllOrganization", "ManageOrganization", "CreateOrganization")]
public class AccountIdController : BaseController<AccountIdController, IAccountIdService, AccountIdReq, AccountIdRes, AccountId>
{
    /// <inheritdoc />
    public AccountIdController(ILogger<AccountIdController> logger, IAccountIdService service) : base(logger, service)
    {
    }
    [HttpGet("Parent/{parentId}")]
    public async Task<IActionResult> GetByParentId(Guid parentId)
    {
        var response = await Service.GetByParent(parentId);
        return StatusCode((int)response.StatusCode, response);
    }

    [HttpGet("allhierarchy")]
    public async Task<IActionResult> GetAllHierarchy()
    {
        var response = await Service.GetHierarchy();
        return StatusCode((int)response.StatusCode, response);
    }
    [HttpGet("search/{name}")]
    public async Task<IActionResult> SearchAccounts(string name)
    {
        var response = await Service.SearchAccounts(name);
        return StatusCode((int)response.StatusCode, response);
    }
}
