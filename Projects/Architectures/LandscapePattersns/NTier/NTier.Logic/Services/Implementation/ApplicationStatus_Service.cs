using NTier.DataAccess.Entities;
using NTier.DataAccess.Functions.CRUD_;
using NTier.DataAccess.Functions.Interfaces;
using NTier.Logic.Services.Interfaces;
using NTier.Logic.Services.Models;
using NTier.Logic.Services.Models.ApplicationStatus;

namespace NTier.Logic.Services.Implementation;

public class ApplicationStatus_Service : IApplicationStatus_Service
{
    private ICRUD _crud = new CRUD();

    public async Task<Generic_ResultSet<ApplicationStatus_ResultSet>> AddApplicationStatus(string name)
    {
        var result = new Generic_ResultSet<ApplicationStatus_ResultSet>();
        try
        {
            var ApplicationStatus = new ApplicationStatus
            {
                ApplicationStatus_Name = name
            };

            ApplicationStatus = await _crud.Create<ApplicationStatus>(ApplicationStatus);
            var statusAdded = new ApplicationStatus_ResultSet
            {
                Name = ApplicationStatus.ApplicationStatus_Name,
                StatusId = ApplicationStatus.ApplicationStatus_ID
            };

            result.userMessage = string.Format("Status {0} was added successfully", name);
            result.internalMessage = "LOGIC.Services.Implementation.ApplicationStatus_Service: AddApplicationStatus() method executed successfully";
            result.result_set = statusAdded;
            result.success = true;
        }
        catch (Exception ex)
        {
            result.exception = ex;
            result.userMessage = string.Format("Failed to register information");
            result.internalMessage = string.Format("ERROR: LOGIC.Services.Implementation.ApplicationStatus_Service: AddApplicationStatus(): {0}", ex.Message);
        }

        return result;
    }

    public async Task<Generic_ResultSet<List<ApplicationStatus_ResultSet>>> GetAllApplicationStatuses()
    {
        var result = new Generic_ResultSet<List<ApplicationStatus_ResultSet>>();
        try
        {
            List<ApplicationStatus> ApplicationStatuses = await _crud.ReadAll<ApplicationStatus>();
            result.result_set = new List<ApplicationStatus_ResultSet>();
            ApplicationStatuses.ForEach(z =>
            {
                result.result_set.Add(new ApplicationStatus_ResultSet
                {
                    StatusId = z.ApplicationStatus_ID,
                    Name = z.ApplicationStatus_Name
                });
            });

            result.userMessage = string.Format("All application statuses obtained successfully");
            result.internalMessage = "LOGIC.Services.Implementation.ApplicationStatus_Service: GetAllApplicationStatuses() method executed successfully";
            result.success = true;
        }
        catch (Exception ex)
        {
            result.exception = ex;
            result.userMessage = string.Format("Failed to fetch statuses from the DB");
            result.internalMessage = string.Format("ERROR: LOGIC.Services.Implementation.ApplicationStatus_Service: GetAllApplicationStatuses(): {0}", ex.Message);
        }

        return result;
    }

    public async Task<Generic_ResultSet<ApplicationStatus_ResultSet>> UpdateApplicationStatus(long status_id, string name)
    {
        var result = new Generic_ResultSet<ApplicationStatus_ResultSet>();
        try
        {
            var ApplicationStatus = new ApplicationStatus
            {
                ApplicationStatus_ID = status_id,
                ApplicationStatus_Name = name,
                ApplicationStatus_ModifiedDate = DateTime.UtcNow
            };

            ApplicationStatus = await _crud.Update<ApplicationStatus>(ApplicationStatus, status_id);
            var statusUpdated = new ApplicationStatus_ResultSet
            {
                Name = ApplicationStatus.ApplicationStatus_Name,
                StatusId = ApplicationStatus.ApplicationStatus_ID
            };

            result.userMessage = string.Format("Status {0} was updated successfully", name);
            result.internalMessage = "LOGIC.Services.Implementation.ApplicationStatus_Service: UpdateApplicationStatus() method executed successfully";
            result.result_set = statusUpdated;
            result.success = true;
        }
        catch (Exception ex)
        {
            result.exception = ex;
            result.userMessage = string.Format("Failed to update status");
            result.internalMessage = string.Format("ERROR: LOGIC.Services.Implementation.ApplicationStatus_Service: UpdateApplicationStatus(): {0}", ex.Message);
        }

        return result;
    }
}
