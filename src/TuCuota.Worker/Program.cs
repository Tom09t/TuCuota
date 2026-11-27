using TuCuota.Worker;
using TuCuota.BuildingBlocks.Observability;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddTuCuotaObservability(builder.Configuration, "TuCuota.Worker");
builder.Services.AddHostedService<Worker>();

var host = builder.Build();
host.Run();
