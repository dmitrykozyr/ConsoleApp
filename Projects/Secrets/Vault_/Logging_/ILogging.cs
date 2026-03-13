namespace Vault_.Logging_;

public interface ILogging
{
    void LogToFile(string message);

    void LogToDB(Guid guid);

    void LogToConsole(string message);
}
