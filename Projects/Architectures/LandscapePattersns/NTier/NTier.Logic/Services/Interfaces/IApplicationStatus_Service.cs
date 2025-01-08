using NTier.Logic.Services.Models;
using NTier.Logic.Services.Models.ApplicationStatus;

namespace NTier.Logic.Services.Interfaces;

public interface IApplicationStatus_Service
{
    Task<Generic_ResultSet<ApplicationStatus_ResultSet>> AddApplicationStatus(string name);
    Task<Generic_ResultSet<List<ApplicationStatus_ResultSet>>> GetAllApplicationStatuses();
    Task<Generic_ResultSet<ApplicationStatus_ResultSet>> UpdateApplicationStatus(Int64 statusId, string name);
}
