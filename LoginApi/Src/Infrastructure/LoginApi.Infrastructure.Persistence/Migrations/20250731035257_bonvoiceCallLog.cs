using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HyBrForex.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class bonvoiceCallLog : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BonvoiceCallLog",
                schema: "Application",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    LeadId = table.Column<string>(type: "text", nullable: true),
                    Token = table.Column<string>(type: "text", nullable: false),
                    TokenType = table.Column<string>(type: "text", nullable: false),
                    HeaderKey = table.Column<string>(type: "text", nullable: false),
                    HeaderValue = table.Column<string>(type: "text", nullable: false),
                    CallStatus = table.Column<string>(type: "text", nullable: true),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    LastModified = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BonvoiceCallLog", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BonvoiceCallLog",
                schema: "Application");
        }
    }
}
