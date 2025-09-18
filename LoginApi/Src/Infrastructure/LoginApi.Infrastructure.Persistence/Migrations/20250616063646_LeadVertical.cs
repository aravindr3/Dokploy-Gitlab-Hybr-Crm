using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HyBrForex.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class LeadVertical : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "VerticalId",
                schema: "Application",
                table: "LeadContact",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VericalId",
                schema: "Application",
                table: "Lead",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VerticalId",
                schema: "Application",
                table: "LeadContact");

            migrationBuilder.DropColumn(
                name: "VericalId",
                schema: "Application",
                table: "Lead");
        }
    }
}
