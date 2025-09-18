using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HyBrForex.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class commonMaster : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_CommonMsater_CommonTypeId",
                schema: "Application",
                table: "CommonMsater",
                column: "CommonTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_CommonMsater_CommonTypeMsater_CommonTypeId",
                schema: "Application",
                table: "CommonMsater",
                column: "CommonTypeId",
                principalSchema: "Application",
                principalTable: "CommonTypeMsater",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CommonMsater_CommonTypeMsater_CommonTypeId",
                schema: "Application",
                table: "CommonMsater");

            migrationBuilder.DropIndex(
                name: "IX_CommonMsater_CommonTypeId",
                schema: "Application",
                table: "CommonMsater");
        }
    }
}
