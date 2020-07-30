using Microsoft.EntityFrameworkCore.Migrations;

namespace Alpaki.Database.Migrations
{
    public partial class CategoryDefaultStepsMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DreamCategoryDefaultSteps",
                columns: table => new
                {
                    DreamCategoryDefaultStepId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StepDescription = table.Column<string>(nullable: false),
                    IsSponsorRelated = table.Column<bool>(nullable: false),
                    DreamCategoryId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DreamCategoryDefaultSteps", x => x.DreamCategoryDefaultStepId);
                    table.ForeignKey(
                        name: "FK_DreamCategoryDefaultSteps_DreamCategories_DreamCategoryId",
                        column: x => x.DreamCategoryId,
                        principalTable: "DreamCategories",
                        principalColumn: "DreamCategoryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DreamCategoryDefaultSteps_DreamCategoryId",
                table: "DreamCategoryDefaultSteps",
                column: "DreamCategoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DreamCategoryDefaultSteps");
        }
    }
}
