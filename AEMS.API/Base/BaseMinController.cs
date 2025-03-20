using IMS.Business.Services;
using Microsoft.AspNetCore.Mvc;

namespace IMS.API.Base;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class BaseMinController<T, TService> : ControllerBase where TService : IBaseMinService
{
    protected readonly TService Service;
    protected readonly ILogger<T> Logger;

    public BaseMinController(ILogger<T> logger, TService service)
    {
        Service = service;
        Logger = logger;
    }

    ~BaseMinController()
    {
        Logger.LogInformation("Instance Destroyed!");
    }
}
