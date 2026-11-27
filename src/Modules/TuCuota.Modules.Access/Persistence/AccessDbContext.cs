using Microsoft.EntityFrameworkCore;
using TuCuota.BuildingBlocks.Tenancy;

namespace TuCuota.Modules.Access.Persistence;

public sealed class AccessDbContext(DbContextOptions<AccessDbContext> options, TenantContext tenant)
    : DbContext(options)
{
    public DbSet<TenantBoundaryProbe> TenantBoundaryProbes => Set<TenantBoundaryProbe>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("access");
        modelBuilder.Entity<TenantBoundaryProbe>(entity =>
        {
            entity.ToTable("tenant_boundary_probes", table => table.HasCheckConstraint(
                "ck_tenant_boundary_probes_club_id",
                "\"ClubId\" <> '00000000-0000-0000-0000-000000000000'::uuid"));
            entity.HasKey(probe => probe.Id);
            entity.Property(probe => probe.Id).ValueGeneratedNever();
            entity.Property(probe => probe.ClubId).IsRequired();
            entity.Property(probe => probe.CreatedAt).IsRequired();
            entity.HasIndex(probe => new { probe.ClubId, probe.CreatedAt });
            entity.HasQueryFilter("Tenant", probe => probe.ClubId == tenant.ClubId);
        });
    }
}
