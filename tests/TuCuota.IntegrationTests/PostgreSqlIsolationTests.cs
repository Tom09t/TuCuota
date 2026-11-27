using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Npgsql;
using System.Net.Http.Json;
using Testcontainers.PostgreSql;
using TuCuota.Api.Contracts;
using TuCuota.BuildingBlocks.Tenancy;
using TuCuota.Modules.Access.Persistence;
using TuCuota.Modules.Access.Reliability;

namespace TuCuota.IntegrationTests;

public sealed class PostgreSqlIsolationTests : IAsyncLifetime
{
    private const string ApplicationRole = "tucuota_app";
    private const string ApplicationPassword = "integration-only";
    private static readonly Guid ClubA = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa");
    private static readonly Guid ClubB = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb");

    private readonly PostgreSqlContainer _postgres = new PostgreSqlBuilder("postgres:18.7-alpine")
        .WithDatabase("tucuota")
        .WithUsername("postgres")
        .WithPassword("integration-admin-only")
        .Build();

    public async ValueTask InitializeAsync()
    {
        await _postgres.StartAsync();

        await using var db = CreateDbContext(_postgres.GetConnectionString(), ClubA);
        await db.Database.MigrateAsync();

        await db.Database.ExecuteSqlRawAsync($$"""
            CREATE ROLE {{ApplicationRole}} LOGIN PASSWORD '{{ApplicationPassword}}' NOSUPERUSER NOCREATEDB NOCREATEROLE NOINHERIT;
            GRANT USAGE ON SCHEMA access TO {{ApplicationRole}};
            GRANT SELECT, INSERT, UPDATE, DELETE ON access.tenant_boundary_probes TO {{ApplicationRole}};
            GRANT SELECT, INSERT, UPDATE, DELETE ON access.outbox_messages TO {{ApplicationRole}};
            GRANT SELECT, INSERT, UPDATE, DELETE ON access.inbox_messages TO {{ApplicationRole}};
            GRANT SELECT, INSERT, UPDATE, DELETE ON access.idempotency_records TO {{ApplicationRole}};
            """);
    }

    public async ValueTask DisposeAsync() => await _postgres.DisposeAsync();

    [Fact]
    [Trait("SPEC", "SPEC-001")]
    [Trait("AC", "AC-001-01")]
    public async Task RlsDeniesCrossClubReadsAndWrites()
    {
        await InsertProbeAsync(ClubA, ClubA);

        Assert.Equal(1, await CountVisibleProbesAsync(ClubA));
        Assert.Equal(0, await CountVisibleProbesAsync(ClubB));

        var exception = await Assert.ThrowsAsync<PostgresException>(() => InsertProbeAsync(ClubB, ClubA));
        Assert.Equal(PostgresErrorCodes.InsufficientPrivilege, exception.SqlState);
    }

    [Fact]
    [Trait("ADR", "ADR-0003")]
    [Trait("ADR", "ADR-0013")]
    public async Task ApiReportsPostgreSqlReadinessThroughTheTechnicalContract()
    {
        await using var factory = new WebApplicationFactory<Program>()
            .WithWebHostBuilder(builder => builder.UseSetting(
                "ConnectionStrings:PostgreSql",
                _postgres.GetConnectionString()));
        using var client = factory.CreateClient();

        var cancellationToken = TestContext.Current.CancellationToken;
        var persistence = await client.GetFromJsonAsync<PersistenceStatusResponse>(
            "/api/system/persistence",
            cancellationToken);
        var readiness = await client.GetFromJsonAsync<HealthStatusResponse>(
            "/health/ready",
            cancellationToken);

        Assert.NotNull(persistence);
        Assert.True(persistence.Configured);
        Assert.True(persistence.CanConnect);
        Assert.Equal("access", persistence.Schema);
        Assert.NotNull(readiness);
        Assert.Equal("healthy", readiness.Status);
        Assert.Equal("healthy", readiness.Checks["postgresql"]);
    }

    [Fact]
    [Trait("ADR", "ADR-0011")]
    [Trait("ADR", "ADR-0012")]
    public async Task ConcurrentOutboxClaimersReceiveDifferentMessages()
    {
        var cancellationToken = TestContext.Current.CancellationToken;
        await InsertOutboxMessageAsync(Guid.NewGuid());
        await InsertOutboxMessageAsync(Guid.NewGuid());

        var firstClaimer = new PostgreSqlOutboxClaimer(
            ApplicationConnectionString(),
            TenantContext.ForClub(ClubA));
        var secondClaimer = new PostgreSqlOutboxClaimer(
            ApplicationConnectionString(),
            TenantContext.ForClub(ClubA));

        var claims = await Task.WhenAll(
            firstClaimer.ClaimAsync("worker-a", 1, TimeSpan.FromMinutes(1), cancellationToken),
            secondClaimer.ClaimAsync("worker-b", 1, TimeSpan.FromMinutes(1), cancellationToken));

        Assert.Single(claims[0]);
        Assert.Single(claims[1]);
        Assert.NotEqual(claims[0][0].MessageId, claims[1][0].MessageId);
        Assert.All(claims.SelectMany(claim => claim), claim => Assert.Equal(1, claim.AttemptCount));
    }

    [Fact]
    [Trait("ADR", "ADR-0011")]
    public async Task InboxAndIdempotencyConstraintsRejectDuplicates()
    {
        var messageId = Guid.NewGuid();
        await InsertInboxMessageAsync(messageId);
        var inboxConflict = await Assert.ThrowsAsync<PostgresException>(() => InsertInboxMessageAsync(messageId));
        Assert.Equal(PostgresErrorCodes.UniqueViolation, inboxConflict.SqlState);

        await InsertIdempotencyRecordAsync(Guid.NewGuid(), "technical-probe");
        var idempotencyConflict = await Assert.ThrowsAsync<PostgresException>(
            () => InsertIdempotencyRecordAsync(Guid.NewGuid(), "technical-probe"));
        Assert.Equal(PostgresErrorCodes.UniqueViolation, idempotencyConflict.SqlState);
    }

    [Fact]
    [Trait("ADR", "ADR-0011")]
    public async Task ExplicitVersionRejectsStaleUpdates()
    {
        var cancellationToken = TestContext.Current.CancellationToken;
        var probeId = Guid.NewGuid();
        await using (var setup = CreateDbContext(_postgres.GetConnectionString(), ClubA))
        {
            setup.TenantBoundaryProbes.Add(new TenantBoundaryProbe(probeId, ClubA, DateTimeOffset.UtcNow));
            await setup.SaveChangesAsync(cancellationToken);
        }

        await using var first = CreateDbContext(_postgres.GetConnectionString(), ClubA);
        await using var second = CreateDbContext(_postgres.GetConnectionString(), ClubA);
        var firstCopy = await first.TenantBoundaryProbes.SingleAsync(
            probe => probe.Id == probeId,
            cancellationToken);
        var staleCopy = await second.TenantBoundaryProbes.SingleAsync(
            probe => probe.Id == probeId,
            cancellationToken);

        firstCopy.Touch(DateTimeOffset.UtcNow.AddMinutes(1));
        await first.SaveChangesAsync(cancellationToken);
        staleCopy.Touch(DateTimeOffset.UtcNow.AddMinutes(2));

        await Assert.ThrowsAsync<DbUpdateConcurrencyException>(
            () => second.SaveChangesAsync(cancellationToken));
    }

    private async Task InsertProbeAsync(Guid sessionClubId, Guid rowClubId)
    {
        await using var connection = new NpgsqlConnection(ApplicationConnectionString());
        await connection.OpenAsync();
        await using var transaction = await connection.BeginTransactionAsync();
        await SetTenantAsync(connection, transaction, sessionClubId);

        await using var command = new NpgsqlCommand(
            "INSERT INTO access.tenant_boundary_probes (\"Id\", \"ClubId\", \"CreatedAt\") VALUES ($1, $2, $3)",
            connection,
            transaction);
        command.Parameters.AddWithValue(Guid.NewGuid());
        command.Parameters.AddWithValue(rowClubId);
        command.Parameters.AddWithValue(DateTimeOffset.UtcNow);
        await command.ExecuteNonQueryAsync();
        await transaction.CommitAsync();
    }

    private async Task InsertOutboxMessageAsync(Guid messageId)
    {
        await ExecuteTenantCommandAsync("""
            INSERT INTO access.outbox_messages
                ("MessageId", "ClubId", "MessageType", "Payload", "OccurredAt", "AvailableAt",
                 "AttemptCount", "CorrelationId")
            VALUES ($2, $1, 'technical.probe', '{}'::jsonb, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP, 0, 'r1-durable-test')
            """, messageId);
    }

    private async Task InsertInboxMessageAsync(Guid messageId)
    {
        await ExecuteTenantCommandAsync("""
            INSERT INTO access.inbox_messages
                ("Consumer", "MessageId", "ClubId", "PayloadHash", "ReceivedAt", "AttemptCount", "CorrelationId")
            VALUES ('technical-consumer', $2, $1, 'aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa',
                    CURRENT_TIMESTAMP, 0, 'r1-durable-test')
            """, messageId);
    }

    private async Task InsertIdempotencyRecordAsync(Guid id, string key)
    {
        await ExecuteTenantCommandAsync("""
            INSERT INTO access.idempotency_records
                ("Id", "ClubId", "OperationScope", "Key", "RequestHash", "Status", "CreatedAt", "ExpiresAt", "CorrelationId")
            VALUES ($2, $1, 'technical-operation', $3,
                    'bbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbb',
                    'started', CURRENT_TIMESTAMP, CURRENT_TIMESTAMP + INTERVAL '1 day', 'r1-durable-test')
            """, id, key);
    }

    private async Task ExecuteTenantCommandAsync(string sql, params object[] values)
    {
        await using var connection = new NpgsqlConnection(ApplicationConnectionString());
        await connection.OpenAsync();
        await using var transaction = await connection.BeginTransactionAsync();
        await SetTenantAsync(connection, transaction, ClubA);
        await using var command = new NpgsqlCommand(sql, connection, transaction);
        command.Parameters.AddWithValue(ClubA);
        foreach (var value in values)
        {
            command.Parameters.AddWithValue(value);
        }
        await command.ExecuteNonQueryAsync();
        await transaction.CommitAsync();
    }

    private async Task<long> CountVisibleProbesAsync(Guid sessionClubId)
    {
        await using var connection = new NpgsqlConnection(ApplicationConnectionString());
        await connection.OpenAsync();
        await using var transaction = await connection.BeginTransactionAsync();
        await SetTenantAsync(connection, transaction, sessionClubId);

        await using var command = new NpgsqlCommand(
            "SELECT COUNT(*) FROM access.tenant_boundary_probes",
            connection,
            transaction);
        return (long)(await command.ExecuteScalarAsync())!;
    }

    private static async Task SetTenantAsync(
        NpgsqlConnection connection,
        NpgsqlTransaction transaction,
        Guid clubId)
    {
        await using var command = new NpgsqlCommand(
            "SELECT set_config('app.club_id', $1, true)",
            connection,
            transaction);
        command.Parameters.AddWithValue(clubId.ToString());
        await command.ExecuteNonQueryAsync();
    }

    private string ApplicationConnectionString()
    {
        var builder = new NpgsqlConnectionStringBuilder(_postgres.GetConnectionString())
        {
            Username = ApplicationRole,
            Password = ApplicationPassword,
            Pooling = false,
        };
        return builder.ConnectionString;
    }

    private static AccessDbContext CreateDbContext(string connectionString, Guid clubId)
    {
        var options = new DbContextOptionsBuilder<AccessDbContext>()
            .UseNpgsql(connectionString, npgsql =>
                npgsql.MigrationsHistoryTable("__ef_migrations_history", "access"))
            .Options;
        return new AccessDbContext(options, TenantContext.ForClub(clubId));
    }
}
