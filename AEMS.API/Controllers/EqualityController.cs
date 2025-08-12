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
public class EqualityController : BaseController<EqualityController, IEqualityService, EqualityReq, EqualityRes, Equality>
{
    /// <inheritdoc />
    public EqualityController(ILogger<EqualityController> logger, IEqualityService service) : base(logger, service)
    {
    }
    [HttpGet("Parent/{ParentId}")]

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