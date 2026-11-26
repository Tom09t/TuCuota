namespace TuCuota.Worker;

public partial class Worker(ILogger<Worker> logger) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            WorkerHeartbeat(logger, DateTimeOffset.UtcNow);
            await Task.Delay(1000, stoppingToken);
        }
    }

    [LoggerMessage(LogLevel.Information, "Worker heartbeat at {Timestamp}")]
    private static partial void WorkerHeartbeat(ILogger logger, DateTimeOffset timestamp);
}
