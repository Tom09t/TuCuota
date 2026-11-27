namespace TuCuota.Modules.Access.Reliability;

public sealed class InboxMessage
{
    public required string Consumer { get; set; }
    public Guid MessageId { get; set; }
    public Guid ClubId { get; set; }
    public required string PayloadHash { get; set; }
    public DateTimeOffset ReceivedAt { get; set; }
    public DateTimeOffset? ProcessedAt { get; set; }
    public int AttemptCount { get; set; }
    public string? ResultReference { get; set; }
    public required string CorrelationId { get; set; }
}
