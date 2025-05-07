using NTier.Logic.Services.Models;
using NTier.Logic.Services.Models.Applicant;

namespace NTier.Logic.Services.Interfaces;

public interface IApplicantService
{
    Task<GenericResultSet<ApplicantResultSet>> AddSingleApplicant(
        string name,
        string surname,
        DateTime birthday,
        string email,
        string phoneNumber);

    Task<GenericResultSet<ApplicantResultSet>> GetApplicantById(long applicantId);

    Task<GenericResultSet<ApplicantResultSet>> UpdateApplicant(
        long applicantId,
        string name,
        string surname,
        DateTime birthday,
        string email,
        string phoneNumber);

}
