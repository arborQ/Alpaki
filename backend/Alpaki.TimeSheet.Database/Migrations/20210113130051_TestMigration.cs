using Microsoft.EntityFrameworkCore.Migrations;

namespace Alpaki.TimeSheet.Database.Migrations
{
    public partial class TestMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TimeEntry_TimeSheetPeriod_Year_Month",
                schema: "TimeSheet",
                table: "TimeEntry");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TimeSheetPeriod",
                schema: "TimeSheet",
                table: "TimeSheetPeriod");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TimeSheetPeriod",
                schema: "TimeSheet",
                table: "TimeSheetPeriod",
                columns: new[] { "Year", "Month", "UserId" });

            migrationBuilder.AddForeignKey(
                name: "FK_TimeEntry_TimeSheetPeriod_Year_Month_UserId",
                schema: "TimeSheet",
                table: "TimeEntry",
                columns: new[] { "Year", "Month", "UserId" },
                principalSchema: "TimeSheet",
                principalTable: "TimeSheetPeriod",
                principalColumns: new[] { "Year", "Month", "UserId" },
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TimeEntry_TimeSheetPeriod_Year_Month_UserId",
                schema: "TimeSheet",
                table: "TimeEntry");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TimeSheetPeriod",
                schema: "TimeSheet",
                table: "TimeSheetPeriod");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TimeSheetPeriod",
                schema: "TimeSheet",
                table: "TimeSheetPeriod",
                columns: new[] { "Year", "Month" });

            migrationBuilder.AddForeignKey(
                name: "FK_TimeEntry_TimeSheetPeriod_Year_Month",
                schema: "TimeSheet",
                table: "TimeEntry",
                columns: new[] { "Year", "Month" },
                principalSchema: "TimeSheet",
                principalTable: "TimeSheetPeriod",
                principalColumns: new[] { "Year", "Month" },
                onDelete: ReferentialAction.Cascade);
        }
    }
}
