using NTier.Logic.Services.Models;
using NTier.Logic.Services.Models.Application;

namespace NTier.Logic.Services.Interfaces;

public interface IApplication_Service
{
    Task<Generic_ResultSet<ApplicationApplicant_ResultSet>> AddApplicationAndApplicant(
                Int64 grade_id, Int64 application_status_id, Int32 school_year, string name, string surname, 
                DateTime birthday, string email, string phone_number);

    Task<Generic_ResultSet<Application_ResultSet>> GetApplicationById(Int64 application_id);

    Task<Generic_ResultSet<Application_ResultSet>> UpdateApplication(
                Int64 application_id, Int64 grade_id, Int64 application_status_id, Int32 school_year, Int64 applicant_id);

    Task<Generic_ResultSet<List<Application_ResultSet>>> GetApplicationsByApplicantId(Int64 applicant_id);
}
