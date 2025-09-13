using Domain.Enums;
using Domain.Interfaces;

namespace Infrastructure.LoggingData;

public class Logging : ILogging
{
    public void LogToFile(string message)
    {

    }

    public void LogToDB(RestMethods restMethod, string message, Guid fileGuid)
    {

    }

    public void LogToConsole(string message)
    {

    }
}
