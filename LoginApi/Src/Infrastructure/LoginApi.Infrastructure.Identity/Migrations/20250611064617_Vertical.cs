using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HyBrForex.Infrastructure.Identity.Migrations
{
    /// <inheritdoc />
    public partial class Vertical : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Vertical",
                schema: "Identity",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    TenantId = table.Column<string>(type: "text", nullable: true),
                    DomainId = table.Column<string>(type: "text", nullable: true),
                    VerticalName = table.Column<string>(type: "text", nullable: true),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    LastModified = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vertical", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Vertical_Domain_DomainId",
                        column: x => x.DomainId,
                        principalSchema: "Identity",
                        principalTable: "Domain",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Vertical_Tenants_TenantId",
                        column: x => x.TenantId,
                        principalSchema: "Identity",
                        principalTable: "Tenants",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Vertical_DomainId",
                schema: "Identity",
                table: "Vertical",
                column: "DomainId");

            migrationBuilder.CreateIndex(
                name: "IX_Vertical_TenantId",
                schema: "Identity",
                table: "Vertical",
                column: "TenantId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Vertical",
                schema: "Identity");
        }
    }
}
