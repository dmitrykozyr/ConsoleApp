using CommunityToolkit.Diagnostics;
using Domain.Domain_Services.Login;
using Domain.Interfaces;
using Domain.Interfaces.Db;
using Domain.Models.Options;
using Microsoft.Extensions.Options;
using System.Data;
using System.Data.SqlClient;

namespace Infrastructure.Services;

public class SqlService : ISqlService
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

    //! Пакет System.Data.SqlClient устарел, следует заменить на Microsoft.Data.SqlClient
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
