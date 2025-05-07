using NTier.Logic.Services.Models;
using NTier.Logic.Services.Models.ApplicationStatus;

namespace NTier.Logic.Services.Interfaces;

public interface IApplicationStatusService
{
    Task<GenericResultSet<ApplicationStatusResultSet>> AddApplicationStatus(string name);

    Task<GenericResultSet<List<ApplicationStatusResultSet>>> GetAllApplicationStatuses();

    Task<GenericResultSet<ApplicationStatusResultSet>> UpdateApplicationStatus(long statusId, string name);
}
