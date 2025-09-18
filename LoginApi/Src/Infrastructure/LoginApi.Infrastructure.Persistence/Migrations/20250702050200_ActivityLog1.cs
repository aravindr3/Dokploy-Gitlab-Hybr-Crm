using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HyBrForex.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ActivityLog1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ArrivalDate",
                schema: "Application",
                table: "ActivityLog",
                newName: "ActivityDate");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ActivityDate",
                schema: "Application",
                table: "ActivityLog",
                newName: "ArrivalDate");
        }
    }
}
