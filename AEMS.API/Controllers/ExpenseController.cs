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
public class ExpenseController : BaseController<ExpenseController, IExpenseService, ExpenseReq, ExpenseRes, Expense>
{
    /// <inheritdoc />
    public ExpenseController(ILogger<ExpenseController> logger, IExpenseService service) : base(logger, service)
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