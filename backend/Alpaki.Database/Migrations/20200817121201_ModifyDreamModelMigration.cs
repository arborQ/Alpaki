using Microsoft.EntityFrameworkCore.Migrations;

namespace Alpaki.Database.Migrations
{
    public partial class ModifyDreamModelMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DisplayName",
                table: "Dreams",
                maxLength: 500,
                nullable: true);

            migrationBuilder.Sql(@"UPDATE [Dreams] SET [DisplayName] = CONCAT([FirstName], ' ', [LastName] )");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DisplayName",
                table: "Dreams");
        }
    }
}
