using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HyBrForex.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class callrequestowner1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OwnerId",
                schema: "Application",
                table: "Lead");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                schema: "Application",
                table: "HoliDaysLead");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OwnerId",
                schema: "Application",
                table: "Lead",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OwnerId",
                schema: "Application",
                table: "HoliDaysLead",
                type: "text",
                nullable: true);
        }
    }
}
