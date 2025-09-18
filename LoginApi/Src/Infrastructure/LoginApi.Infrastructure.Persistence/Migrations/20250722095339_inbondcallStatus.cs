using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HyBrForex.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class inbondcallStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "InBondCall",
                schema: "Application",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Direction = table.Column<string>(type: "text", nullable: false),
                    SourceNumber = table.Column<string>(type: "text", nullable: false),
                    DestinationNumber = table.Column<string>(type: "text", nullable: false),
                    DisplayNumber = table.Column<string>(type: "text", nullable: false),
                    StartTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EndTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CallDuration = table.Column<double>(type: "double precision", nullable: false),
                    CallStatus = table.Column<string>(type: "text", nullable: false),
                    DataSource = table.Column<string>(type: "text", nullable: false),
                    CallType = table.Column<int>(type: "integer", nullable: false),
                    DTMF = table.Column<string>(type: "text", nullable: false),
                    AccountID = table.Column<string>(type: "text", nullable: false),
                    ResourceURL = table.Column<string>(type: "text", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    LastModified = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InBondCall", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InBondCall",
                schema: "Application");
        }
    }
}
