using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HyBrForex.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class bonvoiceEventId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EventId",
                schema: "Application",
                table: "BonvoiceCallLog",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TaskId",
                schema: "Application",
                table: "BonvoiceCallLog",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EventId",
                schema: "Application",
                table: "BonvoiceCallLog");

            migrationBuilder.DropColumn(
                name: "TaskId",
                schema: "Application",
                table: "BonvoiceCallLog");
        }
    }
}
