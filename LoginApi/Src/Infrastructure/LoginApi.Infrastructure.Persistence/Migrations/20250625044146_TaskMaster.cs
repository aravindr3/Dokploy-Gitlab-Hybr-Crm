using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HyBrForex.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class TaskMaster : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TaskMaster",
                schema: "Application",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    LeadId = table.Column<string>(type: "text", nullable: true),
                    OwnerId = table.Column<string>(type: "text", nullable: true),
                    DomainStagesId = table.Column<string>(type: "text", nullable: true),
                    TaskDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    TaskNote = table.Column<string>(type: "text", nullable: true),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    LastModified = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskMaster", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TaskMaster_DomainStages_DomainStagesId",
                        column: x => x.DomainStagesId,
                        principalSchema: "Application",
                        principalTable: "DomainStages",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TaskMaster_Lead_LeadId",
                        column: x => x.LeadId,
                        principalSchema: "Application",
                        principalTable: "Lead",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_TaskMaster_DomainStagesId",
                schema: "Application",
                table: "TaskMaster",
                column: "DomainStagesId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskMaster_LeadId",
                schema: "Application",
                table: "TaskMaster",
                column: "LeadId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TaskMaster",
                schema: "Application");
        }
    }
}
