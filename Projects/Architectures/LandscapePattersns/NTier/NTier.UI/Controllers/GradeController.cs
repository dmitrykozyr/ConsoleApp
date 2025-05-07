using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using NTier.Logic.Services.Interfaces;
using NTier.UI.Models.Grade;

namespace NTier.UI.Controllers;

[EnableCors("angular")]
[Route("api/[controller]")]
[ApiController]
public class GradeController : ControllerBase
{
    private IGradeService _gradeService;

    public GradeController(IGradeService gradeService)
    {
        _gradeService = gradeService;
    }

    [HttpPost]
    [Route("[action]")]
    public async Task<IActionResult> AddGrade(GradePassObject gradeObject)
    {
        var result = await _gradeService.AddSingleGrade(
            gradeObject.Name,
            gradeObject.GradeNumber,
            gradeObject.Capacity);

        switch (result.Success)
        {
            case true:  return Ok(result);
            case false: return StatusCode(500, result);
        }
    }

    [HttpGet]
    [Route("[action]")]
    public async Task<IActionResult> GetAllGrades()
    {
        var result = await _gradeService.GetAllGrades();

        switch (result.Success)
        {
            case true:  return Ok(result);
            case false: return StatusCode(500, result);
        }
    }

    [HttpPost]
    [Route("[action]")]
    public async Task<IActionResult> UpdateGrade(GradeUpdatePassObject gradeObject)
    {
        var result = await _gradeService.UpdateGrade(
            gradeObject.Id,
            gradeObject.Name,
            gradeObject.GradeNumber,
            gradeObject.Capacity);

        switch (result.Success)
        {
            case true:  return Ok(result);
            case false: return StatusCode(500, result);
        }
    }
}
