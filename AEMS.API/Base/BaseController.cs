using IMS.API.Utilities.Auth;
using IMS.Business.Services;
using IMS.Business.Utitlity;
using IMS.Domain.Utilities;
using Microsoft.AspNetCore.Mvc;
using System.Net;

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
    [AuthorizeAnyPolicy("AllAll", "AllOrganization", "ManageOrganization", "CreateOrganization")]
    public virtual async Task<IActionResult> Get([FromQuery] Pagination pagination)
    {
        var result = await Service.GetAll(pagination);
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
    [AuthorizeAnyPolicy("AllAll", "AllOrganization", "ManageOrganization", "CreateOrganization")]
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

    [HttpDelete("{id:guid}")]
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
