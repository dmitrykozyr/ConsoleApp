using Domain.Enums;

namespace Domain.Interfaces;

public interface ILogging
{
    void LogToFile(string message);

    void LogToDB(RestMethods operationType, string bucketPath, Guid guid);

    void LogToConsole(string message);
}
