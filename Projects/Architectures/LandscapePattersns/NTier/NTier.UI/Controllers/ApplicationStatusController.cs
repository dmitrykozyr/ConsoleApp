using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using NTier.Logic.Services.Interfaces;
using NTier.UI.Models.ApplicationStatus;

namespace NTier.UI.Controllers;

[EnableCors("angular")]
[Route("api/[controller]")]
[ApiController]
public class ApplicationStatusController : ControllerBase
{
    private IApplicationStatusService _applicationStatusService;

    public ApplicationStatusController(IApplicationStatusService applicationStatusService)
    {
        _applicationStatusService = applicationStatusService;
    }

    [HttpGet]
    [Route("[action]")]
    public async Task<IActionResult> AddAppStatus(string name)
    {
        var result = await _applicationStatusService.AddApplicationStatus(name);

        switch (result.Success)
        {
            case true:  return Ok(result);
            case false: return StatusCode(500, result);
        }
    }

    [HttpGet]
    [Route("[action]")]
    public async Task<IActionResult> GetAllAppStatuses()
    {
        var result = await _applicationStatusService.GetAllApplicationStatuses();

        switch (result.Success)
        {
            case true:  return Ok(result);
            case false: return StatusCode(500, result);
        }
    }

    [HttpPost]
    [Route("[action]")]
    public async Task<IActionResult> UpdateAppStatus(ApplicationStatusUpdatePassObject status)
    {
        var result = await _applicationStatusService.UpdateApplicationStatus(status.Id, status.Name);

        switch (result.Success)
        {
            case true:  return Ok(result);
            case false: return StatusCode(500, result);
        }
    }
}
