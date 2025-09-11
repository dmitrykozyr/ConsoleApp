using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using NTier.Logic.Services.Interfaces;
using NTier.UI.Models.Application;

namespace NTier.UI.Controllers;

[EnableCors("angular")]
[Route("api/[controller]")]
[ApiController]
public class ApplicationController
{
    private IApplicationService _applicationService;

    public ApplicationController(IApplicationService applicationService)
    {
        _applicationService = applicationService;
    }

    [HttpPost]
    [Route("[action]")]
    public async Task<IResult> AddApplicationApplicant(ApplicationPassObject application)
    {
        var result = await _applicationService.AddApplicationAndApplicant(
            application.GradeId,
            1,
            application.SchoolYear,
            application.Applicant.Name,
            application.Applicant.Surname,
            application.Applicant.Birthday,
            application.Applicant.Email,
            application.Applicant.PhoneNumber);

        switch (result.Success)
        {
            case true:  return Results.Ok(result);
            case false: return Results.BadRequest(result);
        }
    }

    [HttpPost]
    [Route("[action]")]
    public async Task<IResult> UpdateApplication(ApplicationUpdatePassObject application)
    {
        var result = await _applicationService.UpdateApplication(
            application.Id,
            application.GradeId,
            application.StatusId,
            application.SchoolYear,
            application.ApplicantId);

        switch (result.Success)
        {
            case true:  return Results.Ok(result);
            case false: return Results.BadRequest(result);
        }
    }

    [HttpGet]
    [Route("[action]")]
    public async Task<IResult> GetApplicationsByApplicantId(int id)
    {
        var result = await _applicationService.GetApplicationsByApplicantId(id);

        switch (result.Success)
        {
            case true:  return Results.Ok(result);
            case false: return Results.BadRequest(result);
        }
    }

    [HttpGet]
    [Route("[action]")]
    public async Task<IResult> GetApplicationById(int id)
    {
        var result = await _applicationService.GetApplicationById(id);

        switch (result.Success)
        {
            case true:  return Results.Ok(result);
            case false: return Results.BadRequest(result);
        }
    }
}
