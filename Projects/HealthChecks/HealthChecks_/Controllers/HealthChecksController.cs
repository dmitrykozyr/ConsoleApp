using HealthChecks_.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HealthChecks_.Controllers;

[ApiController]
[Route("[controller]")]
public class HealthChecksController
{
    private readonly IHealthService _healthService;

    public HealthChecksController(IHealthService healthService)
    {
        _healthService = healthService;
    }

    [HttpGet(Name = "Get")]
    public IResult Get()
    {
        _healthService.CheckAllServices();

        return Results.Ok();
    }
}
