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
public class BuyerController : BaseController<BuyerController, IBuyerService, BuyerReq, BuyerRes, Buyer>
{
    /// <inheritdoc />
    public BuyerController(ILogger<BuyerController> logger, IBuyerService service) : base(logger, service)
    {
    }

}