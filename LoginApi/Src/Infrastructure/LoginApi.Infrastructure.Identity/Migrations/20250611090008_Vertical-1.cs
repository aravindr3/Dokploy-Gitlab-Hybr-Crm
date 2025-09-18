using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HyBrForex.Infrastructure.Identity.Migrations
{
    /// <inheritdoc />
    public partial class Vertical1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Domain",
                schema: "Identity",
                table: "Tenants",
                newName: "DomainId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DomainId",
                schema: "Identity",
                table: "Tenants",
                newName: "Domain");
        }
    }
}
