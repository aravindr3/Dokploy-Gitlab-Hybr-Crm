using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HyBrForex.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class holiday : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HoliDaysLead",
                schema: "Application",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    LeadId = table.Column<string>(type: "text", nullable: true),
                    EnquiryType = table.Column<string>(type: "text", nullable: true),
                    TravelType = table.Column<string>(type: "text", nullable: true),
                    PrefferedDestination = table.Column<string>(type: "text", nullable: true),
                    TripDuration = table.Column<string>(type: "text", nullable: true),
                    DepatureCity = table.Column<string>(type: "text", nullable: true),
                    StartDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    EndDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Adults = table.Column<int>(type: "integer", nullable: false),
                    ChildWithBed = table.Column<int>(type: "integer", nullable: false),
                    ChildWithoutBed = table.Column<int>(type: "integer", nullable: false),
                    BeddingPreference = table.Column<string>(type: "text", nullable: true),
                    RoomType = table.Column<string>(type: "text", nullable: true),
                    MealPlan = table.Column<string>(type: "text", nullable: true),
                    Notes = table.Column<string>(type: "text", nullable: true),
                    FollowUpDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    AsignedAgent = table.Column<string>(type: "text", nullable: true),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    LastModified = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HoliDaysLead", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SubDomain",
                schema: "Application",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    DomainId = table.Column<string>(type: "text", nullable: true),
                    SubDomainName = table.Column<string>(type: "text", nullable: true),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    LastModified = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubDomain", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HoliDaysLead",
                schema: "Application");

            migrationBuilder.DropTable(
                name: "SubDomain",
                schema: "Application");
        }
    }
}
