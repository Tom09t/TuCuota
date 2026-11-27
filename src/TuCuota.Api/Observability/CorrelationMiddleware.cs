using System.Diagnostics;
using System.Globalization;
using System.Text.RegularExpressions;
using TuCuota.BuildingBlocks.Observability;

namespace TuCuota.Api.Observability;

public sealed partial class CorrelationMiddleware(RequestDelegate next, ILogger<CorrelationMiddleware> logger)
{
    public const string HeaderName = "X-Correlation-ID";

    public async Task InvokeAsync(HttpContext context)
    {
        var correlationId = ResolveCorrelationId(context);
        context.TraceIdentifier = correlationId;
        context.Response.OnStarting(() =>
        {
            context.Response.Headers[HeaderName] = correlationId;
            return Task.CompletedTask;
        });

        var started = Stopwatch.GetTimestamp();
        using var scope = logger.BeginScope(new Dictionary<string, object>
        {
            ["CorrelationId"] = correlationId,
            ["TraceId"] = Activity.Current?.TraceId.ToString() ?? string.Empty,
        });

        try
        {
            await next(context);
        }
        finally
        {
            TuCuotaTelemetry.Requests.Add(1, new KeyValuePair<string, object?>("service", "TuCuota.Api"));
            TuCuotaTelemetry.RequestDuration.Record(
                Stopwatch.GetElapsedTime(started).TotalMilliseconds,
                new KeyValuePair<string, object?>("service", "TuCuota.Api"));
            var endpointName = context.GetEndpoint()?.DisplayName ?? "unmatched";
            RequestCompleted(logger, context.Request.Method, endpointName, context.Response.StatusCode, correlationId);
        }
    }

    private static string ResolveCorrelationId(HttpContext context)
    {
        var supplied = context.Request.Headers[HeaderName].FirstOrDefault();
        if (!string.IsNullOrWhiteSpace(supplied) && SafeCorrelationId().IsMatch(supplied))
        {
            return supplied;
        }

        return Activity.Current?.TraceId.ToString() ?? Guid.NewGuid().ToString("N", CultureInfo.InvariantCulture);
    }

    [GeneratedRegex("^[A-Za-z0-9._-]{1,64}$", RegexOptions.CultureInvariant)]
    private static partial Regex SafeCorrelationId();

    [LoggerMessage(1001, LogLevel.Information, "HTTP {Method} {Endpoint} completed with {StatusCode}; correlation {CorrelationId}")]
    private static partial void RequestCompleted(
        ILogger logger,
        string method,
        string endpoint,
        int statusCode,
        string correlationId);
}
