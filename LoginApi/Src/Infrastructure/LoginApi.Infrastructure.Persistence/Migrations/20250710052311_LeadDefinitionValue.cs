using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HyBrForex.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class LeadDefinitionValue : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SubDomainName",
                schema: "Application",
                table: "SubDomain",
                newName: "CategoryName");

            migrationBuilder.CreateTable(
                name: "LeadProperyValues",
                schema: "Application",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    LeadId = table.Column<string>(type: "text", nullable: true),
                    OwnerId = table.Column<string>(type: "text", nullable: true),
                    PropertyDefinitionId = table.Column<string>(type: "text", nullable: true),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    LastModified = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeadProperyValues", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LeadProperyValues_LeadProperyDefinition_PropertyDefinitionId",
                        column: x => x.PropertyDefinitionId,
                        principalSchema: "Application",
                        principalTable: "LeadProperyDefinition",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_LeadProperyValues_PropertyDefinitionId",
                schema: "Application",
                table: "LeadProperyValues",
                column: "PropertyDefinitionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LeadProperyValues",
                schema: "Application");

            migrationBuilder.RenameColumn(
                name: "CategoryName",
                schema: "Application",
                table: "SubDomain",
                newName: "SubDomainName");
        }
    }
}
