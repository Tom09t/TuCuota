namespace TuCuota.Modules.Access.Persistence;

public sealed class TenantBoundaryProbe
{
    public Guid Id { get; private set; }
    public Guid ClubId { get; private set; }
    public DateTimeOffset CreatedAt { get; private set; }

    private TenantBoundaryProbe() { }

    public TenantBoundaryProbe(Guid id, Guid clubId, DateTimeOffset createdAt)
    {
        if (id == Guid.Empty) throw new ArgumentException("Id must not be empty.", nameof(id));
        if (clubId == Guid.Empty) throw new ArgumentException("ClubId must not be empty.", nameof(clubId));
        Id = id;
        ClubId = clubId;
        CreatedAt = createdAt;
    }
}
