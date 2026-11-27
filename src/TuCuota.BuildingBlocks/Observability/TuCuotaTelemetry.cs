using System.Diagnostics;
using System.Diagnostics.Metrics;

namespace TuCuota.BuildingBlocks.Observability;

public static class TuCuotaTelemetry
{
    public const string ActivitySourceName = "TuCuota";
    public const string MeterName = "TuCuota";

    public static readonly ActivitySource ActivitySource = new(ActivitySourceName);
    public static readonly Meter Meter = new(MeterName);
    public static readonly Counter<long> Requests = Meter.CreateCounter<long>(
        "tucuota.requests",
        description: "Technical requests processed by TuCuota hosts.");
    public static readonly Histogram<double> RequestDuration = Meter.CreateHistogram<double>(
        "tucuota.request.duration",
        unit: "ms",
        description: "Technical request duration in milliseconds.");
    public static readonly Counter<long> WorkerHeartbeats = Meter.CreateCounter<long>(
        "tucuota.worker.heartbeats",
        description: "Worker heartbeat executions.");
}
