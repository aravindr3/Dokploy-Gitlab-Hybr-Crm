using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HyBrForex.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class leadpropertyvalue : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Value",
                schema: "Application",
                table: "LeadProperyValues",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CategoryId",
                schema: "Application",
                table: "HoliDaysLead",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Value",
                schema: "Application",
                table: "LeadProperyValues");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                schema: "Application",
                table: "HoliDaysLead");
        }
    }
}
