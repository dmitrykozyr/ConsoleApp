using CommunityToolkit.Diagnostics;
using Domain.Enums;
using Domain.Formatters;
using Domain.Interfaces;
using Domain.Interfaces.Db;
using Domain.Models.Options;
using Microsoft.Extensions.Options;
using System.Data.SqlClient;
using System.Text;

namespace Infrastructure.LoggingData;

public class Logging : ILogging
{
    public static GeneralOptions? GeneralOptions { get; set; }

    private readonly IDbConStrService _dbConStrService;

    private const string DOSERVICE_BCSFS_LOG_ADD = "";

    public Logging(IOptions<GeneralOptions> generalOptions, IDbConStrService dbConStrService)
    {
        GeneralOptions = generalOptions.Value;

        _dbConStrService = dbConStrService;
    }

    public void LogToFile(string message)
    {
        Guard.IsNotNull(GeneralOptions);

        string? logFile = GeneralOptions.LogFile ?? null;
        Guard.IsNotNullOrWhiteSpace(logFile);

        string? directoryPath = Path.GetDirectoryName(logFile);
        if (!string.IsNullOrEmpty(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);
        }

        bool logFileExist = File.Exists(logFile);
        if (!logFileExist)
        {
            File.Create(logFile).Close();
        }

        using (var lf = new StreamWriter(logFile, true, Encoding.Default))
        {
            lf.WriteLine("----------------------------------------------------------------");
            lf.WriteLine(DateFormatters.DateTimeNow() + ", " + message);
            lf.Flush();
            lf.Close();
        }
    }

    public void LogToDB(RestMethods operationType, string bucketPath, Guid guid)
    {
        Guard.IsNotNull(GeneralOptions);
        Guard.IsNotNull(GeneralOptions.CommandTimeout);

        string dbConnStr = _dbConStrService.GetDbConnectionString();

        using (SqlConnection connection = SqlCommands.Connect(dbConnStr))
        {
            try
            {
                var sqlParameters = new SqlParameter[]
                {
                     SqlCommands.CreateParameter("@operationType",  operationType.ToString()),
                     SqlCommands.CreateParameter("@bucketPath",     bucketPath + "\\" + guid)
                };

                using (SqlCommand cmd = SqlCommands.CreateCommand(connection, DOSERVICE_BCSFS_LOG_ADD, sqlParameters))
                {
                    cmd.CommandTimeout = GeneralOptions.CommandTimeout;
                    SqlCommands.ExecuteNonQuery(cmd);
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Ошибка при выполнении процедуры {DOSERVICE_BCSFS_LOG_ADD}: {ex}");
            }
        }
    }

    public void LogToConsole(string message)
    {
        throw new NotImplementedException();
    }
}
