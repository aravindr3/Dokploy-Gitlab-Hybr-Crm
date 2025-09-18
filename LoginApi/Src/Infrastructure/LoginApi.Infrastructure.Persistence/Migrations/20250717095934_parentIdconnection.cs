using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HyBrForex.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class parentIdconnection : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_DomainStages_ParentId",
                schema: "Application",
                table: "DomainStages",
                column: "ParentId");

            migrationBuilder.AddForeignKey(
                name: "FK_DomainStages_DomainStages_ParentId",
                schema: "Application",
                table: "DomainStages",
                column: "ParentId",
                principalSchema: "Application",
                principalTable: "DomainStages",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DomainStages_DomainStages_ParentId",
                schema: "Application",
                table: "DomainStages");

            migrationBuilder.DropIndex(
                name: "IX_DomainStages_ParentId",
                schema: "Application",
                table: "DomainStages");
        }
    }
}
