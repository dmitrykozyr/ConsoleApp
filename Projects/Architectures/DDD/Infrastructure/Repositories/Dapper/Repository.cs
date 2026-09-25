using CommunityToolkit.Diagnostics;
using Dapper;
using DDD.Domain.Enums;
using DDD.Infrastructure.Interfaces.Db.Dapper;
using Domain.Interfaces;
using Domain.Models.Options;
using Microsoft.Extensions.Options;
using System.Data;
using System.Data.SqlClient;

namespace DDD.Infrastructure.Repositories.Dapper;

public class Repository<TResult> : IRepository<TResult>
{
    private readonly ILoggingService _logging;

    private readonly DatabaseOptions DatabaseOptions;

    private readonly string? _connectionStringGeneral;
    private readonly string? _connectionStringBuffer;

    public Repository(
     ILoggingService logging,
     IOptions<DatabaseOptions> databaseOptions)
    {
        _logging = logging;

        DatabaseOptions = databaseOptions.Value;

        Guard.IsNotNullOrWhiteSpace(DatabaseOptions.ConnectionString);
        Guard.IsNotNullOrWhiteSpace(DatabaseOptions.DbPassword);
        Guard.IsNotNullOrWhiteSpace(DatabaseOptions.ConnStrBuffer);
        Guard.IsNotNullOrWhiteSpace(DatabaseOptions.DbPasswordBuffer);

        //!
        //_connectionStringGeneral = GetConnectionString(
        //     DatabaseOptions.ConnectionString,
        //     DatabaseOptions.DbPassword);

        //_connectionStringBuffer = GetConnectionString(
        //     DatabaseOptions.ConnStrBuffer,
        //     DatabaseOptions.DbPasswordBuffer);
    }

    public async Task<IEnumerable<TResult>?> CallProcedure(string procedureName, object? parameters, DatabaseType databaseType)
    {
        try
        {
            string connectionString = GetConnectionStringForCurrentDb(databaseType);

            using var connection = new SqlConnection(connectionString);
            await connection.OpenAsync();

            IEnumerable<TResult> result = await connection.QueryAsync<TResult>(
             procedureName,
             parameters,
             commandType: CommandType.StoredProcedure,
             commandTimeout: DatabaseOptions.SqlCommandTimeout
             );

            return result;
        }
        catch (Exception ex)
        {
            await _logging.LogToFile(LoggingTypes.Error, $"Ошибка вызова процедуры {procedureName}: {ex}");

#if DEBUG
            throw;
#else
    return default;
#endif
        }
    }

    public string GetConnectionStringForCurrentDb(DatabaseType databaseType)
    {
        string? connectionString = databaseType switch
        {
            DatabaseType.General => _connectionStringGeneral,
            DatabaseType.Buffer => _connectionStringBuffer,
            _ => default
        };

        Guard.IsNotNull(connectionString);
        return connectionString;
    }
}
