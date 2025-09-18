using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HyBrForex.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Lead : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Lead",
                schema: "Application",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    LeadContactId = table.Column<string>(type: "text", nullable: true),
                    LeadSourceId = table.Column<string>(type: "text", nullable: true),
                    LeadStatusId = table.Column<string>(type: "text", nullable: true),
                    Notes = table.Column<string>(type: "text", nullable: true),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    LastModified = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lead", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Lead_CommonMsater_LeadSourceId",
                        column: x => x.LeadSourceId,
                        principalSchema: "Application",
                        principalTable: "CommonMsater",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Lead_LeadContact_LeadContactId",
                        column: x => x.LeadContactId,
                        principalSchema: "Application",
                        principalTable: "LeadContact",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Lead_LeadContactId",
                schema: "Application",
                table: "Lead",
                column: "LeadContactId");

            migrationBuilder.CreateIndex(
                name: "IX_Lead_LeadSourceId",
                schema: "Application",
                table: "Lead",
                column: "LeadSourceId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Lead",
                schema: "Application");
        }
    }
}
