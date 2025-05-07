using NTier.DataAccess.Entities;
using NTier.DataAccess.Functions.CRUD_;
using NTier.DataAccess.Functions.Interfaces;
using NTier.DataAccess.Functions.Specific;
using NTier.Logic.Services.Interfaces;
using NTier.Logic.Services.Models;
using NTier.Logic.Services.Models.Application;

namespace NTier.Logic.Services.Implementation;

public class Application_Service : IApplication_Service
{
    private ICRUD _crud = new CRUD();
    private IApplication_Operations _application_Operations = new Application_Operations();

    public async Task<Generic_ResultSet<ApplicationApplicant_ResultSet>> AddApplicationAndApplicant(
        long grade_id,
        long application_status_id,
        Int32 school_year,
        string name, 
        string surname,
        DateTime birthday,
        string email,
        string phone_number)
    {
        var result = new Generic_ResultSet<ApplicationApplicant_ResultSet>();
        result.result_set = new ApplicationApplicant_ResultSet();
        try
        {
            Application ApplicationAdded = await _application_Operations.AddFullApplication(
                grade_id,
                application_status_id,
                school_year,
                name,
                surname,
                birthday,
                email,
                phone_number);

            result.result_set.applicant_ResultSet = new Models.Applicant.Applicant_ResultSet
            {
                Id = ApplicationAdded.ApplicantId,
                Name = ApplicationAdded.Applicant.Applicant_Name,
                Surname = ApplicationAdded.Applicant.Applicant_Surname,
                Birthday = ApplicationAdded.Applicant.Applicant_BirthDate,
                Email = ApplicationAdded.Applicant.Contact_Email,
                PhoneNumber = ApplicationAdded.Applicant.Contact_Number,
                SubmissionDate = ApplicationAdded.Applicant.Applicant_CreationDate
            };

            result.result_set.application_ResultSet = new Application_ResultSet
            {
                id = ApplicationAdded.ApplicationId,
                applicant_id = ApplicationAdded.ApplicantId,
                grade_id = ApplicationAdded.GradeId,
                status_id = ApplicationAdded.ApplicationStatusId,
                school_year = ApplicationAdded.SchoolYear,
                submission_date = ApplicationAdded.ApplicationCreationDate
            };

            result.userMessage = "Application and applicant was added successfully";
            
            result.internalMessage = "LOGIC.Services.Implementation.Application_Service: AddApplicationAndApplicant() method executed successfully";

            result.result_set = result.result_set;
            
            result.success = true;
        }
        catch (Exception exception)
        {
            result.exception = exception;
            
            result.userMessage = "Failed to register information for your child";
            
            result.internalMessage = string.Format("ERROR: LOGIC.Services.Implementation.Application_Service: AddApplicationAndApplicant(): {0}", exception.Message); ;
        }
        return result;
    }

    public async Task<Generic_ResultSet<Application_ResultSet>> GetApplicationById(long application_id)
    {
        var result = new Generic_ResultSet<Application_ResultSet>();

        try
        {
            var Application = await _crud.Read<Application>(application_id);

            var appplicationReturned = new Application_ResultSet
            {
                id = Application.ApplicationId,
                applicant_id = Application.ApplicantId,
                grade_id = Application.GradeId,
                status_id = Application.ApplicationStatusId,
                school_year = Application.SchoolYear,
                submission_date = Application.ApplicationCreationDate
            };

            result.userMessage = "Your application was located successfully";
            
            result.internalMessage = "LOGIC.Services.Implementation.Application_Service: GetApplicationById() method executed successfully";
            
            result.result_set = appplicationReturned;
            
            result.success = true;
        }
        catch (Exception exception)
        {
            result.exception = exception;
            
            result.userMessage = "Failed to find the application you are looking for";
            
            result.internalMessage = string.Format("ERROR: LOGIC.Services.Implementation.Application_Service: GetApplicationById(): {0}", exception.Message);
        }
        return result;
    }

    public async Task<Generic_ResultSet<Application_ResultSet>> UpdateApplication(
        long application_id,
        long grade_id,
        long application_status_id,
        Int32 school_year,
        long applicant_id)
    {
        var result = new Generic_ResultSet<Application_ResultSet>();

        try
        {
            Application Application = new Application
            {
                ApplicationId = application_id,
                ApplicantId = applicant_id,
                GradeId = grade_id,
                ApplicationStatusId = application_status_id,
                SchoolYear = school_year,
                ApplicationModifiedDate = DateTime.UtcNow
            };

            Application = await _crud.Update<Application>(Application, application_id);

            Application_ResultSet applicationUpdated = new Application_ResultSet
            {
                id = Application.ApplicationId,
                applicant_id = Application.ApplicantId,
                grade_id = Application.GradeId,
                status_id = Application.ApplicationStatusId,
                school_year = Application.SchoolYear,
                submission_date = Application.ApplicationCreationDate
            };

            result.userMessage = "Application was updated successfully";
            
            result.internalMessage = "LOGIC.Services.Implementation.Application_Service: UpdateApplication() method executed successfully";
            
            result.result_set = applicationUpdated;
            
            result.success = true;
        }
        catch (Exception exception)
        {
            result.exception = exception;
            
            result.userMessage = "Failed to update your information for the application supplied";
            
            result.internalMessage = string.Format("ERROR: LOGIC.Services.Implementation.Application_Service: UpdateApplication(): {0}", exception.Message); ;
        }
        return result;
    }

    public async Task<Generic_ResultSet<List<Application_ResultSet>>> GetApplicationsByApplicantId(long applicant_id)
    {
        var result = new Generic_ResultSet<List<Application_ResultSet>>();
        
        result.result_set = new List<Application_ResultSet>();

        try
        {
            List<Application> Applications = await _application_Operations.GetApplicationsByApplicantId(applicant_id);

            Applications.ForEach(app => {
                result.result_set.Add(new Application_ResultSet
                {
                    applicant_id = app.ApplicantId,
                    grade_id = app.GradeId,
                    id = app.ApplicationId,
                    school_year = app.SchoolYear,
                    status_id = app.ApplicationStatusId,
                    submission_date = app.ApplicationCreationDate
                });
            });

            result.userMessage = "Applications were located successfully";
            
            result.internalMessage = "LOGIC.Services.Implementation.Application_Service: GetApplicationsByApplicantId() method executed successfully";
            
            result.result_set = result.result_set;
            
            result.success = true;
        }
        catch (Exception exception)
        {
            result.exception = exception;
            
            result.userMessage = "Failed to find the applications you were looking for";
            
            result.internalMessage = string.Format("ERROR: LOGIC.Services.Implementation.Application_Service: GetApplicationsByApplicantId(): {0}", exception.Message);
        }
        return result;
    }
}
