using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HyBrForex.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class holidayleadSource : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LeadSourceId",
                schema: "Application",
                table: "HoliDaysLead",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LeadStatusId",
                schema: "Application",
                table: "HoliDaysLead",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LeadSourceId",
                schema: "Application",
                table: "HoliDaysLead");

            migrationBuilder.DropColumn(
                name: "LeadStatusId",
                schema: "Application",
                table: "HoliDaysLead");
        }
    }
}
