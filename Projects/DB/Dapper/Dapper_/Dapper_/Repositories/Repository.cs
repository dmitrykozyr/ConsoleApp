using Dapper;
using Dapper_.Interfaces;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Dapper_.Repositories;

public class Repository<TResult> : IRepository<TResult>
{
    private readonly string? _connectionString;

    public Repository()
    {
        _connectionString = GetConnectionString();
    }

    public async Task<IEnumerable<TResult>> CallProcedure(string procedureName, object? parameters = default)
    {
        try
        {
            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            IEnumerable<TResult> result = await connection.QueryAsync<TResult>(
                procedureName,
                parameters,
                commandType: CommandType.StoredProcedure,
                commandTimeout: 120); //! В конфиг

            return result;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    private string GetConnectionString()
    {
        //Guard.IsNotNullOrEmpty(DatabaseOptions.DbPassword);

        //string decryptedDbPassword = _encryptionService.Decrypt(DatabaseOptions.DbPassword);
        //string result = DatabaseOptions.ConnStr + decryptedDbPassword;

        //return result;

        string connectionString = "";

        return connectionString;
    }
}
