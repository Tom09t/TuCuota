using Microsoft.EntityFrameworkCore;
using TuCuota.BuildingBlocks.Tenancy;
using TuCuota.Modules.Access.Reliability;

namespace TuCuota.Modules.Access.Persistence;

public sealed class AccessDbContext(DbContextOptions<AccessDbContext> options, TenantContext tenant)
    : DbContext(options)
{
    public DbSet<TenantBoundaryProbe> TenantBoundaryProbes => Set<TenantBoundaryProbe>();
    public DbSet<OutboxMessage> OutboxMessages => Set<OutboxMessage>();
    public DbSet<InboxMessage> InboxMessages => Set<InboxMessage>();
    public DbSet<IdempotencyRecord> IdempotencyRecords => Set<IdempotencyRecord>();

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
            entity.Property(probe => probe.Version).HasDefaultValue(1L).IsConcurrencyToken();
            entity.HasIndex(probe => new { probe.ClubId, probe.CreatedAt });
            entity.HasQueryFilter("Tenant", probe => probe.ClubId == tenant.ClubId);
        });

        modelBuilder.Entity<OutboxMessage>(entity =>
        {
            entity.ToTable("outbox_messages", table =>
            {
                table.HasCheckConstraint("ck_outbox_messages_club_id", "\"ClubId\" <> '00000000-0000-0000-0000-000000000000'::uuid");
                table.HasCheckConstraint("ck_outbox_messages_attempt_count", "\"AttemptCount\" >= 0");
                table.HasCheckConstraint(
                    "ck_outbox_messages_lease",
                    "(\"LeaseOwner\" IS NULL) = (\"LeaseExpiresAt\" IS NULL)");
            });
            entity.HasKey(message => message.MessageId);
            entity.Property(message => message.MessageId).ValueGeneratedNever();
            entity.Property(message => message.MessageType).HasMaxLength(200);
            entity.Property(message => message.Payload).HasColumnType("jsonb");
            entity.Property(message => message.CorrelationId).HasMaxLength(64);
            entity.Property(message => message.LeaseOwner).HasMaxLength(100);
            entity.HasIndex(message => new { message.ClubId, message.AvailableAt, message.ProcessedAt });
            entity.HasQueryFilter("Tenant", message => message.ClubId == tenant.ClubId);
        });

        modelBuilder.Entity<InboxMessage>(entity =>
        {
            entity.ToTable("inbox_messages", table =>
            {
                table.HasCheckConstraint("ck_inbox_messages_club_id", "\"ClubId\" <> '00000000-0000-0000-0000-000000000000'::uuid");
                table.HasCheckConstraint("ck_inbox_messages_attempt_count", "\"AttemptCount\" >= 0");
            });
            entity.HasKey(message => new { message.Consumer, message.MessageId });
            entity.Property(message => message.Consumer).HasMaxLength(200);
            entity.Property(message => message.PayloadHash).HasMaxLength(64);
            entity.Property(message => message.CorrelationId).HasMaxLength(64);
            entity.Property(message => message.ResultReference).HasMaxLength(500);
            entity.HasIndex(message => new { message.ClubId, message.ReceivedAt });
            entity.HasQueryFilter("Tenant", message => message.ClubId == tenant.ClubId);
        });

        modelBuilder.Entity<IdempotencyRecord>(entity =>
        {
            entity.ToTable("idempotency_records", table =>
            {
                table.HasCheckConstraint("ck_idempotency_records_club_id", "\"ClubId\" <> '00000000-0000-0000-0000-000000000000'::uuid");
                table.HasCheckConstraint("ck_idempotency_records_expiration", "\"ExpiresAt\" >= \"CreatedAt\"");
            });
            entity.HasKey(record => record.Id);
            entity.Property(record => record.Id).ValueGeneratedNever();
            entity.Property(record => record.OperationScope).HasMaxLength(200);
            entity.Property(record => record.Key).HasMaxLength(200);
            entity.Property(record => record.RequestHash).HasMaxLength(64);
            entity.Property(record => record.Status).HasMaxLength(32);
            entity.Property(record => record.ResourceReference).HasMaxLength(500);
            entity.Property(record => record.ResponseReference).HasMaxLength(500);
            entity.Property(record => record.CorrelationId).HasMaxLength(64);
            entity.HasIndex(record => new { record.ClubId, record.OperationScope, record.Key }).IsUnique();
            entity.HasQueryFilter("Tenant", record => record.ClubId == tenant.ClubId);
        });
    }

    public override int SaveChanges(bool acceptAllChangesOnSuccess)
    {
        AdvanceConcurrencyVersions();
        return base.SaveChanges(acceptAllChangesOnSuccess);
    }

    public override Task<int> SaveChangesAsync(
        bool acceptAllChangesOnSuccess,
        CancellationToken cancellationToken = default)
    {
        AdvanceConcurrencyVersions();
        return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }

    private void AdvanceConcurrencyVersions()
    {
        foreach (var entry in ChangeTracker.Entries<TenantBoundaryProbe>())
        {
            if (entry.State == EntityState.Modified)
            {
                entry.Property(probe => probe.Version).CurrentValue =
                    entry.Property(probe => probe.Version).OriginalValue + 1;
            }
        }
    }
}
