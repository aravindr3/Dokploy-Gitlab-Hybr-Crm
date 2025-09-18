using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HyBrForex.Infrastructure.Identity.Migrations
{
    /// <inheritdoc />
    public partial class authorizedDealer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuthorizedDealerCategory",
                schema: "Identity");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AuthorizedDealerCategory",
                schema: "Identity",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    DealerCategory = table.Column<string>(type: "text", nullable: true),
                    LastModified = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuthorizedDealerCategory", x => x.Id);
                });
        }
    }
}
