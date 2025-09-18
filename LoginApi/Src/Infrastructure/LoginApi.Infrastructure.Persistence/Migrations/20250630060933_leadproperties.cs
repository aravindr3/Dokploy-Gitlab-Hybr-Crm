using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HyBrForex.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class leadproperties : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LeadProperties",
                schema: "Application",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    OwnerId = table.Column<string>(type: "text", nullable: true),
                    LeadId = table.Column<string>(type: "text", nullable: true),
                    CountryInterestedIn = table.Column<string>(type: "text", nullable: true),
                    DocumentUploadStatus = table.Column<bool>(type: "boolean", nullable: true),
                    UniversityPreferred = table.Column<string>(type: "text", nullable: true),
                    OfferLetterStatus = table.Column<int>(type: "integer", nullable: true),
                    DepositPaidUniversity = table.Column<bool>(type: "boolean", nullable: true),
                    VisaStatus = table.Column<bool>(type: "boolean", nullable: true),
                    RefundStatus = table.Column<int>(type: "integer", nullable: true),
                    FutureIntake = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    LastModified = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeadProperties", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LeadProperties",
                schema: "Application");
        }
    }
}
