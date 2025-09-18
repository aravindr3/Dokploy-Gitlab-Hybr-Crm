using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HyBrForex.Infrastructure.Identity.Migrations
{
    /// <inheritdoc />
    public partial class authorizedDealer1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AuthorizedDealerId",
                schema: "Identity",
                table: "Tenants");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AuthorizedDealerId",
                schema: "Identity",
                table: "Tenants",
                type: "text",
                nullable: true);
        }
    }
}
