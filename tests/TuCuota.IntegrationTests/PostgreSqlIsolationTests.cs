using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System.Net.Http.Json;
using Testcontainers.PostgreSql;
using TuCuota.Api.Contracts;
using TuCuota.BuildingBlocks.Tenancy;
using TuCuota.Modules.Access.Persistence;

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
            .WithWebHostBuilder(builder => builder.ConfigureAppConfiguration((_, configuration) =>
                configuration.AddInMemoryCollection(new Dictionary<string, string?>
                {
                    ["ConnectionStrings:PostgreSql"] = _postgres.GetConnectionString(),
                })));
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
