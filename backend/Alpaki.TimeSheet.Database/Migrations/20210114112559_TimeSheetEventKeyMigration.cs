using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Alpaki.TimeSheet.Database.Migrations
{
    public partial class TimeSheetEventKeyMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PeriodDomainEvents",
                schema: "TimeSheet",
                columns: table => new
                {
                    EventId = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    EventData = table.Column<string>(nullable: true),
                    EventType = table.Column<int>(nullable: false),
                    TimeSheetPeriodMonth = table.Column<int>(nullable: true),
                    TimeSheetPeriodUserId = table.Column<long>(nullable: true),
                    TimeSheetPeriodYear = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PeriodDomainEvents", x => x.EventId);
                    table.ForeignKey(
                        name: "FK_PeriodDomainEvents_TimeSheetPeriod_TimeSheetPeriodYear_TimeSheetPeriodMonth_TimeSheetPeriodUserId",
                        columns: x => new { x.TimeSheetPeriodYear, x.TimeSheetPeriodMonth, x.TimeSheetPeriodUserId },
                        principalSchema: "TimeSheet",
                        principalTable: "TimeSheetPeriod",
                        principalColumns: new[] { "Year", "Month", "UserId" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PeriodDomainEvents_TimeSheetPeriodYear_TimeSheetPeriodMonth_TimeSheetPeriodUserId",
                schema: "TimeSheet",
                table: "PeriodDomainEvents",
                columns: new[] { "TimeSheetPeriodYear", "TimeSheetPeriodMonth", "TimeSheetPeriodUserId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PeriodDomainEvents",
                schema: "TimeSheet");
        }
    }
}
