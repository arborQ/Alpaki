using Microsoft.EntityFrameworkCore.Migrations;

namespace Alpaki.Moto.Database.Migrations
{
    public partial class TestMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                schema: "Moto",
                table: "Brand",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "Model",
                schema: "Moto",
                columns: table => new
                {
                    ModelId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ModelName = table.Column<string>(maxLength: 500, nullable: false),
                    BrandId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Model", x => x.ModelId);
                    table.ForeignKey(
                        name: "FK_Model_Brand_BrandId",
                        column: x => x.BrandId,
                        principalSchema: "Moto",
                        principalTable: "Brand",
                        principalColumn: "BrandId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Model_BrandId",
                schema: "Moto",
                table: "Model",
                column: "BrandId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Model",
                schema: "Moto");

            migrationBuilder.DropColumn(
                name: "IsActive",
                schema: "Moto",
                table: "Brand");
        }
    }
}
