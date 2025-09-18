using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HyBrForex.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class LeadContact : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Stages",
                schema: "Application",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    LastModified = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StateMaster",
                schema: "Application",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    StateCode = table.Column<string>(type: "text", nullable: true),
                    StateName = table.Column<string>(type: "text", nullable: true),
                    GSTCode = table.Column<int>(type: "integer", nullable: true),
                    ConuntryId = table.Column<string>(type: "text", nullable: true),
                    CountryId = table.Column<string>(type: "text", nullable: true),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    LastModified = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StateMaster", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StateMaster_CountryMaster_CountryId",
                        column: x => x.CountryId,
                        principalSchema: "Application",
                        principalTable: "CountryMaster",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "LeadContact",
                schema: "Application",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    FirstName = table.Column<string>(type: "text", nullable: true),
                    LastName = table.Column<string>(type: "text", nullable: true),
                    PhoneNumber1 = table.Column<string>(type: "text", nullable: true),
                    PhoneNumber2 = table.Column<string>(type: "text", nullable: true),
                    WhatsAppNumber = table.Column<string>(type: "text", nullable: true),
                    Email = table.Column<string>(type: "text", nullable: true),
                    GenderId = table.Column<string>(type: "text", nullable: true),
                    ParentsName = table.Column<string>(type: "text", nullable: true),
                    ParentsPhoneNumber = table.Column<string>(type: "text", nullable: true),
                    CountryId = table.Column<string>(type: "text", nullable: true),
                    StateId = table.Column<string>(type: "text", nullable: true),
                    District = table.Column<string>(type: "text", nullable: true),
                    City = table.Column<string>(type: "text", nullable: true),
                    Locality = table.Column<string>(type: "text", nullable: true),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    LastModified = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeadContact", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LeadContact_CommonMsater_GenderId",
                        column: x => x.GenderId,
                        principalSchema: "Application",
                        principalTable: "CommonMsater",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_LeadContact_CountryMaster_CountryId",
                        column: x => x.CountryId,
                        principalSchema: "Application",
                        principalTable: "CountryMaster",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_LeadContact_StateMaster_StateId",
                        column: x => x.StateId,
                        principalSchema: "Application",
                        principalTable: "StateMaster",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_LeadContact_CountryId",
                schema: "Application",
                table: "LeadContact",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_LeadContact_GenderId",
                schema: "Application",
                table: "LeadContact",
                column: "GenderId");

            migrationBuilder.CreateIndex(
                name: "IX_LeadContact_StateId",
                schema: "Application",
                table: "LeadContact",
                column: "StateId");

            migrationBuilder.CreateIndex(
                name: "IX_StateMaster_CountryId",
                schema: "Application",
                table: "StateMaster",
                column: "CountryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LeadContact",
                schema: "Application");

            migrationBuilder.DropTable(
                name: "Stages",
                schema: "Application");

            migrationBuilder.DropTable(
                name: "StateMaster",
                schema: "Application");
        }
    }
}
