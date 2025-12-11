using ZMS.API.Middleware;
using IMS.API.Utilities.Auth;
using IMS.Business.Services;
using IMS.Business.Utitlity;
using IMS.Domain.Utilities;
using IMS.Domain.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using ZMS.API.Middleware;
using IMS.Business.Services;
using ZMS.Business.DTOs.Requests;

namespace IMS.API.Base;

/// <summary>
/// BaseController
/// </summary>
/// <typeparam name="T">The Controller Reference</typeparam>
/// <typeparam name="TService">The Service Interface</typeparam>
/// <typeparam name="TReq">The Request Object</typeparam>
/// <typeparam name="TRes">The Response Object</typeparam>
[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
[Authorize]
public class BaseController<TController, TService, TReq, TRes, T> : ControllerBase where TService : IBaseService<TReq, TRes, T>
{
    private readonly ILogger<TController> _logger;
    protected readonly TService Service;

    public BaseController(ILogger<TController> logger, TService service)
    {
        _logger = logger;
        Service = service;
    }


    /// <summary>
    /// This Return a List of Addresses
    /// </summary>
    /// <see cref="GetAttribute"/>
    /// <returns></returns>
    /// <remarks>Remember this is Only For Specific Reasons.
    /// If you want to do something Else, ignore this</remarks>
    /// <exception cref="NotFoundResult">When Result isn't Found!</exception>
    /// <seealso cref="ApplicationIdentity"/>
    /// <response code="200">Data Added!</response>
    [HttpGet]
    [ProducesResponseType(typeof(IList<Response<object>>), 200)]
    [ProducesDefaultResponseType(typeof(string))]
    [ProducesResponseType(typeof(bool), 201)]
    [ProducesResponseType(typeof(string), 404)]

    [Permission("Role", "Read")]
    public virtual async Task<IActionResult> Get([FromQuery] Pagination pagination)
    {
        bool isSuperAdmin = User.IsInRole("SuperAdmin");
        var userId = User.GetUserId();
        pagination.RefId = userId.ToString();
        var result = await Service.GetAllByUser(pagination, (Guid)userId, !isSuperAdmin);
        if (result.StatusCode == HttpStatusCode.OK || result.StatusCode == HttpStatusCode.Created)
        {
            return Ok(result);
        }
        else
        {
            return BadRequest(result);
        }
    }


    /// <summary>
    /// This Returns with Id
    /// </summary>
    /// <param name="id">Id in Long. like: 80,12</param>
    /// <returns>A Good List Of Something?</returns>
    /// 
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(bool), 200)]
    [AllowAnonymous]
    public virtual async Task<IActionResult> Get(Guid id)
    {
        var result = await Service.Get(id);
        if (result.StatusCode == HttpStatusCode.OK || result.StatusCode == HttpStatusCode.Created)
        {
            return Ok(result);
        }
        else
        {
            return BadRequest(result);
        }
    }

    [HttpPost]
    [Permission("Role", "Create")]
    public virtual async Task<IActionResult> Post(TReq model)
    {

        var result = await Service.Add(model);
        if (result.StatusCode == HttpStatusCode.OK || result.StatusCode == HttpStatusCode.Created)
        {
            return Ok(result);
        }
        else
        {
            return BadRequest(result);
        }
    }

    [HttpPut]
    [Permission("Role", "Update")]
    public virtual async Task<IActionResult> Put(TReq model)
    {
        var result = await Service.Update(model);
        if (result.StatusCode == HttpStatusCode.OK || result.StatusCode == HttpStatusCode.Created)
        {
            return Ok(result);
        }
        else
        {
            return BadRequest(result);
        }
    }


    [HttpPut("Files/{id}")]
    public async Task<IActionResult> UpdateBookingOrder([FromBody] FileReq request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await Service.UpdateFileAsync((Guid)request.Id, request);
        return Ok(result);

    }

    [HttpDelete("{id:guid}")]
    [Permission("Role", "Delete")]
    public virtual async Task<IActionResult> Delete(Guid id)
    {
        var result = await Service.Delete(id);
        if (result.StatusCode == HttpStatusCode.OK || result.StatusCode == HttpStatusCode.Created)
        {
            return Ok(result);
        }
        else
        {
            return BadRequest(result);
        }
    }

    ~BaseController()
    {
        _logger.LogInformation("Instance Destroyed!");
    }

    public static string GetVersionedBase(string action)
    {
        return "api/v{version:apiVersion}/" + action;
    }
}
