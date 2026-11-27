using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TuCuota.Modules.Access.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitialAccessPersistence : Migration
    {
        private static readonly string[] TenantIndexColumns = ["ClubId", "CreatedAt"];

        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "access");

            migrationBuilder.CreateTable(
                name: "tenant_boundary_probes",
                schema: "access",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ClubId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tenant_boundary_probes", x => x.Id);
                    table.CheckConstraint("ck_tenant_boundary_probes_club_id", "\"ClubId\" <> '00000000-0000-0000-0000-000000000000'::uuid");
                });

            migrationBuilder.CreateIndex(
                name: "IX_tenant_boundary_probes_ClubId_CreatedAt",
                schema: "access",
                table: "tenant_boundary_probes",
                columns: TenantIndexColumns);

            migrationBuilder.Sql("""
                ALTER TABLE access.tenant_boundary_probes ENABLE ROW LEVEL SECURITY;
                ALTER TABLE access.tenant_boundary_probes FORCE ROW LEVEL SECURITY;

                CREATE POLICY tenant_boundary_probes_read
                ON access.tenant_boundary_probes
                FOR SELECT
                USING (
                    "ClubId" = NULLIF(current_setting('app.club_id', true), '')::uuid
                );

                CREATE POLICY tenant_boundary_probes_write
                ON access.tenant_boundary_probes
                FOR ALL
                USING (
                    "ClubId" = NULLIF(current_setting('app.club_id', true), '')::uuid
                )
                WITH CHECK (
                    "ClubId" = NULLIF(current_setting('app.club_id', true), '')::uuid
                );
                """);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tenant_boundary_probes",
                schema: "access");
        }
    }
}
