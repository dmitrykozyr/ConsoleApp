using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using NTier.Logic.Services.Interfaces;
using NTier.UI.Models.Applicant;

namespace NTier.UI.Controllers;

[ApiController]
[EnableCors("angular")]
[Route("api/[controller]")]
public class ApplicantController : ControllerBase
{
    private IApplicantService _applicantService;

    public ApplicantController(IApplicantService applicant_Service)
    {
        _applicantService = applicant_Service;
    }

    [HttpPost]
    [Route("[action]")]
    public async Task<IActionResult> AddApplicant(ApplicantPassObject applicant)
    {
        var result = await _applicantService.AddSingleApplicant(
            applicant.Name,
            applicant.Surname,
            applicant.Birthday,
            applicant.Email,
            applicant.PhoneNumber);

        switch (result.Success)
        {
            case true:  return Ok(result);
            case false: return StatusCode(500, result);
        }
    }

    [HttpGet]
    [Route("[action]")]
    public async Task<IActionResult> GetApplicantById(int id)
    {

        var result = await _applicantService.GetApplicantById(id);
        switch (result.Success)
        {
            case true:  return Ok(result);
            case false: return StatusCode(500, result);
        }
    }

    [HttpPost]
    [Route("[action]")]
    public async Task<IActionResult> UpdateApplicant(ApplicantUpdatePassObject applicant)
    {
        var result = await _applicantService.UpdateApplicant(
            applicant.Id,
            applicant.Name,
            applicant.Surname,
            applicant.Birthday,
            applicant.Email,
            applicant.PhoneNumber);

        switch (result.Success)
        {
            case true:  return Ok(result);
            case false: return StatusCode(500, result);
        }
    }
}
