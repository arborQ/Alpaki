using Microsoft.EntityFrameworkCore.Migrations;

namespace Alpaki.Moto.Database.Migrations
{
    public partial class InitBrandsMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Moto");

            migrationBuilder.CreateTable(
                name: "Brand",
                schema: "Moto",
                columns: table => new
                {
                    BrandId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BrandName = table.Column<string>(maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brand", x => x.BrandId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Brand",
                schema: "Moto");
        }
    }
}
