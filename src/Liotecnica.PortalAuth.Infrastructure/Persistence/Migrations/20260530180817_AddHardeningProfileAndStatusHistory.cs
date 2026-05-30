using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Liotecnica.PortalAuth.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddHardeningProfileAndStatusHistory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "must_change_password",
                schema: "portal_auth",
                table: "AspNetUsers",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "is_active",
                schema: "portal_auth",
                table: "AspNetRoles",
                type: "boolean",
                nullable: false,
                defaultValue: true);

            migrationBuilder.AddColumn<bool>(
                name: "is_deleted",
                schema: "portal_auth",
                table: "AspNetRoles",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "updated_at",
                schema: "portal_auth",
                table: "AspNetRoles",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "updated_by",
                schema: "portal_auth",
                table: "AspNetRoles",
                type: "character varying(256)",
                maxLength: 256,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "operational_status_snapshots",
                schema: "portal_auth",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    status = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    checked_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    total_duration_ms = table.Column<long>(type: "bigint", nullable: false),
                    components_summary = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_operational_status_snapshots", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_operational_status_snapshots_checked_at",
                schema: "portal_auth",
                table: "operational_status_snapshots",
                column: "checked_at");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "operational_status_snapshots",
                schema: "portal_auth");

            migrationBuilder.DropColumn(
                name: "must_change_password",
                schema: "portal_auth",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "is_active",
                schema: "portal_auth",
                table: "AspNetRoles");

            migrationBuilder.DropColumn(
                name: "is_deleted",
                schema: "portal_auth",
                table: "AspNetRoles");

            migrationBuilder.DropColumn(
                name: "updated_at",
                schema: "portal_auth",
                table: "AspNetRoles");

            migrationBuilder.DropColumn(
                name: "updated_by",
                schema: "portal_auth",
                table: "AspNetRoles");
        }
    }
}
