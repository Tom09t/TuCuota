using Microsoft.EntityFrameworkCore;
using TuCuota.BuildingBlocks.Tenancy;
using TuCuota.Modules.Access.Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHealthChecks();
builder.Services.AddScoped(_ => TenantContext.ForClub(Guid.Parse("11111111-1111-1111-1111-111111111111")));

var connectionString = builder.Configuration.GetConnectionString("PostgreSql");
if (!string.IsNullOrWhiteSpace(connectionString))
{
    builder.Services.AddDbContext<AccessDbContext>(options =>
        options.UseNpgsql(connectionString, npgsql =>
            npgsql.MigrationsHistoryTable("__ef_migrations_history", "access")));
}

var app = builder.Build();

app.MapHealthChecks("/health");
app.MapGet("/api/system", () => Results.Ok(new { service = "TuCuota.Api", status = "ready" }));
if (!string.IsNullOrWhiteSpace(connectionString))
{
    app.MapGet("/api/system/persistence", async (AccessDbContext db, CancellationToken cancellationToken) =>
    {
        var canConnect = await db.Database.CanConnectAsync(cancellationToken);
        return Results.Ok(new { provider = "PostgreSQL", schema = "access", canConnect });
    });
}

app.Run();

public partial class Program;
