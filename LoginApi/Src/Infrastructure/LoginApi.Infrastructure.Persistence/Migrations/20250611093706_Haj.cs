using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HyBrForex.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Haj : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApplicationHis",
                schema: "Application");

            migrationBuilder.DropTable(
                name: "ApplicationMaster",
                schema: "Application");

            migrationBuilder.DropTable(
                name: "CustomerMaster",
                schema: "Application");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ApplicationHis",
                schema: "Application",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    ApplicationId = table.Column<int>(type: "integer", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    LastModified = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    OldHAJJNo = table.Column<string>(type: "text", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationHis", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CustomerMaster",
                schema: "Application",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Address = table.Column<string>(type: "text", nullable: false),
                    Bank = table.Column<string>(type: "text", nullable: false),
                    BankDetails = table.Column<string>(type: "text", nullable: false),
                    BranchName = table.Column<string>(type: "text", nullable: false),
                    CareRemarks = table.Column<string>(type: "text", nullable: false),
                    ChequeDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ChequeNo = table.Column<string>(type: "text", nullable: false),
                    ContactNumber = table.Column<string>(type: "text", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    DDAmount = table.Column<decimal>(type: "numeric(18,6)", nullable: false),
                    DOB = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DOE = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DOI = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Gender = table.Column<string>(type: "text", nullable: false),
                    LastModified = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: false),
                    PackageType = table.Column<string>(type: "text", nullable: false),
                    PanCard = table.Column<string>(type: "text", nullable: false),
                    PlaceOfBirth = table.Column<string>(type: "text", nullable: false),
                    PlaceOfIssue = table.Column<string>(type: "text", nullable: false),
                    RcNumber = table.Column<string>(type: "text", nullable: false),
                    Remarks = table.Column<string>(type: "text", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    SurName = table.Column<string>(type: "text", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "numeric(18,6)", nullable: false),
                    pptNo = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerMaster", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ApplicationMaster",
                schema: "Application",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    CustomerId = table.Column<string>(type: "text", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    HAJJNo = table.Column<string>(type: "text", nullable: false),
                    LastModified = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    SLNO = table.Column<int>(type: "integer", nullable: false),
                    SmsStatus = table.Column<int>(type: "integer", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationMaster", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApplicationMaster_CustomerMaster_CustomerId",
                        column: x => x.CustomerId,
                        principalSchema: "Application",
                        principalTable: "CustomerMaster",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationMaster_CustomerId",
                schema: "Application",
                table: "ApplicationMaster",
                column: "CustomerId");
        }
    }
}
