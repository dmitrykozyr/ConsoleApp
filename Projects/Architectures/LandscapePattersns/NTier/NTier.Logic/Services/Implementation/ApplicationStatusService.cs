using NTier.DataAccess.Entities;
using NTier.DataAccess.Functions.CRUD_;
using NTier.DataAccess.Functions.Interfaces;
using NTier.Logic.Services.Interfaces;
using NTier.Logic.Services.Models;
using NTier.Logic.Services.Models.ApplicationStatus;

namespace NTier.Logic.Services.Implementation;

public class ApplicationStatusService : IApplicationStatusService
{
    private ICRUD _crud = new CRUD();

    public async Task<GenericResultSet<ApplicationStatusResultSet>> AddApplicationStatus(string name)
    {
        var result = new GenericResultSet<ApplicationStatusResultSet>();

        try
        {
            var ApplicationStatus = new ApplicationStatus
            {
                ApplicationStatusName = name
            };

            ApplicationStatus = await _crud.Create<ApplicationStatus>(ApplicationStatus);

            var statusAdded = new ApplicationStatusResultSet
            {
                Name = ApplicationStatus.ApplicationStatusName,
                StatusId = ApplicationStatus.ApplicationStatusId
            };

            result.UserMessage = string.Format("Status {0} was added successfully", name);
            
            result.InternalMessage = "LOGIC.Services.Implementation.ApplicationStatus_Service: AddApplicationStatus() method executed successfully";
            
            result.ResultSet = statusAdded;
            
            result.Success = true;
        }
        catch (Exception ex)
        {
            result.Exception = ex;
            
            result.UserMessage = string.Format("Failed to register information");
            
            result.InternalMessage = string.Format("ERROR: LOGIC.Services.Implementation.ApplicationStatus_Service: AddApplicationStatus(): {0}", ex.Message);
        }

        return result;
    }

    public async Task<GenericResultSet<List<ApplicationStatusResultSet>>> GetAllApplicationStatuses()
    {
        var result = new GenericResultSet<List<ApplicationStatusResultSet>>();
        try
        {
            List<ApplicationStatus> ApplicationStatuses = await _crud.ReadAll<ApplicationStatus>();

            result.ResultSet = new List<ApplicationStatusResultSet>();

            ApplicationStatuses.ForEach(z =>
            {
                result.ResultSet.Add(new ApplicationStatusResultSet
                {
                    StatusId = z.ApplicationStatusId,
                    Name = z.ApplicationStatusName
                });
            });

            result.UserMessage = string.Format("All application statuses obtained successfully");
            
            result.InternalMessage = "LOGIC.Services.Implementation.ApplicationStatus_Service: GetAllApplicationStatuses() method executed successfully";
            
            result.Success = true;
        }
        catch (Exception ex)
        {
            result.Exception = ex;
            
            result.UserMessage = string.Format("Failed to fetch statuses from the DB");
            
            result.InternalMessage = string.Format("ERROR: LOGIC.Services.Implementation.ApplicationStatus_Service: GetAllApplicationStatuses(): {0}", ex.Message);
        }

        return result;
    }

    public async Task<GenericResultSet<ApplicationStatusResultSet>> UpdateApplicationStatus(long status_id, string name)
    {
        var result = new GenericResultSet<ApplicationStatusResultSet>();
        try
        {
            var ApplicationStatus = new ApplicationStatus
            {
                ApplicationStatusId = status_id,
                ApplicationStatusName = name,
                ApplicationStatusModifiedDate = DateTime.UtcNow
            };

            ApplicationStatus = await _crud.Update<ApplicationStatus>(ApplicationStatus, status_id);

            var statusUpdated = new ApplicationStatusResultSet
            {
                Name = ApplicationStatus.ApplicationStatusName,
                StatusId = ApplicationStatus.ApplicationStatusId
            };

            result.UserMessage = string.Format("Status {0} was updated successfully", name);
            
            result.InternalMessage = "LOGIC.Services.Implementation.ApplicationStatus_Service: UpdateApplicationStatus() method executed successfully";
            
            result.ResultSet = statusUpdated;
            
            result.Success = true;
        }
        catch (Exception ex)
        {
            result.Exception = ex;
            
            result.UserMessage = string.Format("Failed to update status");
            
            result.InternalMessage = string.Format("ERROR: LOGIC.Services.Implementation.ApplicationStatus_Service: UpdateApplicationStatus(): {0}", ex.Message);
        }

        return result;
    }
}
