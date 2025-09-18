using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HyBrForex.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class taskupdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CountryInterestedIn",
                schema: "Application",
                table: "TaskMaster",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DepositPaidUniversity",
                schema: "Application",
                table: "TaskMaster",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SubDescription",
                schema: "Application",
                table: "TaskMaster",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UniversityPreferred",
                schema: "Application",
                table: "TaskMaster",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CountryInterestedIn",
                schema: "Application",
                table: "TaskMaster");

            migrationBuilder.DropColumn(
                name: "DepositPaidUniversity",
                schema: "Application",
                table: "TaskMaster");

            migrationBuilder.DropColumn(
                name: "SubDescription",
                schema: "Application",
                table: "TaskMaster");

            migrationBuilder.DropColumn(
                name: "UniversityPreferred",
                schema: "Application",
                table: "TaskMaster");
        }
    }
}
