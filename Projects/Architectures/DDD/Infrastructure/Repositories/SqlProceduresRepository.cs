using CommunityToolkit.Diagnostics;
using Domain.Interfaces;
using Domain.Interfaces.Repositories;
using Domain.Models.ResponseModels;
using System.Data;
using System.Data.SqlClient;

namespace Infrastructure.Repositories;

public class SqlProceduresRepository : ISqlProceduresRepository
{
    private readonly IDbConStrService _dbConStrService;
    private readonly ILogging _logging;

    public SqlProceduresRepository(IDbConStrService dbConStrService, ILogging logging)
    {
        _dbConStrService = dbConStrService;
        _logging = logging;
    }

    public DbDataResponseModel GetDbDataDictionaryLongString(string storeProcedureName)
    {
        var result = new DbDataResponseModel
        {
            DbData = new Dictionary<long, string>()
        };

        try
        {
            string? dbConnStr = _dbConStrService.GetDbConnectionString();

            if (dbConnStr is null)
            {
                Guard.IsNotNull(dbConnStr, "Не удалось получить строку подключения к БД. Возможно, строка не обогащена паролем");
            }

            using (SqlConnection connection = SqlCommands.Connect(dbConnStr))
            {
                Guard.IsNotNull(connection, "Не удалось создать экземпляр SqlConnection");

                using SqlCommand command = connection.CreateCommand();

                try
                {
                    Guard.IsNotNull(command, "Не удалось создать экземпляр SqlCommand");

                    command.CommandText = storeProcedureName;
                    command.CommandType = CommandType.StoredProcedure;

                    using SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        result.DbData.Add(reader.GetInt64(0), reader.GetString(1));
                    }
                }
                catch (Exception ex)
                {
                    string errorMessage = $"Не удалось получить Dictionary<long, string> из БД, {ex.Message}";

                    if (string.IsNullOrEmpty(result.ErrorMessage))
                    {
                        result.ErrorMessage = errorMessage;
                    }

                    _logging.LogToFile(errorMessage);
                }
            }

            return result;
        }
        catch (Exception ex)
        {
            string errorMessage = $"Не удалось получить Dictionary<long, string> из БД, {ex.Message}";

            if (string.IsNullOrEmpty(result.ErrorMessage))
            {
                result.ErrorMessage = errorMessage;
            }

            _logging.LogToFile(errorMessage);

            return result;
        }
    }

    public List<string> GetDbDataListString(string storeProcedureName)
    {
        var result = new List<string>();

        try
        {
            string? dbConnStr = _dbConStrService.GetDbConnectionString();

            if (dbConnStr is null)
            {
                Guard.IsNotNull(dbConnStr, "Не удалось получить строку подключения к БД. Возможно, строка не обогащена паролем");
            }

            using (SqlConnection connection = SqlCommands.Connect(dbConnStr))
            {
                Guard.IsNotNull(connection, "Не удалось создать экземпляр SqlConnection");

                using SqlCommand command = connection.CreateCommand();

                try
                {
                    Guard.IsNotNull(command, "Не удалось создать экземпляр SqlCommand");

                    command.CommandText = storeProcedureName;
                    command.CommandType = CommandType.StoredProcedure;

                    using SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        result.Add(reader.GetString(0));
                    }
                }
                catch (Exception ex)
                {
                    string errorMessage = $"Не удалось получить List<string>, {ex.Message}";
                    _logging.LogToFile(errorMessage);
                }
            }

            return result;
        }
        catch (Exception ex)
        {
            string errorMessage = $"Не удалось получить List<string>, {ex.Message}";
            _logging.LogToFile(errorMessage);

            return result;
        }
    }
}
