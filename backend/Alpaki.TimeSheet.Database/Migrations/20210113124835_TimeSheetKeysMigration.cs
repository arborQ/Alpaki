using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Alpaki.TimeSheet.Database.Migrations
{
    public partial class TimeSheetKeysMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TimeSheetPeriod",
                schema: "TimeSheet",
                columns: table => new
                {
                    Year = table.Column<int>(nullable: false),
                    Month = table.Column<int>(nullable: false),
                    UserId = table.Column<long>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    LockedFrom = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimeSheetPeriod", x => new { x.Year, x.Month });
                });

            migrationBuilder.Sql(@"
                  INSERT INTO [TimeSheet].[TimeSheetPeriod]
                  SELECT DISTINCT [Year], [Month], [UserId], GETDATE() as [CreatedAt], NULL as LockedFrom from [TimeSheet].[TimeEntry]
            ");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TimeEntry_TimeSheetPeriod_Year_Month",
                schema: "TimeSheet",
                table: "TimeEntry");

            migrationBuilder.DropTable(
                name: "TimeSheetPeriod",
                schema: "TimeSheet");
        }
    }
}
