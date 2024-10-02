using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "outbox");

            migrationBuilder.EnsureSchema(
                name: "market");

            migrationBuilder.CreateTable(
                name: "outbox_messages",
                schema: "outbox",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    occurred_on_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    type = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    content = table.Column<string>(type: "text", nullable: true),
                    processed_on_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    error_on_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    error_message = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_outbox_messages", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "permissions",
                schema: "market",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    type = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp", nullable: false, defaultValueSql: "now()"),
                    updated_at = table.Column<DateTime>(type: "timestamp", nullable: false, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_permissions", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "roles",
                schema: "market",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    user_id = table.Column<int>(type: "integer", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp", nullable: false, defaultValueSql: "now()"),
                    updated_at = table.Column<DateTime>(type: "timestamp", nullable: false, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_roles", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "roles_permissions",
                schema: "market",
                columns: table => new
                {
                    role_id = table.Column<int>(type: "integer", nullable: false),
                    permission_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_roles_permissions", x => new { x.role_id, x.permission_id });
                    table.ForeignKey(
                        name: "fk_roles_permissions_permissions_permission_id",
                        column: x => x.permission_id,
                        principalSchema: "market",
                        principalTable: "permissions",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_roles_permissions_roles_role_id",
                        column: x => x.role_id,
                        principalSchema: "market",
                        principalTable: "roles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "users",
                schema: "market",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    last_name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    email = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    password = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    photo = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    role_id = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp", nullable: false, defaultValueSql: "now()"),
                    updated_at = table.Column<DateTime>(type: "timestamp", nullable: false, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_users", x => x.id);
                    table.ForeignKey(
                        name: "fk_users_roles_role_id",
                        column: x => x.role_id,
                        principalSchema: "market",
                        principalTable: "roles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "outbox",
                table: "outbox_messages",
                columns: new[] { "id", "content", "error_message", "error_on_utc", "occurred_on_utc", "processed_on_utc", "type" },
                values: new object[] { new Guid("079d2522-a9ea-49d4-b354-97c67cf1c522"), "Content", null, null, new DateTime(2024, 10, 2, 16, 15, 44, 892, DateTimeKind.Utc).AddTicks(9853), new DateTime(2024, 10, 2, 16, 15, 44, 892, DateTimeKind.Utc).AddTicks(9855), "Type" });

            migrationBuilder.CreateIndex(
                name: "ix_roles_user_id",
                schema: "market",
                table: "roles",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_roles_permissions_permission_id",
                schema: "market",
                table: "roles_permissions",
                column: "permission_id");

            migrationBuilder.CreateIndex(
                name: "ix_roles_permissions_role_id",
                schema: "market",
                table: "roles_permissions",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "ix_users_email",
                schema: "market",
                table: "users",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_users_role_id",
                schema: "market",
                table: "users",
                column: "role_id");

            migrationBuilder.AddForeignKey(
                name: "fk_roles_users_user_id",
                schema: "market",
                table: "roles",
                column: "user_id",
                principalSchema: "market",
                principalTable: "users",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_roles_users_user_id",
                schema: "market",
                table: "roles");

            migrationBuilder.DropTable(
                name: "outbox_messages",
                schema: "outbox");

            migrationBuilder.DropTable(
                name: "roles_permissions",
                schema: "market");

            migrationBuilder.DropTable(
                name: "permissions",
                schema: "market");

            migrationBuilder.DropTable(
                name: "users",
                schema: "market");

            migrationBuilder.DropTable(
                name: "roles",
                schema: "market");
        }
    }
}
