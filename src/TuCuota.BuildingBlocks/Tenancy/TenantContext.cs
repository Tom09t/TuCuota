namespace TuCuota.BuildingBlocks.Tenancy;

public sealed record TenantContext(Guid ClubId, Guid? DisciplineId)
{
    public static TenantContext ForClub(Guid clubId) =>
        clubId == Guid.Empty
            ? throw new ArgumentException("ClubId must not be empty.", nameof(clubId))
            : new TenantContext(clubId, null);
}
