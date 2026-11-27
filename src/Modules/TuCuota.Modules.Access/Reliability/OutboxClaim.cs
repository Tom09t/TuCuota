namespace TuCuota.Modules.Access.Reliability;

public sealed record OutboxClaim(
    Guid MessageId,
    Guid ClubId,
    string MessageType,
    string Payload,
    int AttemptCount,
    string CorrelationId,
    Guid? CausationId);
