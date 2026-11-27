using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.OpenApi;
using OpenTelemetry.Metrics;
using OpenTelemetry.Trace;
using System.Text.Json;
using TuCuota.Api.Contracts;
using TuCuota.Api.Health;
using TuCuota.Api.Observability;
using TuCuota.BuildingBlocks.Observability;
using TuCuota.BuildingBlocks.Tenancy;
using TuCuota.Modules.Access.Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi(options => options.OpenApiVersion = OpenApiSpecVersion.OpenApi3_1);
builder.Services
    .AddTuCuotaObservability(builder.Configuration, "TuCuota.Api")
    .WithTracing(tracing => tracing.AddAspNetCoreInstrumentation())
    .WithMetrics(metrics => metrics.AddAspNetCoreInstrumentation());
var healthChecks = builder.Services.AddHealthChecks();
builder.Services.AddScoped(_ => TenantContext.ForClub(Guid.Parse("11111111-1111-1111-1111-111111111111")));

var connectionString = builder.Configuration.GetConnectionString("PostgreSql");
if (!string.IsNullOrWhiteSpace(connectionString))
{
    builder.Services.AddDbContext<AccessDbContext>(options =>
        options.UseNpgsql(connectionString, npgsql =>
            npgsql.MigrationsHistoryTable("__ef_migrations_history", "access")));
    healthChecks.AddCheck<PostgreSqlReadinessHealthCheck>("postgresql", tags: ["ready"]);
}

var app = builder.Build();

app.UseMiddleware<CorrelationMiddleware>();
app.MapOpenApi();
app.MapHealthChecks("/health/live", new HealthCheckOptions
{
    Predicate = _ => false,
    ResponseWriter = WriteHealthResponseAsync,
});
app.MapHealthChecks("/health/ready", new HealthCheckOptions
{
    Predicate = check => check.Tags.Contains("ready"),
    ResponseWriter = WriteHealthResponseAsync,
});
app.MapGet("/api/system", () => Results.Ok(new SystemStatusResponse(
        "TuCuota.Api",
        "ready",
        string.IsNullOrWhiteSpace(connectionString) ? "not-configured" : "configured")))
    .WithName("GetSystemStatus")
    .Produces<SystemStatusResponse>();
app.MapGet("/api/system/persistence", async (IServiceProvider services, CancellationToken cancellationToken) =>
{
    if (string.IsNullOrWhiteSpace(connectionString))
    {
        return Results.Ok(new PersistenceStatusResponse("PostgreSQL", "access", false, null));
    }

    var db = services.GetRequiredService<AccessDbContext>();
    var canConnect = await db.Database.CanConnectAsync(cancellationToken);
    return Results.Ok(new PersistenceStatusResponse("PostgreSQL", "access", true, canConnect));
})
    .WithName("GetPersistenceStatus")
    .Produces<PersistenceStatusResponse>();

app.Run();

static Task WriteHealthResponseAsync(HttpContext context, Microsoft.Extensions.Diagnostics.HealthChecks.HealthReport report)
{
    context.Response.ContentType = "application/json";
    var response = new HealthStatusResponse(
        report.Status.ToString().ToLowerInvariant(),
        report.Entries.ToDictionary(
            entry => entry.Key,
            entry => entry.Value.Status.ToString().ToLowerInvariant()));
    return context.Response.WriteAsync(JsonSerializer.Serialize(response, JsonSerializerOptions.Web));
}

public partial class Program;
