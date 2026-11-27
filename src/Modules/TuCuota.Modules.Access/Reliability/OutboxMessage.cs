namespace TuCuota.Modules.Access.Reliability;

public sealed class OutboxMessage
{
    public Guid MessageId { get; set; }
    public Guid ClubId { get; set; }
    public required string MessageType { get; set; }
    public required string Payload { get; set; }
    public DateTimeOffset OccurredAt { get; set; }
    public DateTimeOffset AvailableAt { get; set; }
    public int AttemptCount { get; set; }
    public DateTimeOffset? ProcessedAt { get; set; }
    public string? LeaseOwner { get; set; }
    public DateTimeOffset? LeaseExpiresAt { get; set; }
    public required string CorrelationId { get; set; }
    public Guid? CausationId { get; set; }
}
