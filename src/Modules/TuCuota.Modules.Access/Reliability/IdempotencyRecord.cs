namespace TuCuota.Modules.Access.Reliability;

public sealed class IdempotencyRecord
{
    public Guid Id { get; set; }
    public Guid ClubId { get; set; }
    public required string OperationScope { get; set; }
    public required string Key { get; set; }
    public required string RequestHash { get; set; }
    public required string Status { get; set; }
    public string? ResourceReference { get; set; }
    public string? ResponseReference { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset ExpiresAt { get; set; }
    public required string CorrelationId { get; set; }
    public Guid? CausationId { get; set; }
}
