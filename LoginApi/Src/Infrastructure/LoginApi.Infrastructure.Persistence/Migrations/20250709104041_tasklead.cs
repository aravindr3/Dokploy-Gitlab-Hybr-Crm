using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HyBrForex.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class tasklead : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TaskMaster_Lead_LeadId",
                schema: "Application",
                table: "TaskMaster");

            migrationBuilder.DropIndex(
                name: "IX_TaskMaster_LeadId",
                schema: "Application",
                table: "TaskMaster");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_TaskMaster_LeadId",
                schema: "Application",
                table: "TaskMaster",
                column: "LeadId");

            migrationBuilder.AddForeignKey(
                name: "FK_TaskMaster_Lead_LeadId",
                schema: "Application",
                table: "TaskMaster",
                column: "LeadId",
                principalSchema: "Application",
                principalTable: "Lead",
                principalColumn: "Id");
        }
    }
}
