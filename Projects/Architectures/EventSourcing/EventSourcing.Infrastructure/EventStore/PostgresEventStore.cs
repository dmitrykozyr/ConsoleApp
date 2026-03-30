using EventSourcing.Domain.Interfaces;
using Npgsql;
using NpgsqlTypes;

namespace EventSourcing.Infrastructure.EventStore;

public sealed class PostgresEventStore : IEventStore
{
    private readonly string _connectionString;

    public PostgresEventStore(string connectionString)
    {
        _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
    }

    public async Task AppendAsync(Guid aggregateId, IReadOnlyList<PersistedEvent> events, CancellationToken cancellationToken = default)
    {
        await using var connection = new NpgsqlConnection(_connectionString);

        await connection.OpenAsync(cancellationToken);

        const string sql = """
            INSERT INTO events (aggregate_id, event_type, event_data)
            VALUES (@aggregate_id, @event_type, @event_data::jsonb)
            """;

        foreach (var e in events)
        {
            await using var cmd = new NpgsqlCommand(sql, connection);

            cmd.Parameters.AddWithValue("aggregate_id", aggregateId);
            cmd.Parameters.AddWithValue("event_type", e.EventType);
            cmd.Parameters.Add(new NpgsqlParameter("event_data", NpgsqlDbType.Jsonb) { Value = e.PayloadJson });

            await cmd.ExecuteNonQueryAsync(cancellationToken);
        }
    }

    public async Task<IReadOnlyList<PersistedEvent>> ReadStreamAsync(Guid aggregateId, CancellationToken cancellationToken = default)
    {
        var result = new List<PersistedEvent>();

        await using var connection = new NpgsqlConnection(_connectionString);

        await connection.OpenAsync(cancellationToken);

        const string sql = """
            SELECT event_type, event_data::text
            FROM events
            WHERE aggregate_id = @aggregate_id
            ORDER BY id
            """;

        await using var cmd = new NpgsqlCommand(sql, connection);

        cmd.Parameters.AddWithValue("aggregate_id", aggregateId);

        await using var reader = await cmd.ExecuteReaderAsync(cancellationToken);

        while (await reader.ReadAsync(cancellationToken))
        {
            var type = reader.GetString(0);

            var data = reader.GetString(1);

            result.Add(new PersistedEvent(type, data));
        }

        return result;
    }
}
