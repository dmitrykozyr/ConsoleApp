using NTier.Logic.Services.Models;
using NTier.Logic.Services.Models.Applicant;

namespace NTier.Logic.Services.Interfaces;

public interface IApplicant_Service
{
    Task<Generic_ResultSet<Applicant_ResultSet>> AddSingleApplicant(
        string name,
        string surname,
        DateTime birthday,
        string email,
        string phone_number);

    Task<Generic_ResultSet<Applicant_ResultSet>> GetApplicantById(long applicant_id);

    Task<Generic_ResultSet<Applicant_ResultSet>> UpdateApplicant(
        long applicant_id,
        string name,
        string surname,
        DateTime birthday,
        string email,
        string phone_number);

}
