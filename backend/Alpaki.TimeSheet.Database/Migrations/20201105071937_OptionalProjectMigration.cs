using Microsoft.EntityFrameworkCore.Migrations;

namespace Alpaki.TimeSheet.Database.Migrations
{
    public partial class OptionalProjectMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TimeEntry_Project_ProjectId",
                schema: "TimeSheet",
                table: "TimeEntry");

            migrationBuilder.AlterColumn<long>(
                name: "ProjectId",
                schema: "TimeSheet",
                table: "TimeEntry",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddForeignKey(
                name: "FK_TimeEntry_Project_ProjectId",
                schema: "TimeSheet",
                table: "TimeEntry",
                column: "ProjectId",
                principalSchema: "TimeSheet",
                principalTable: "Project",
                principalColumn: "ProjectId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TimeEntry_Project_ProjectId",
                schema: "TimeSheet",
                table: "TimeEntry");

            migrationBuilder.AlterColumn<long>(
                name: "ProjectId",
                schema: "TimeSheet",
                table: "TimeEntry",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_TimeEntry_Project_ProjectId",
                schema: "TimeSheet",
                table: "TimeEntry",
                column: "ProjectId",
                principalSchema: "TimeSheet",
                principalTable: "Project",
                principalColumn: "ProjectId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
