using Npgsql;
using Timer.Infrastructure.Interfaces;

namespace Timer.Infrastructure.Repositories;

public class TimelineInfoRepository : ITimelineInfoRepository
{
    //! IOption
    private readonly string _connectionString;

    public TimelineInfoRepository()
    {
        // Берем строку подключения из конфигурации
        //_connectionString = configuration.GetConnectionString("PostgresDb");
    }

    public async Task<string> GetDatabaseVersionAsync()
    {
        using var connection = new NpgsqlConnection("Host=localhost;Database=timer;Username=postgres;Password=admin");

        await connection.OpenAsync();

        using var command = new NpgsqlCommand("SELECT version();", connection);

        var version = await command.ExecuteScalarAsync();

        return version?.ToString() ?? "No data";
    }

    public async Task AddTimerEntry(string taskName, int duration)
    {
        using var connection = new NpgsqlConnection(_connectionString);

        await connection.OpenAsync();

        const string sql = "INSERT INTO tasks (name, duration) VALUES (@name, @duration)";

        using var command = new NpgsqlCommand(sql, connection);

        command.Parameters.AddWithValue("name", taskName);
        command.Parameters.AddWithValue("duration", duration);

        await command.ExecuteNonQueryAsync();
    }
}
