using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using TuCuota.BuildingBlocks.Tenancy;

namespace TuCuota.Modules.Access.Persistence;

public sealed class AccessDbContextFactory : IDesignTimeDbContextFactory<AccessDbContext>
{
    public AccessDbContext CreateDbContext(string[] args)
    {
        var connectionString = Environment.GetEnvironmentVariable("TUCUOTA_POSTGRES")
            ?? "Host=localhost;Port=5432;Database=tucuota;Username=tucuota_migrator;Password=development-only";
        var options = new DbContextOptionsBuilder<AccessDbContext>()
            .UseNpgsql(connectionString, npgsql =>
                npgsql.MigrationsHistoryTable("__ef_migrations_history", "access"))
            .Options;
        return new AccessDbContext(options, TenantContext.ForClub(Guid.Parse("11111111-1111-1111-1111-111111111111")));
    }
}
