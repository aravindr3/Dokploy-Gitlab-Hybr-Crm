using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HyBrForex.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class commonTypeMaster : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Application");

            migrationBuilder.CreateTable(
                name: "ApplicationHis",
                schema: "Application",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    ApplicationId = table.Column<int>(type: "integer", nullable: false),
                    OldHAJJNo = table.Column<string>(type: "text", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    LastModified = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationHis", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CommonTypeMsater",
                schema: "Application",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    TypeName = table.Column<string>(type: "text", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    LastModified = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommonTypeMsater", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CustomerMaster",
                schema: "Application",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    BranchName = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    SurName = table.Column<string>(type: "text", nullable: false),
                    pptNo = table.Column<string>(type: "text", nullable: false),
                    Gender = table.Column<string>(type: "text", nullable: false),
                    DOB = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DOI = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DOE = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    PlaceOfIssue = table.Column<string>(type: "text", nullable: false),
                    PlaceOfBirth = table.Column<string>(type: "text", nullable: false),
                    Address = table.Column<string>(type: "text", nullable: false),
                    ContactNumber = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    PanCard = table.Column<string>(type: "text", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "numeric(18,6)", nullable: false),
                    DDAmount = table.Column<decimal>(type: "numeric(18,6)", nullable: false),
                    ChequeNo = table.Column<string>(type: "text", nullable: false),
                    ChequeDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Bank = table.Column<string>(type: "text", nullable: false),
                    PackageType = table.Column<string>(type: "text", nullable: false),
                    Remarks = table.Column<string>(type: "text", nullable: false),
                    CareRemarks = table.Column<string>(type: "text", nullable: false),
                    BankDetails = table.Column<string>(type: "text", nullable: false),
                    RcNumber = table.Column<string>(type: "text", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    LastModified = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerMaster", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CommonMsater",
                schema: "Application",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    CommonTypeId = table.Column<string>(type: "text", nullable: false),
                    CommonName = table.Column<string>(type: "text", nullable: false),
                    CommonTypeMsaterId = table.Column<string>(type: "text", nullable: true),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    LastModified = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommonMsater", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CommonMsater_CommonTypeMsater_CommonTypeMsaterId",
                        column: x => x.CommonTypeMsaterId,
                        principalSchema: "Application",
                        principalTable: "CommonTypeMsater",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ApplicationMaster",
                schema: "Application",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    SLNO = table.Column<int>(type: "integer", nullable: false),
                    HAJJNo = table.Column<string>(type: "text", nullable: false),
                    CustomerId = table.Column<string>(type: "text", nullable: false),
                    SmsStatus = table.Column<int>(type: "integer", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    LastModified = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
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

            migrationBuilder.CreateTable(
                name: "EmployeeMaster",
                schema: "Application",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    EmployeeCode = table.Column<string>(type: "text", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Mobile = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Address1 = table.Column<string>(type: "text", nullable: false),
                    Address2 = table.Column<string>(type: "text", nullable: false),
                    City = table.Column<string>(type: "text", nullable: false),
                    Pin = table.Column<string>(type: "text", nullable: false),
                    Phone = table.Column<string>(type: "text", nullable: false),
                    DesignationId = table.Column<string>(type: "text", nullable: true),
                    JoinDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ResignDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    BranchId = table.Column<string>(type: "text", nullable: true),
                    SpecifyBranch = table.Column<string>(type: "text", nullable: true),
                    ReportingTo = table.Column<string>(type: "text", nullable: true),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    LastModified = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeMaster", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployeeMaster_CommonMsater_DesignationId",
                        column: x => x.DesignationId,
                        principalSchema: "Application",
                        principalTable: "CommonMsater",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationMaster_CustomerId",
                schema: "Application",
                table: "ApplicationMaster",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_CommonMsater_CommonTypeMsaterId",
                schema: "Application",
                table: "CommonMsater",
                column: "CommonTypeMsaterId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeMaster_DesignationId",
                schema: "Application",
                table: "EmployeeMaster",
                column: "DesignationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApplicationHis",
                schema: "Application");

            migrationBuilder.DropTable(
                name: "ApplicationMaster",
                schema: "Application");

            migrationBuilder.DropTable(
                name: "EmployeeMaster",
                schema: "Application");

            migrationBuilder.DropTable(
                name: "CustomerMaster",
                schema: "Application");

            migrationBuilder.DropTable(
                name: "CommonMsater",
                schema: "Application");

            migrationBuilder.DropTable(
                name: "CommonTypeMsater",
                schema: "Application");
        }
    }
}
