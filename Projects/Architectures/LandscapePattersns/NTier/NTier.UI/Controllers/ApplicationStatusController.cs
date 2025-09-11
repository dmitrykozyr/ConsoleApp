using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using NTier.Logic.Services.Interfaces;
using NTier.UI.Models.ApplicationStatus;

namespace NTier.UI.Controllers;

[EnableCors("angular")]
[Route("api/[controller]")]
[ApiController]
public class ApplicationStatusController
{
    private IApplicationStatusService _applicationStatusService;

    public ApplicationStatusController(IApplicationStatusService applicationStatusService)
    {
        _applicationStatusService = applicationStatusService;
    }

    [HttpGet]
    [Route("[action]")]
    public async Task<IResult> AddAppStatus(string name)
    {
        var result = await _applicationStatusService.AddApplicationStatus(name);

        switch (result.Success)
        {
            case true:  return Results.Ok(result);
            case false: return Results.BadRequest(result);
        }
    }

    [HttpGet]
    [Route("[action]")]
    public async Task<IResult> GetAllAppStatuses()
    {
        var result = await _applicationStatusService.GetAllApplicationStatuses();

        switch (result.Success)
        {
            case true:  return Results.Ok(result);
            case false: return Results.BadRequest(result);
        }
    }

    [HttpPost]
    [Route("[action]")]
    public async Task<IResult> UpdateAppStatus(ApplicationStatusUpdatePassObject status)
    {
        var result = await _applicationStatusService.UpdateApplicationStatus(status.Id, status.Name);

        switch (result.Success)
        {
            case true:  return Results.Ok(result);
            case false: return Results.BadRequest(result);
        }
    }
}
