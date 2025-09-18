using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HyBrForex.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class leadstatuslead : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_Lead_Stages_LeadStatusId",
            //    schema: "Application",
            //    table: "Lead");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_Lead_DomainStages_LeadStatusId",
            //    schema: "Application",
            //    table: "Lead",
            //    column: "LeadStatusId",
            //    principalSchema: "Application",
            //    principalTable: "DomainStages",
            //    principalColumn: "Id");
        }

        /// <inheritdoc />
        //protected override void Down(MigrationBuilder migrationBuilder)
        //{
        //    migrationBuilder.DropForeignKey(
        //        name: "FK_Lead_DomainStages_LeadStatusId",
        //        schema: "Application",
        //        table: "Lead");

        //    migrationBuilder.AddForeignKey(
        //        name: "FK_Lead_Stages_LeadStatusId",
        //        schema: "Application",
        //        table: "Lead",
        //        column: "LeadStatusId",
        //        principalSchema: "Application",
        //        principalTable: "Stages",
        //        principalColumn: "Id");
        //}
    }
}
