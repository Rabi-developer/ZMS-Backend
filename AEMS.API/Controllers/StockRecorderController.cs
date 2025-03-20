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
[AllowAnonymous]
public class StockReorderController : BaseController<StockReorderController, IStockReorderService, StockReorderReq, StockReorderRes, StockReorder>
{
    /// <inheritdoc />
    public StockReorderController(ILogger<StockReorderController> logger, IStockReorderService service) : base(logger, service)
    {
    }


}