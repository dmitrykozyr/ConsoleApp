using Domain.Enums;

namespace Domain.Interfaces;

public interface ILogging
{
    void LogToFile(string message);

    void LogToDB(RestMethods restMethod, string message, Guid fileGuid);

    void LogToConsole(string message);
}
