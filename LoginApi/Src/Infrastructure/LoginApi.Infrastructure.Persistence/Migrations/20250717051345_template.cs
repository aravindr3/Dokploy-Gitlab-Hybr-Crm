using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HyBrForex.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class template : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TemplateId",
                schema: "Application",
                table: "DomainStages",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "TemplateStatus",
                schema: "Application",
                table: "DomainStages",
                type: "boolean",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TemplateId",
                schema: "Application",
                table: "DomainStages");

            migrationBuilder.DropColumn(
                name: "TemplateStatus",
                schema: "Application",
                table: "DomainStages");
        }
    }
}
