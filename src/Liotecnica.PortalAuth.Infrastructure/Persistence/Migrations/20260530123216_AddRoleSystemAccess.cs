using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Liotecnica.PortalAuth.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddRoleSystemAccess : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "role_system_access",
                schema: "portal_auth",
                columns: table => new
                {
                    role_id = table.Column<Guid>(type: "uuid", nullable: false),
                    system_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_role_system_access", x => new { x.role_id, x.system_id });
                    table.ForeignKey(
                        name: "FK_role_system_access_AspNetRoles_role_id",
                        column: x => x.role_id,
                        principalSchema: "portal_auth",
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_role_system_access_corporate_systems_system_id",
                        column: x => x.system_id,
                        principalSchema: "portal_auth",
                        principalTable: "corporate_systems",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_role_system_access_system_id",
                schema: "portal_auth",
                table: "role_system_access",
                column: "system_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "role_system_access",
                schema: "portal_auth");
        }
    }
}
