using NTier.Logic.Services.Models;
using NTier.Logic.Services.Models.Application;

namespace NTier.Logic.Services.Interfaces;

public interface IApplicationService
{
    Task<GenericResultSet<ApplicationApplicantResultSet>> AddApplicationAndApplicant(
        long gradeId,
        long applicationStatusId,
        int schoolYear,
        string name,
        string surname, 
        DateTime birthday,
        string email,
        string phoneNumber);

    Task<GenericResultSet<ApplicationResultSet>> GetApplicationById(long applicationId);

    Task<GenericResultSet<ApplicationResultSet>> UpdateApplication(
        long applicationId,
        long gradeId,
        long applicationStatusId,
        int schoolYear,
        long applicantId);

    Task<GenericResultSet<List<ApplicationResultSet>>> GetApplicationsByApplicantId(long applicantId);
}
