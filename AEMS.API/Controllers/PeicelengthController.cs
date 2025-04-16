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
public class PeicelengthController : BaseController<PeicelengthController, IPeicelengthService, PeicelengthReq, PeicelengthRes, Peicelength>
{
    /// <inheritdoc />
    public PeicelengthController(ILogger<PeicelengthController> logger, IPeicelengthService service) : base(logger, service)
    {
    }
}