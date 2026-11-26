var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHealthChecks();

var app = builder.Build();

app.MapHealthChecks("/health");
app.MapGet("/api/system", () => Results.Ok(new { service = "TuCuota.Api", status = "ready" }));

app.Run();

public partial class Program;
