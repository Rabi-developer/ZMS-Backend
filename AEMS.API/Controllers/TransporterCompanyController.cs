﻿using IMS.API.Utilities.Auth;
using IMS.API.Base;
using IMS.Business.DTOs.Requests;
using IMS.Business.DTOs.Responses;
using IMS.Business.Services;
using IMS.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using ZMS.Business.DTOs.Requests;
using ZMS.Business.DTOs.Responses;
using ZMS.Domain.Entities;

namespace ZMS.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[AuthorizeAnyPolicy("AllAll", "AllOrganization", "ManageOrganization", "CreateOrganization")]
public class TransporterCompanyController : BaseController<TransporterCompanyController, ITransporterCompanyService, TransporterCompanyReq, TransporterCompanyRes, TransporterCompany>
{
    /// <inheritdoc />
    public TransporterCompanyController(ILogger<TransporterCompanyController> logger, ITransporterCompanyService service) : base(logger, service)
    {
    }
}