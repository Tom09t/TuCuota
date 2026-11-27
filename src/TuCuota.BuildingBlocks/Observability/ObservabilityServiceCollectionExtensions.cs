using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OpenTelemetry;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

namespace TuCuota.BuildingBlocks.Observability;

public static class ObservabilityServiceCollectionExtensions
{
    public static OpenTelemetryBuilder AddTuCuotaObservability(
        this IServiceCollection services,
        IConfiguration configuration,
        string serviceName)
    {
        var builder = services
            .AddOpenTelemetry()
            .ConfigureResource(resource => resource.AddService(serviceName))
            .WithTracing(tracing => tracing
                .AddSource(TuCuotaTelemetry.ActivitySourceName)
                .AddSource("Npgsql")
                .AddHttpClientInstrumentation())
            .WithMetrics(metrics => metrics
                .AddMeter(TuCuotaTelemetry.MeterName)
                .AddHttpClientInstrumentation()
                .AddRuntimeInstrumentation());

        if (!string.IsNullOrWhiteSpace(configuration["OTEL_EXPORTER_OTLP_ENDPOINT"]))
        {
            builder.UseOtlpExporter();
        }

        return builder;
    }
}
