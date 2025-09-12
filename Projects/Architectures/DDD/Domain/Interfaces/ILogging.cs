namespace Domain.Interfaces;

public interface ILogging
{
    void LogToFile(string message);

    void LogToDb(string message);

    void LogToConsole(string message);
}
