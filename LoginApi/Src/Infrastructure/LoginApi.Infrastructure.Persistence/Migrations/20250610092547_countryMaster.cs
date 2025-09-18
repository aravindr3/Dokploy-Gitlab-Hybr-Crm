using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HyBrForex.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class countryMaster : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CountryMaster",
                schema: "Application",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    CountryName = table.Column<string>(type: "text", nullable: true),
                    Nationality = table.Column<string>(type: "text", nullable: true),
                    ClassId = table.Column<string>(type: "text", nullable: true),
                    SwiftCode = table.Column<string>(type: "text", nullable: true),
                    FATFSanction = table.Column<int>(type: "integer", nullable: true),
                    IBANLength = table.Column<int>(type: "integer", nullable: true),
                    CountryFlag = table.Column<byte[]>(type: "bytea", nullable: true),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    LastModified = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CountryMaster", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CurrencyMaster",
                schema: "Application",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    CountryId = table.Column<string>(type: "text", nullable: false),
                    ISD = table.Column<string>(type: "text", nullable: false),
                    Currency = table.Column<string>(type: "text", nullable: false),
                    FLM8Cd = table.Column<string>(type: "text", nullable: true),
                    CurrencyFlag = table.Column<byte[]>(type: "bytea", nullable: true),
                    CountryName = table.Column<string>(type: "text", nullable: true),
                    CountryMasterId = table.Column<string>(type: "text", nullable: true),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    LastModified = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CurrencyMaster", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CurrencyMaster_CountryMaster_CountryMasterId",
                        column: x => x.CountryMasterId,
                        principalSchema: "Application",
                        principalTable: "CountryMaster",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_CurrencyMaster_CountryMasterId",
                schema: "Application",
                table: "CurrencyMaster",
                column: "CountryMasterId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CurrencyMaster",
                schema: "Application");

            migrationBuilder.DropTable(
                name: "CountryMaster",
                schema: "Application");
        }
    }
}
