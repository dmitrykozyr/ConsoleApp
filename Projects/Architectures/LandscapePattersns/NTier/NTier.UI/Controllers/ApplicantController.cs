using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using NTier.Logic.Services.Interfaces;
using NTier.UI.Models.Applicant;

namespace NTier.UI.Controllers
{
    [EnableCors("angular")]
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicantController : ControllerBase
    {
        private IApplicant_Service _applicantService;

        public ApplicantController(IApplicant_Service applicant_Service)
        {
            _applicantService = applicant_Service;
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> AddApplicant(Applicant_Pass_Object applicant)
        {
            var result = await _applicantService.AddSingleApplicant(
                applicant.name, applicant.surname, applicant.birthday, applicant.email, applicant.phone_number);

            switch (result.success)
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
            switch (result.success)
            {
                case true:  return Ok(result);
                case false: return StatusCode(500, result);
            }
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> UpdateApplicant(ApplicantUpdate_Pass_Object applicant)
        {
            var result = await _applicantService.UpdateApplicant(
                applicant.id, applicant.name, applicant.surname, applicant.birthday, applicant.email, applicant.phone_number);

            switch (result.success)
            {
                case true:  return Ok(result);
                case false: return StatusCode(500, result);
            }
        }
    }
}
