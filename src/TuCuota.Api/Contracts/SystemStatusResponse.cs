namespace TuCuota.Api.Contracts;

public sealed record SystemStatusResponse(string Service, string Status, string Persistence);

public sealed record PersistenceStatusResponse(
    string Provider,
    string Schema,
    bool Configured,
    bool? CanConnect);

public sealed record HealthStatusResponse(string Status, IReadOnlyDictionary<string, string> Checks);
