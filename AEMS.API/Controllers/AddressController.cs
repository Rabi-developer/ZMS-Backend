﻿using IMS.API.Utilities.Auth;
using IMS.API.Base;
using IMS.Business.DTOs.Requests;
using IMS.Business.DTOs.Responses;
using IMS.Business.Services;
using IMS.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace IMS.API.Controllers;

[Route("api/[controller]")]
[AuthorizeAnyPolicy("AllAll", "AllOrganization", "ManageOrganization", "CreateOrganization")]
public class AddressController : BaseController<AddressController, IAddressService, AddressReq, AddressRes, Address>
{
    /// <inheritdoc />
    public AddressController(ILogger<AddressController> logger, IAddressService service) : base(logger, service)
    {
    }


}