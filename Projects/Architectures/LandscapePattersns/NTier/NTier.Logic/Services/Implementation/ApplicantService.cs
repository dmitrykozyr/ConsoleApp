using NTier.DataAccess.Entities;
using NTier.DataAccess.Functions.CRUD_;
using NTier.DataAccess.Functions.Interfaces;
using NTier.Logic.Services.Interfaces;
using NTier.Logic.Services.Models;
using NTier.Logic.Services.Models.Applicant;

namespace NTier.Logic.Services.Implementation;

public class ApplicantService : IApplicantService
{
    private ICRUD _crud = new CRUD();

    public async Task<GenericResultSet<ApplicantResultSet>> AddSingleApplicant(
        string name,
        string surname,
        DateTime birthday,
        string email,
        string phoneNumber)
    {
        var result = new GenericResultSet<ApplicantResultSet>();

        try
        {
            var Applicant = new Applicant()
            {
                ApplicantName = name,
                ApplicantSurname = surname,
                ApplicantBirthDate = birthday,
                ContactEmail = email,
                ContactNumber = phoneNumber
            };

            Applicant = await _crud.Create(Applicant);

            var applicantAdded = new ApplicantResultSet
            {
                Id              = Applicant.ApplicantId,
                Name            = Applicant.ApplicantName,
                Surname         = Applicant.ApplicantSurname,
                Birthday        = Applicant.ApplicantBirthDate,
                Email           = Applicant.ContactEmail,
                PhoneNumber     = Applicant.ContactNumber,
                SubmissionDate  = Applicant.ApplicantCreationDate
            };

            result.UserMessage = string.Format("Your child {0} was registered successfully", name);
            
            result.InternalMessage = "LOGIC.Services.Implementation.Applicant_Service: AddSingleApplicant() method executed successfully";
            
            result.ResultSet = applicantAdded;
           
            result.Success = true;
        }
        catch (Exception ex)
        {
            result.Exception = ex;
            
            result.UserMessage      = string.Format("Failed to register information about your child");            
            result.InternalMessage  = string.Format("ERROR: LOGIC.Services.Implementation.Applicant_Service: AddSingleApplicant(): {0}", ex.Message);
        }

        return result;
    }

    public async Task<GenericResultSet<ApplicantResultSet>> GetApplicantById(long applicant_id)
    {
        var result = new GenericResultSet<ApplicantResultSet>();

        try
        {
            Applicant Applicant = await _crud.Read<Applicant>(applicant_id);

            var applicantReturned = new ApplicantResultSet
            {
                Id              = Applicant.ApplicantId,
                Name            = Applicant.ApplicantName,
                Surname         = Applicant.ApplicantSurname,
                Birthday        = Applicant.ApplicantBirthDate,
                Email           = Applicant.ContactEmail,
                PhoneNumber     = Applicant.ContactNumber,
                SubmissionDate  = Applicant.ApplicantCreationDate
            };

            result.UserMessage      = string.Format("Applicant {0} was found successfully", applicantReturned.Name);            
            result.InternalMessage  = "LOGIC.Services.Implementation.Applicant_Service: GetApplicantById() method executed successfully";
            result.ResultSet        = applicantReturned;
            result.Success          = true;
        }
        catch (Exception ex)
        {
            result.Exception = ex;            

            result.UserMessage      = string.Format("Failed to get applicant");            
            result.InternalMessage  = string.Format("ERROR: LOGIC.Services.Implementation.Applicant_Service: GetApplicantById(): {0}", ex.Message);
        }

        return result;
    }

    public async Task<GenericResultSet<ApplicantResultSet>> UpdateApplicant(
        long applicantId,
        string name,
        string surname,
        DateTime birthday,
        string email,
        string phone_number)
    {
        var result = new GenericResultSet<ApplicantResultSet>();

        try
        {
            var ApplicantToUpdate = new Applicant()
            {
                ApplicantId             = applicantId,
                ApplicantName           = name,
                ApplicantSurname        = surname,
                ApplicantBirthDate      = birthday,
                ContactEmail            = email,
                ContactNumber           = phone_number,
                ApplicantModifiedDate   = DateTime.UtcNow
            };

            ApplicantToUpdate = await _crud.Update<Applicant>(ApplicantToUpdate, applicantId);

            var applicantAdded = new ApplicantResultSet
            {
                Id              = ApplicantToUpdate.ApplicantId,
                Name            = ApplicantToUpdate.ApplicantName,
                Surname         = ApplicantToUpdate.ApplicantSurname,
                Birthday        = ApplicantToUpdate.ApplicantBirthDate,
                Email           = ApplicantToUpdate.ContactEmail,
                PhoneNumber     = ApplicantToUpdate.ContactNumber,
                SubmissionDate  = ApplicantToUpdate.ApplicantCreationDate
            };

            result.UserMessage = string.Format("Applicant {0} was updated successfully", name);
            result.InternalMessage = "LOGIC.Services.Implementation.Applicant_Service: UpdateApplicant() method executed successfully";
            result.ResultSet = applicantAdded;
            result.Success = true;
        }
        catch (Exception ex)
        {
            result.Exception = ex;            
            result.UserMessage      = string.Format("Failed to update applicant");            
            result.InternalMessage  = string.Format("ERROR: LOGIC.Services.Implementation.Applicant_Service: UpdateApplicant(): {0}", ex.Message);
        }

        return result;
    }
}
