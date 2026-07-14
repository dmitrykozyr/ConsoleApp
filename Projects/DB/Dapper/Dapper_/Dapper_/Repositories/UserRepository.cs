using Dapper;
using Dapper_.Interfaces;
using Dapper_.Models;
using Npgsql;
using System.Data;

namespace Dapper_.Repositories;
    
public class UserRepository : IUsersRepository
{
    private readonly string? _connectionString;

    public UserRepository(IConfiguration configuration)
    {
        _connectionString = configuration?.GetConnectionString("DefaultConnection");
    }

    public async Task<IEnumerable<User>> GetAllAsync()
    {
        using var connection = CreateConnection();

        string sql = "SELECT Id, Name, Age FROM Users";

        var result = await connection.QueryAsync<User>(sql);

        return result;
    }

    public async Task<User?> GetByIdAsync(int id)
    {
        using var connection = CreateConnection();

        string sql = """
                     SELECT Id, Name, Age
                     FROM Users
                     WHERE Id = @Id
                     """;

        var result = await connection.QueryFirstOrDefaultAsync<User>(sql, new { Id = id });

        return result;
    }

    public async Task<int> AddAsync(User user)
    {
        using var connection = CreateConnection();

        string sql = """
                     INSERT INTO Users(Id, Name, Age)
                     VALUES (@Id, @Name, @Age);
                     """;

        var result = await connection.ExecuteScalarAsync<int>(sql, user);

        return result;
    }

    public async Task UpdateAsync(User user)
    {
        using var connection = CreateConnection();

        string sql = """
                     UPDATE Users
                     SET Name = @Name,
                         Age = @Age
                     WHERE Id = @Id
                     """;

        await connection.ExecuteAsync(sql, user);
    }

    public async Task DeleteAsync(int id)
    {
        using var connection = CreateConnection();

        await connection.ExecuteAsync(
            "DELETE FROM Users WHERE Id = @Id",
            new { Id = id });
    }

    private IDbConnection CreateConnection()
    {
        // NpgsqlConnection для PostgreSQL
        // Connection       для MSSQL
        var result = new NpgsqlConnection(_connectionString);
        return result;
    }
}
