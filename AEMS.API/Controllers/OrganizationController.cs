using IMS.API.Utilities.Auth;
using IMS.API.Base;
using IMS.Business.DTOs.Requests;
using IMS.Business.Services;
using IMS.Domain.Utilities;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Claims;

namespace IMS.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrganizationController : BaseMinController<OrganizationController, IOrganizationService>
{
    /// <inheritdoc />
    public OrganizationController(ILogger<OrganizationController> logger, IOrganizationService service) : base(logger, service)
    {
    }

    [HttpPost]
    [AuthorizeAnyPolicy("AllAll", "AllOrganization", "ManageOrganization", "CreateOrganization")]
    public async Task<IActionResult> Post([FromBody] OrganizationReq req)
    {
        string name = User.Identity.Name;
        string Id = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier || x.Type == "Id")?.Value;
        var result = await Service.Add(req, Id);
        if (result.StatusCode == HttpStatusCode.OK || result.StatusCode == HttpStatusCode.Created)
        {
            return Ok(result);
        }
        else
        {
            return BadRequest(result);
        }
    }

    [HttpGet]
    [AuthorizeAnyPolicy("AllAll", "AllOrganization", "ManageOrganization", "ReadOrganization")]
    public async Task<IActionResult> Get([FromQuery] Pagination pagination)
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

    [HttpGet("{id:guid}")]
    [AuthorizeAnyPolicy("AllAll", "AllOrganization", "ManageOrganization", "ReadOrganization")]
    public async Task<IActionResult> Get(Guid id)
    {
        var result = await Service.Get(id);
        if (result.StatusCode == HttpStatusCode.OK || result.StatusCode == HttpStatusCode.Created)
        {
            return Ok(result);
        }
        else
        {
            return BadRequest(result);
        }
    }

    [HttpPut]
    [AuthorizeAnyPolicy("AllAll", "AllOrganization", "ManageOrganization", "ReadOrganization")]
    public async Task<IActionResult> Put([FromBody] OrganizationUpdateReq req)
    {
        var result = await Service.Update(req);
        if (result.StatusCode == HttpStatusCode.OK || result.StatusCode == HttpStatusCode.Created)
        {
            return Ok(result);
        }
        else
        {
            return BadRequest(result);
        }
    }

    [HttpDelete("{id:guid}")]
    [AuthorizeAnyPolicy("AllAll", "AllOrganization", "ManageOrganization", "ReadOrganization")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await Service.Delete(id);
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