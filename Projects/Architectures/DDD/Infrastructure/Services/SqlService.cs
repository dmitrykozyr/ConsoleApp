using CommunityToolkit.Diagnostics;
using DDD.Domain.Enums;
using Domain.Interfaces;
using Domain.Models.Options;
using Infrastructure.Interfaces;
using Infrastructure.Interfaces.Db;
using Infrastructure.Services.Login;
using Microsoft.Extensions.Options;
using System.Data;
using System.Data.SqlClient;

namespace Infrastructure.Services;

public class SqlService : ISqlService
{
    private readonly DatabaseOptions? DatabaseOptions;

    private readonly IDbConStrService _dbConStrService;
    private readonly ILoggingService _logging;

    public SqlService(IOptions<DatabaseOptions> databaseOptions, IDbConStrService dbConStrService, ILoggingService logging)
    {
        DatabaseOptions = databaseOptions.Value;

        _dbConStrService = dbConStrService;
        _logging = logging;
    }

    public async Task<SqlConnection?> CreateConnection()
    {
        Guard.IsNotNull(DatabaseOptions);

        SqlConnection? conn = null;

        try
        {
            string dbConnStr = _dbConStrService.GetDbConnectionString();

            conn = new SqlConnection(dbConnStr);

            await conn.OpenAsync();

            return conn;
        }
        catch (Exception ex)
        {
            await conn?.CloseAsync();

            await _logging.LogToFile(LoggingTypes.Error, "Ошибка в SqlConnection: " + ex.Message);

            return null;
        }
    }

    public IUserContextCommand CreateCommand(string commandText, CommandType commandType)
    {
        int commandTimeout = DatabaseOptions?.SqlCommandTimeout ?? 0;

        return new UserContextCommand(commandText, commandType, commandTimeout, this);
    }
}
