using IMS.API.Utilities.Auth;
using IMS.API.Base;
using IMS.Business.DTOs.Requests;
using IMS.Business.DTOs.Responses;
using IMS.Business.Services;
using IMS.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace IMS.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class PickInsertionController : BaseController<PickInsertionController, IPickInsertionService, PickInsertionReq, PickInsertionRes, PickInsertion>
{
    /// <inheritdoc />
    public PickInsertionController(ILogger<PickInsertionController> logger, IPickInsertionService service) : base(logger, service)
    {
    }
}