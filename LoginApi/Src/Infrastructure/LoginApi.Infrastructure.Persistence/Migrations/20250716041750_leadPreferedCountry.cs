using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HyBrForex.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class leadPreferedCountry : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PreferedCountry",
                schema: "Application",
                table: "Lead",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PreferedCountry",
                schema: "Application",
                table: "Lead");
        }
    }
}
