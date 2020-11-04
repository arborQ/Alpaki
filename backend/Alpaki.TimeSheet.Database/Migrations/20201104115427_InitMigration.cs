using Microsoft.EntityFrameworkCore.Migrations;

namespace Alpaki.TimeSheet.Database.Migrations
{
    public partial class InitMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "TimeSheet");

            migrationBuilder.CreateTable(
                name: "Project",
                schema: "TimeSheet",
                columns: table => new
                {
                    ProjectId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Project", x => x.ProjectId);
                });

            migrationBuilder.CreateTable(
                name: "TimeEntry",
                schema: "TimeSheet",
                columns: table => new
                {
                    Year = table.Column<int>(nullable: false),
                    Month = table.Column<int>(nullable: false),
                    Day = table.Column<int>(nullable: false),
                    UserId = table.Column<long>(nullable: false),
                    Hours = table.Column<decimal>(nullable: false),
                    ProjectId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimeEntry", x => new { x.Year, x.Month, x.UserId, x.Day });
                    table.ForeignKey(
                        name: "FK_TimeEntry_Project_ProjectId",
                        column: x => x.ProjectId,
                        principalSchema: "TimeSheet",
                        principalTable: "Project",
                        principalColumn: "ProjectId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TimeEntry_ProjectId",
                schema: "TimeSheet",
                table: "TimeEntry",
                column: "ProjectId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TimeEntry",
                schema: "TimeSheet");

            migrationBuilder.DropTable(
                name: "Project",
                schema: "TimeSheet");
        }
    }
}
