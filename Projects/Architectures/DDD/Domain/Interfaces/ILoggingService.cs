using DDD.Domain.Enums;
using Domain.Enums;

namespace Domain.Interfaces;

public interface ILoggingService
{
    Task LogToFile(LoggingTypes loggingType, string message);

    Task LogToDB(RestMethods operationType, string bucketPath, Guid guid);

    Task LogToConsole(string message);
}
