﻿using IMS.API.Utilities.Auth;
using IMS.API.Base;
using IMS.Business.DTOs.Requests;
using IMS.Business.DTOs.Responses;
using IMS.Business.Services;
using IMS.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using ZMS.Domain.Entities;
using IMS.Domain.Migrations;
using ZMS.Business.DTOs.Responses;

namespace ZMS.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[AuthorizeAnyPolicy("AllAll", "AllOrganization", "ManageOrganization", "CreateOrganization")]
public class DispatchNoteController : BaseController<DispatchNoteController, IDispatchNoteService, DispatchNoteReq, DispatchNoteRes, DispatchNote>
{
    public DispatchNoteController(ILogger<DispatchNoteController> logger, IDispatchNoteService service) : base(logger, service)
    {

    }
}