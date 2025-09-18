using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HyBrForex.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class callrequest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CallRequest",
                schema: "Application",
                table: "BonvoiceCallLog",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CallResponse",
                schema: "Application",
                table: "BonvoiceCallLog",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CallRequest",
                schema: "Application",
                table: "BonvoiceCallLog");

            migrationBuilder.DropColumn(
                name: "CallResponse",
                schema: "Application",
                table: "BonvoiceCallLog");
        }
    }
}
