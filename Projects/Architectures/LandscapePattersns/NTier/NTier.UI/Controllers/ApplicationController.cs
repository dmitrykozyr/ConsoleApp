using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using NTier.Logic.Services.Interfaces;
using NTier.UI.Models.Application;

namespace NTier.UI.Controllers
{
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
        public async Task<IActionResult> AddApplicationApplicant(Application_Pass_Object application)
        {
            var result = await _application_Service.AddApplicationAndApplicant(
                application.grade_id, 1, application.school_year, application.applicant.name, application.applicant.surname,
                application.applicant.birthday, application.applicant.email, application.applicant.phone_number);

            switch (result.success)
            {
                case true:  return Ok(result);
                case false: return StatusCode(500, result);
            }
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> UpdateApplication(ApplicationUpdate_Pass_Object application)
        {
            var result = await _application_Service.UpdateApplication(
                application.id, application.grade_id, application.status_id, application.school_year, application.applicant_id);

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
}
