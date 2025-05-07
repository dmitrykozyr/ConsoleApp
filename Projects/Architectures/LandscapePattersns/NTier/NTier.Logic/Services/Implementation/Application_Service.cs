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
        Int64 grade_id,
        Int64 application_status_id,
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
                id = ApplicationAdded.Applicant_ID,
                name = ApplicationAdded.Applicant.Applicant_Name,
                surname = ApplicationAdded.Applicant.Applicant_Surname,
                birthday = ApplicationAdded.Applicant.Applicant_BirthDate,
                email = ApplicationAdded.Applicant.Contact_Email,
                phone_number = ApplicationAdded.Applicant.Contact_Number,
                submission_date = ApplicationAdded.Applicant.Applicant_CreationDate
            };

            result.result_set.application_ResultSet = new Application_ResultSet
            {
                id = ApplicationAdded.Application_ID,
                applicant_id = ApplicationAdded.Applicant_ID,
                grade_id = ApplicationAdded.Grade_ID,
                status_id = ApplicationAdded.ApplicationStatus_ID,
                school_year = ApplicationAdded.SchoolYear,
                submission_date = ApplicationAdded.Application_CreationDate
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

    public async Task<Generic_ResultSet<Application_ResultSet>> GetApplicationById(Int64 application_id)
    {
        var result = new Generic_ResultSet<Application_ResultSet>();

        try
        {
            var Application = await _crud.Read<Application>(application_id);

            var appplicationReturned = new Application_ResultSet
            {
                id = Application.Application_ID,
                applicant_id = Application.Applicant_ID,
                grade_id = Application.Grade_ID,
                status_id = Application.ApplicationStatus_ID,
                school_year = Application.SchoolYear,
                submission_date = Application.Application_CreationDate
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
        Int64 application_id,
        Int64 grade_id,
        Int64 application_status_id,
        Int32 school_year,
        Int64 applicant_id)
    {
        var result = new Generic_ResultSet<Application_ResultSet>();

        try
        {
            Application Application = new Application
            {
                Application_ID = application_id,
                Applicant_ID = applicant_id,
                Grade_ID = grade_id,
                ApplicationStatus_ID = application_status_id,
                SchoolYear = school_year,
                Application_ModifiedDate = DateTime.UtcNow
            };

            Application = await _crud.Update<Application>(Application, application_id);

            Application_ResultSet applicationUpdated = new Application_ResultSet
            {
                id = Application.Application_ID,
                applicant_id = Application.Applicant_ID,
                grade_id = Application.Grade_ID,
                status_id = Application.ApplicationStatus_ID,
                school_year = Application.SchoolYear,
                submission_date = Application.Application_CreationDate
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

    public async Task<Generic_ResultSet<List<Application_ResultSet>>> GetApplicationsByApplicantId(Int64 applicant_id)
    {
        var result = new Generic_ResultSet<List<Application_ResultSet>>();
        
        result.result_set = new List<Application_ResultSet>();

        try
        {
            List<Application> Applications = await _application_Operations.GetApplicationsByApplicantId(applicant_id);

            Applications.ForEach(app => {
                result.result_set.Add(new Application_ResultSet
                {
                    applicant_id = app.Applicant_ID,
                    grade_id = app.Grade_ID,
                    id = app.Application_ID,
                    school_year = app.SchoolYear,
                    status_id = app.ApplicationStatus_ID,
                    submission_date = app.Application_CreationDate
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
