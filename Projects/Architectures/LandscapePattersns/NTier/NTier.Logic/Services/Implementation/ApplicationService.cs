using NTier.DataAccess.Entities;
using NTier.DataAccess.Functions.CRUD_;
using NTier.DataAccess.Functions.Interfaces;
using NTier.DataAccess.Functions.Specific;
using NTier.Logic.Services.Interfaces;
using NTier.Logic.Services.Models;
using NTier.Logic.Services.Models.Application;

namespace NTier.Logic.Services.Implementation;

public class ApplicationService : IApplicationService
{
    private ICRUD _crud = new CRUD();
    private IApplicationOperations _application_Operations = new ApplicationOperations();

    public async Task<GenericResultSet<ApplicationApplicantResultSet>> AddApplicationAndApplicant(
        long gradeId,
        long applicationStatusId,
        int schoolYear,
        string name, 
        string surname,
        DateTime birthday,
        string email,
        string phoneNumber)
    {
        var result = new GenericResultSet<ApplicationApplicantResultSet>();
        result.ResultSet = new ApplicationApplicantResultSet();

        try
        {
            Application ApplicationAdded = await _application_Operations.AddFullApplication(
                gradeId,
                applicationStatusId,
                schoolYear,
                name,
                surname,
                birthday,
                email,
                phoneNumber);

            result.ResultSet.ApplicantResultSet = new Models.Applicant.ApplicantResultSet
            {
                Id              = ApplicationAdded.ApplicantId,
                Name            = ApplicationAdded.Applicant.ApplicantName,
                Surname         = ApplicationAdded.Applicant.ApplicantSurname,
                Birthday        = ApplicationAdded.Applicant.ApplicantBirthDate,
                Email           = ApplicationAdded.Applicant.ContactEmail,
                PhoneNumber     = ApplicationAdded.Applicant.ContactNumber,
                SubmissionDate  = ApplicationAdded.Applicant.ApplicantCreationDate
            };

            result.ResultSet.ApplicationResultSet = new ApplicationResultSet
            {
                Id              = ApplicationAdded.ApplicationId,
                ApplicantId     = ApplicationAdded.ApplicantId,
                GradeId         = ApplicationAdded.GradeId,
                StatusId        = ApplicationAdded.ApplicationStatusId,
                SchoolYear      = ApplicationAdded.SchoolYear,
                SubmissionDate  = ApplicationAdded.ApplicationCreationDate
            };

            result.UserMessage = "Application and applicant was added successfully";
            
            result.InternalMessage = "LOGIC.Services.Implementation.Application_Service: AddApplicationAndApplicant() method executed successfully";

            result.ResultSet = result.ResultSet;
            
            result.Success = true;
        }
        catch (Exception exception)
        {
            result.Exception = exception;
            
            result.UserMessage = "Failed to register information for your child";
            
            result.InternalMessage = string.Format("ERROR: LOGIC.Services.Implementation.Application_Service: AddApplicationAndApplicant(): {0}", exception.Message); ;
        }
        return result;
    }

    public async Task<GenericResultSet<ApplicationResultSet>> GetApplicationById(long application_id)
    {
        var result = new GenericResultSet<ApplicationResultSet>();

        try
        {
            var Application = await _crud.Read<Application>(application_id);

            var appplicationReturned = new ApplicationResultSet
            {
                Id              = Application.ApplicationId,
                ApplicantId     = Application.ApplicantId,
                GradeId         = Application.GradeId,
                StatusId        = Application.ApplicationStatusId,
                SchoolYear      = Application.SchoolYear,
                SubmissionDate  = Application.ApplicationCreationDate
            };

            result.UserMessage = "Your application was located successfully";
            
            result.InternalMessage = "LOGIC.Services.Implementation.Application_Service: GetApplicationById() method executed successfully";
            
            result.ResultSet = appplicationReturned;
            
            result.Success = true;
        }
        catch (Exception exception)
        {
            result.Exception = exception;
            
            result.UserMessage = "Failed to find the application you are looking for";
            
            result.InternalMessage = string.Format("ERROR: LOGIC.Services.Implementation.Application_Service: GetApplicationById(): {0}", exception.Message);
        }
        return result;
    }

    public async Task<GenericResultSet<ApplicationResultSet>> UpdateApplication(
        long applicationId,
        long gradeId,
        long applicationStatusId,
        int schoolYear,
        long applicantId)
    {
        var result = new GenericResultSet<ApplicationResultSet>();

        try
        {
            var Application = new Application
            {
                ApplicationId           = applicationId,
                ApplicantId             = applicantId,
                GradeId                 = gradeId,
                ApplicationStatusId     = applicationStatusId,
                SchoolYear              = schoolYear,
                ApplicationModifiedDate = DateTime.UtcNow
            };

            Application = await _crud.Update(Application, applicationId);

            var applicationUpdated = new ApplicationResultSet
            {
                Id              = Application.ApplicationId,
                ApplicantId     = Application.ApplicantId,
                GradeId         = Application.GradeId,
                StatusId        = Application.ApplicationStatusId,
                SchoolYear      = Application.SchoolYear,
                SubmissionDate  = Application.ApplicationCreationDate
            };

            result.UserMessage = "Application was updated successfully";
            
            result.InternalMessage = "LOGIC.Services.Implementation.Application_Service: UpdateApplication() method executed successfully";
            
            result.ResultSet = applicationUpdated;
            
            result.Success = true;
        }
        catch (Exception exception)
        {
            result.Exception = exception;
            
            result.UserMessage = "Failed to update your information for the application supplied";
            
            result.InternalMessage = string.Format("ERROR: LOGIC.Services.Implementation.Application_Service: UpdateApplication(): {0}", exception.Message); ;
        }

        return result;
    }

    public async Task<GenericResultSet<List<ApplicationResultSet>>> GetApplicationsByApplicantId(long applicant_id)
    {
        var result = new GenericResultSet<List<ApplicationResultSet>>();
        
        result.ResultSet = new List<ApplicationResultSet>();

        try
        {
            List<Application> Applications = await _application_Operations.GetApplicationsByApplicantId(applicant_id);

            Applications.ForEach(app => {
                result.ResultSet.Add(new ApplicationResultSet
                {
                    ApplicantId     = app.ApplicantId,
                    GradeId         = app.GradeId,
                    Id              = app.ApplicationId,
                    SchoolYear      = app.SchoolYear,
                    StatusId        = app.ApplicationStatusId,
                    SubmissionDate  = app.ApplicationCreationDate
                });
            });

            result.UserMessage = "Applications were located successfully";
            
            result.InternalMessage = "LOGIC.Services.Implementation.Application_Service: GetApplicationsByApplicantId() method executed successfully";
            
            result.ResultSet = result.ResultSet;
            
            result.Success = true;
        }
        catch (Exception exception)
        {
            result.Exception = exception;
            
            result.UserMessage = "Failed to find the applications you were looking for";
            
            result.InternalMessage = string.Format("ERROR: LOGIC.Services.Implementation.Application_Service: GetApplicationsByApplicantId(): {0}", exception.Message);
        }

        return result;
    }
}
