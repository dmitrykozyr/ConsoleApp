using CommunityToolkit.Diagnostics;
using Domain.Interfaces;
using Domain.Models.Options;
using Domain.Services.Login;
using Microsoft.Extensions.Options;
using System.Data;

namespace Domain.Services;

public class SqlService : ISqlService //!
{
    private readonly DatabaseOptions? DatabaseOptions;

    private readonly IDbConStrService _dbConStrService;
    private readonly ILogging _logging;

    public SqlService(IOptions<DatabaseOptions> databaseOptions, IDbConStrService dbConStrService, ILogging logging)
    {
        DatabaseOptions = databaseOptions.Value;

        _dbConStrService = dbConStrService;
        _logging = logging;
    }

    public SqlConnection? CreateConnection()
    {
        Guard.IsNotNull(DatabaseOptions);

        SqlConnection? conn = null;

        try
        {
            string dbConnStr = _dbConStrService.GetDbConnectionString();

            conn = new SqlConnection(dbConnStr);

            conn.Open();

            return conn;
        }
        catch (Exception ex)
        {
            conn?.Close();

            _logging.LogToFile("Ошибка в SqlConnection: " + ex.Message);

            return null;
        }
    }

    public UserContextCommand CreateCommand(string commandText, CommandType commandType)
    {
        int commandTimeout = int.Parse(DatabaseOptions?.SqlCommandTimeout ?? "");

        var result = new UserContextCommand(commandText, commandType, commandTimeout, this);

        return result;
    }
}
