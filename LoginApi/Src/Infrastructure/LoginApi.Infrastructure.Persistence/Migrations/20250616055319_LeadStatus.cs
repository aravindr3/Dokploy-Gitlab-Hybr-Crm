using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HyBrForex.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class LeadStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DomainStages",
                schema: "Application",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    DomainId = table.Column<string>(type: "text", nullable: true),
                    StagesId = table.Column<string>(type: "text", nullable: true),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    LastModified = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DomainStages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DomainStages_Stages_StagesId",
                        column: x => x.StagesId,
                        principalSchema: "Application",
                        principalTable: "Stages",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Lead_LeadStatusId",
                schema: "Application",
                table: "Lead",
                column: "LeadStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_DomainStages_StagesId",
                schema: "Application",
                table: "DomainStages",
                column: "StagesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Lead_Stages_LeadStatusId",
                schema: "Application",
                table: "Lead",
                column: "LeadStatusId",
                principalSchema: "Application",
                principalTable: "Stages",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lead_Stages_LeadStatusId",
                schema: "Application",
                table: "Lead");

            migrationBuilder.DropTable(
                name: "DomainStages",
                schema: "Application");

            migrationBuilder.DropIndex(
                name: "IX_Lead_LeadStatusId",
                schema: "Application",
                table: "Lead");
        }
    }
}
