using CommunityToolkit.Diagnostics;
using Microsoft.Extensions.Options;
using Npgsql;
using Timer.Infrastructure.Interfaces;
using Timer.Infrastructure.Options;

namespace Timer.Infrastructure.Repositories;

public class TimelineInfoRepository : ITimelineInfoRepository
{
    public static DatabaseOptions? DatabaseOptions { get; set; }

    public TimelineInfoRepository(IOptions<DatabaseOptions> databaseOptions)
    {
        DatabaseOptions = databaseOptions.Value;
    }

    public async Task<string> GetDatabaseVersionAsync()
    {
        Guard.IsNotNull(DatabaseOptions);

        using var connection = new NpgsqlConnection(DatabaseOptions.ConnectionString);

        await connection.OpenAsync();

        using var command = new NpgsqlCommand("SELECT version();", connection);

        var version = await command.ExecuteScalarAsync();

        return version?.ToString() ?? "No data";
    }

    public async Task AddTimerEntry()
    {
        try
        {
            Guard.IsNotNull(DatabaseOptions, $"{nameof(DatabaseOptions)} is null");

            using var connection = new NpgsqlConnection(DatabaseOptions.ConnectionString);

            await connection.OpenAsync();

            string sql = $"INSERT INTO timeline_info (birth_date, timeline_end_date) VALUES ('2024-03-09', '2025-03-09')";

            using var command = new NpgsqlCommand(sql, connection);

            //command.Parameters.AddWithValue("name", taskName);
            //command.Parameters.AddWithValue("duration", duration);

            await command.ExecuteNonQueryAsync();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}
