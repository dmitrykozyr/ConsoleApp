using NTier.DataAccess.Entities;

namespace NTier.DataAccess.Functions.Interfaces
{
    public interface IApplication_Operations
    {
        Task<Application> AddFullApplication(
                            Int64 gradeId,
                            Int64 applicationStatusId,
                            Int32 schoolYear,
                            string firstName,
                            string surName,
                            DateTime birthDate,
                            string email,
                            string contactNumber);

        Task<List<Application>> GetApplicationsByApplicantId(Int64 applicantId);
    }
}
