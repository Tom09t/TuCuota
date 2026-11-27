using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TuCuota.Modules.Access.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddDurableDeliveryFoundation : Migration
    {
        private static readonly string[] IdempotencyIndexColumns = ["ClubId", "OperationScope", "Key"];
        private static readonly string[] InboxIndexColumns = ["ClubId", "ReceivedAt"];
        private static readonly string[] OutboxIndexColumns = ["ClubId", "AvailableAt", "ProcessedAt"];

        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "Version",
                schema: "access",
                table: "tenant_boundary_probes",
                type: "bigint",
                nullable: false,
                defaultValue: 1L);

            migrationBuilder.CreateTable(
                name: "idempotency_records",
                schema: "access",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ClubId = table.Column<Guid>(type: "uuid", nullable: false),
                    OperationScope = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Key = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    RequestHash = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    Status = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    ResourceReference = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    ResponseReference = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    ExpiresAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    CorrelationId = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    CausationId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_idempotency_records", x => x.Id);
                    table.CheckConstraint("ck_idempotency_records_club_id", "\"ClubId\" <> '00000000-0000-0000-0000-000000000000'::uuid");
                    table.CheckConstraint("ck_idempotency_records_expiration", "\"ExpiresAt\" >= \"CreatedAt\"");
                });

            migrationBuilder.CreateTable(
                name: "inbox_messages",
                schema: "access",
                columns: table => new
                {
                    Consumer = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    MessageId = table.Column<Guid>(type: "uuid", nullable: false),
                    ClubId = table.Column<Guid>(type: "uuid", nullable: false),
                    PayloadHash = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    ReceivedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    ProcessedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    AttemptCount = table.Column<int>(type: "integer", nullable: false),
                    ResultReference = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    CorrelationId = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_inbox_messages", x => new { x.Consumer, x.MessageId });
                    table.CheckConstraint("ck_inbox_messages_attempt_count", "\"AttemptCount\" >= 0");
                    table.CheckConstraint("ck_inbox_messages_club_id", "\"ClubId\" <> '00000000-0000-0000-0000-000000000000'::uuid");
                });

            migrationBuilder.CreateTable(
                name: "outbox_messages",
                schema: "access",
                columns: table => new
                {
                    MessageId = table.Column<Guid>(type: "uuid", nullable: false),
                    ClubId = table.Column<Guid>(type: "uuid", nullable: false),
                    MessageType = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Payload = table.Column<string>(type: "jsonb", nullable: false),
                    OccurredAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    AvailableAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    AttemptCount = table.Column<int>(type: "integer", nullable: false),
                    ProcessedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    LeaseOwner = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    LeaseExpiresAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    CorrelationId = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    CausationId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_outbox_messages", x => x.MessageId);
                    table.CheckConstraint("ck_outbox_messages_attempt_count", "\"AttemptCount\" >= 0");
                    table.CheckConstraint("ck_outbox_messages_club_id", "\"ClubId\" <> '00000000-0000-0000-0000-000000000000'::uuid");
                    table.CheckConstraint("ck_outbox_messages_lease", "(\"LeaseOwner\" IS NULL) = (\"LeaseExpiresAt\" IS NULL)");
                });

            migrationBuilder.CreateIndex(
                name: "IX_idempotency_records_ClubId_OperationScope_Key",
                schema: "access",
                table: "idempotency_records",
                columns: IdempotencyIndexColumns,
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_inbox_messages_ClubId_ReceivedAt",
                schema: "access",
                table: "inbox_messages",
                columns: InboxIndexColumns);

            migrationBuilder.CreateIndex(
                name: "IX_outbox_messages_ClubId_AvailableAt_ProcessedAt",
                schema: "access",
                table: "outbox_messages",
                columns: OutboxIndexColumns);

            migrationBuilder.Sql("""
                ALTER TABLE access.outbox_messages ENABLE ROW LEVEL SECURITY;
                ALTER TABLE access.outbox_messages FORCE ROW LEVEL SECURITY;
                ALTER TABLE access.inbox_messages ENABLE ROW LEVEL SECURITY;
                ALTER TABLE access.inbox_messages FORCE ROW LEVEL SECURITY;
                ALTER TABLE access.idempotency_records ENABLE ROW LEVEL SECURITY;
                ALTER TABLE access.idempotency_records FORCE ROW LEVEL SECURITY;

                CREATE POLICY outbox_messages_tenant ON access.outbox_messages
                FOR ALL
                USING ("ClubId" = NULLIF(current_setting('app.club_id', true), '')::uuid)
                WITH CHECK ("ClubId" = NULLIF(current_setting('app.club_id', true), '')::uuid);

                CREATE POLICY inbox_messages_tenant ON access.inbox_messages
                FOR ALL
                USING ("ClubId" = NULLIF(current_setting('app.club_id', true), '')::uuid)
                WITH CHECK ("ClubId" = NULLIF(current_setting('app.club_id', true), '')::uuid);

                CREATE POLICY idempotency_records_tenant ON access.idempotency_records
                FOR ALL
                USING ("ClubId" = NULLIF(current_setting('app.club_id', true), '')::uuid)
                WITH CHECK ("ClubId" = NULLIF(current_setting('app.club_id', true), '')::uuid);
                """);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "idempotency_records",
                schema: "access");

            migrationBuilder.DropTable(
                name: "inbox_messages",
                schema: "access");

            migrationBuilder.DropTable(
                name: "outbox_messages",
                schema: "access");

            migrationBuilder.DropColumn(
                name: "Version",
                schema: "access",
                table: "tenant_boundary_probes");
        }
    }
}
