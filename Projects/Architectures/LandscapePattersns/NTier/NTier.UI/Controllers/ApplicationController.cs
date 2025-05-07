using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using NTier.Logic.Services.Interfaces;
using NTier.UI.Models.Application;

namespace NTier.UI.Controllers;

[EnableCors("angular")]
[Route("api/[controller]")]
[ApiController]
public class ApplicationController : ControllerBase
{
    private IApplication_Service _application_Service;
    public ApplicationController(IApplication_Service application_Service)
    {
        _application_Service = application_Service;
    }

    [HttpPost]
    [Route("[action]")]
    public async Task<IActionResult> AddApplicationApplicant(ApplicationPassObject application)
    {
        var result = await _application_Service.AddApplicationAndApplicant(
            application.GradeId,
            1,
            application.SchoolYear,
            application.Applicant.Name,
            application.Applicant.Surname,
            application.Applicant.Birthday,
            application.Applicant.Email,
            application.Applicant.PhoneNumber);

        switch (result.success)
        {
            case true:  return Ok(result);
            case false: return StatusCode(500, result);
        }
    }

    [HttpPost]
    [Route("[action]")]
    public async Task<IActionResult> UpdateApplication(ApplicationUpdatePassObject application)
    {
        var result = await _application_Service.UpdateApplication(
            application.Id,
            application.GradeId,
            application.StatusId,
            application.SchoolYear,
            application.ApplicantId);

        switch (result.success)
        {
            case true:  return Ok(result);
            case false: return StatusCode(500, result);
        }
    }

    [HttpGet]
    [Route("[action]")]
    public async Task<IActionResult> GetApplicationsByApplicantId(int id)
    {
        var result = await _application_Service.GetApplicationsByApplicantId(id);

        switch (result.success)
        {
            case true:  return Ok(result);
            case false: return StatusCode(500, result);
        }
    }

    [HttpGet]
    [Route("[action]")]
    public async Task<IActionResult> GetApplicationById(int id)
    {
        var result = await _application_Service.GetApplicationById(id);

        switch (result.success)
        {
            case true:  return Ok(result);
            case false: return StatusCode(500, result);
        }
    }
}
