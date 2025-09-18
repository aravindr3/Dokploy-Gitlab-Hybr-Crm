using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HyBrForex.Infrastructure.Identity.Migrations
{
    /// <inheritdoc />
    public partial class verticalId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TenantId",
                schema: "Identity",
                table: "User",
                newName: "VerticalId");

            migrationBuilder.RenameColumn(
                name: "TenantId",
                schema: "Identity",
                table: "BranchMaster",
                newName: "VerticalId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "VerticalId",
                schema: "Identity",
                table: "User",
                newName: "TenantId");

            migrationBuilder.RenameColumn(
                name: "VerticalId",
                schema: "Identity",
                table: "BranchMaster",
                newName: "TenantId");
        }
    }
}
