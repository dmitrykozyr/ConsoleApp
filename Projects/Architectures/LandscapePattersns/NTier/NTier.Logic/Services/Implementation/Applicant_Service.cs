using NTier.DataAccess.Entities;
using NTier.DataAccess.Functions.CRUD_;
using NTier.DataAccess.Functions.Interfaces;
using NTier.Logic.Services.Interfaces;
using NTier.Logic.Services.Models;
using NTier.Logic.Services.Models.Applicant;

namespace NTier.Logic.Services.Implementation;

public class Applicant_Service : IApplicant_Service
{
    private ICRUD _crud = new CRUD();

    public async Task<Generic_ResultSet<Applicant_ResultSet>> AddSingleApplicant(
        string name,
        string surname,
        DateTime birthday,
        string email,
        string phone_number)
    {
        var result = new Generic_ResultSet<Applicant_ResultSet>();

        try
        {
            var Applicant = new Applicant()
            {
                Applicant_Name = name,
                Applicant_Surname = surname,
                Applicant_BirthDate = birthday,
                Contact_Email = email,
                Contact_Number = phone_number
            };

            Applicant = await _crud.Create<Applicant>(Applicant);
            var applicantAdded = new Applicant_ResultSet
            {
                id = Applicant.Applicant_ID,
                name = Applicant.Applicant_Name,
                surname = Applicant.Applicant_Surname,
                birthday = Applicant.Applicant_BirthDate,
                email = Applicant.Contact_Email,
                phone_number = Applicant.Contact_Number,
                submission_date = Applicant.Applicant_CreationDate
            };

            result.userMessage = string.Format("Your child {0} was registered successfully", name);
            
            result.internalMessage = "LOGIC.Services.Implementation.Applicant_Service: AddSingleApplicant() method executed successfully";
            
            result.result_set = applicantAdded;
           
            result.success = true;
        }
        catch (Exception ex)
        {
            result.exception = ex;
            
            result.userMessage = string.Format("Failed to register information about your child");
            
            result.internalMessage = string.Format("ERROR: LOGIC.Services.Implementation.Applicant_Service: AddSingleApplicant(): {0}", ex.Message);
        }

        return result;
    }

    public async Task<Generic_ResultSet<Applicant_ResultSet>> GetApplicantById(Int64 applicant_id)
    {
        var result = new Generic_ResultSet<Applicant_ResultSet>();

        try
        {
            Applicant Applicant = await _crud.Read<Applicant>(applicant_id);

            var applicantReturned = new Applicant_ResultSet
            {
                id = Applicant.Applicant_ID,
                name = Applicant.Applicant_Name,
                surname = Applicant.Applicant_Surname,
                birthday = Applicant.Applicant_BirthDate,
                email = Applicant.Contact_Email,
                phone_number = Applicant.Contact_Number,
                submission_date = Applicant.Applicant_CreationDate
            };

            result.userMessage = string.Format("Applicant {0} was found successfully", applicantReturned.name);
            
            result.internalMessage = "LOGIC.Services.Implementation.Applicant_Service: GetApplicantById() method executed successfully";
            
            result.result_set = applicantReturned;
            
            result.success = true;
        }
        catch (Exception ex)
        {
            result.exception = ex;
            
            result.userMessage = string.Format("Failed to get applicant");
            
            result.internalMessage = string.Format("ERROR: LOGIC.Services.Implementation.Applicant_Service: GetApplicantById(): {0}", ex.Message);
        }

        return result;
    }

    public async Task<Generic_ResultSet<Applicant_ResultSet>> UpdateApplicant(
        Int64 applicant_id,
        string name,
        string surname,
        DateTime birthday,
        string email,
        string phone_number)
    {
        var result = new Generic_ResultSet<Applicant_ResultSet>();

        try
        {
            var ApplicantToUpdate = new Applicant()
            {
                Applicant_ID = applicant_id,
                Applicant_Name = name,
                Applicant_Surname = surname,
                Applicant_BirthDate = birthday,
                Contact_Email = email,
                Contact_Number = phone_number,
                Applicant_ModifiedDate = DateTime.UtcNow
            };

            ApplicantToUpdate = await _crud.Update<Applicant>(ApplicantToUpdate, applicant_id);

            var applicantAdded = new Applicant_ResultSet
            {
                id = ApplicantToUpdate.Applicant_ID,
                name = ApplicantToUpdate.Applicant_Name,
                surname = ApplicantToUpdate.Applicant_Surname,
                birthday = ApplicantToUpdate.Applicant_BirthDate,
                email = ApplicantToUpdate.Contact_Email,
                phone_number = ApplicantToUpdate.Contact_Number,
                submission_date = ApplicantToUpdate.Applicant_CreationDate
            };

            result.userMessage = string.Format("Applicant {0} was updated successfully", name);
            
            result.internalMessage = "LOGIC.Services.Implementation.Applicant_Service: UpdateApplicant() method executed successfully";
            
            result.result_set = applicantAdded;
            
            result.success = true;
        }
        catch (Exception ex)
        {
            result.exception = ex;
            
            result.userMessage = string.Format("Failed to update applicant");
            
            result.internalMessage = string.Format("ERROR: LOGIC.Services.Implementation.Applicant_Service: UpdateApplicant(): {0}", ex.Message);
        }

        return result;
    }
}
