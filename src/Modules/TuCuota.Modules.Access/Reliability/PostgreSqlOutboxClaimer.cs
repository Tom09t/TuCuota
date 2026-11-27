using Npgsql;
using TuCuota.BuildingBlocks.Tenancy;

namespace TuCuota.Modules.Access.Reliability;

public sealed class PostgreSqlOutboxClaimer(string connectionString, TenantContext tenant)
{
    public async Task<IReadOnlyList<OutboxClaim>> ClaimAsync(
        string workerId,
        int maximumCount,
        TimeSpan leaseDuration,
        CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(workerId);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(maximumCount);
        ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(leaseDuration, TimeSpan.Zero);

        await using var connection = new NpgsqlConnection(connectionString);
        await connection.OpenAsync(cancellationToken);
        await using var transaction = await connection.BeginTransactionAsync(cancellationToken);

        await using (var setTenant = new NpgsqlCommand(
            "SELECT set_config('app.club_id', $1, true)",
            connection,
            transaction))
        {
            setTenant.Parameters.AddWithValue(tenant.ClubId.ToString());
            await setTenant.ExecuteNonQueryAsync(cancellationToken);
        }

        await using var command = new NpgsqlCommand("""
            WITH candidates AS (
                SELECT "MessageId"
                FROM access.outbox_messages
                WHERE "ClubId" = $1
                  AND "ProcessedAt" IS NULL
                  AND "AvailableAt" <= CURRENT_TIMESTAMP
                  AND ("LeaseExpiresAt" IS NULL OR "LeaseExpiresAt" <= CURRENT_TIMESTAMP)
                ORDER BY "AvailableAt", "OccurredAt", "MessageId"
                FOR UPDATE SKIP LOCKED
                LIMIT $2
            )
            UPDATE access.outbox_messages AS message
            SET "LeaseOwner" = $3,
                "LeaseExpiresAt" = CURRENT_TIMESTAMP + $4,
                "AttemptCount" = message."AttemptCount" + 1
            FROM candidates
            WHERE message."MessageId" = candidates."MessageId"
            RETURNING message."MessageId", message."ClubId", message."MessageType",
                      message."Payload"::text, message."AttemptCount",
                      message."CorrelationId", message."CausationId"
            """, connection, transaction);
        command.Parameters.AddWithValue(tenant.ClubId);
        command.Parameters.AddWithValue(maximumCount);
        command.Parameters.AddWithValue(workerId);
        command.Parameters.AddWithValue(leaseDuration);

        var claims = new List<OutboxClaim>();
        await using var reader = await command.ExecuteReaderAsync(cancellationToken);
        while (await reader.ReadAsync(cancellationToken))
        {
            claims.Add(new OutboxClaim(
                reader.GetGuid(0),
                reader.GetGuid(1),
                reader.GetString(2),
                reader.GetString(3),
                reader.GetInt32(4),
                reader.GetString(5),
                reader.IsDBNull(6) ? null : reader.GetGuid(6)));
        }

        await reader.CloseAsync();
        await transaction.CommitAsync(cancellationToken);
        return claims;
    }
}
