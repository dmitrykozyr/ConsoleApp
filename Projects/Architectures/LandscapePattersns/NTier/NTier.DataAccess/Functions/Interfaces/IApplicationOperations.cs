using NTier.DataAccess.Entities;

namespace NTier.DataAccess.Functions.Interfaces;

public interface IApplicationOperations
{
    Task<Application> AddFullApplication(
                        long gradeId,
                        long applicationStatusId,
                        int schoolYear,
                        string firstName,
                        string surName,
                        DateTime birthDate,
                        string email,
                        string contactNumber);

    Task<List<Application>> GetApplicationsByApplicantId(long applicantId);
}
