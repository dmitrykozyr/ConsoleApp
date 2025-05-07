using NTier.Logic.Services.Models;
using NTier.Logic.Services.Models.Application;

namespace NTier.Logic.Services.Interfaces;

public interface IApplication_Service
{
    Task<Generic_ResultSet<ApplicationApplicant_ResultSet>> AddApplicationAndApplicant(
        long grade_id,
        long application_status_id,
        Int32 school_year,
        string name,
        string surname, 
        DateTime birthday,
        string email,
        string phone_number);

    Task<Generic_ResultSet<Application_ResultSet>> GetApplicationById(long application_id);

    Task<Generic_ResultSet<Application_ResultSet>> UpdateApplication(
        long application_id,
        long grade_id,
        long application_status_id,
        Int32 school_year,
        long applicant_id);

    Task<Generic_ResultSet<List<Application_ResultSet>>> GetApplicationsByApplicantId(long applicant_id);
}
