﻿using IMS.API.Utilities.Auth;
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
public class CapitalAccountController : BaseController<CapitalAccountController, ICapitalAccountService, CapitalAccountReq, CapitalAccountRes, CapitalAccount>
{
    /// <inheritdoc />
    public CapitalAccountController(ILogger<CapitalAccountController> logger, ICapitalAccountService service) : base(logger, service)
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
    [HttpGet("allhierarchy")]
    public async Task<IActionResult> GetAllHierarchy()
    {
        var response = await Service.GetAllHierarchy();
        return StatusCode((int)response.StatusCode, response);
    }
}