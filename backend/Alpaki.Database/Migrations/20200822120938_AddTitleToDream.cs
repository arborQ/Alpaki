using Microsoft.EntityFrameworkCore.Migrations;

namespace Alpaki.Database.Migrations
{
    public partial class AddTitleToDream : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Dreams",
                maxLength: 500,
                nullable: true);

            migrationBuilder.Sql(
                @"
UPDATE d
SET Title = 'Marzenie_' +CAST(d.DreamId AS NVARCHAR(50))
FROM Dreams d"
            );

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1L,
                column: "PasswordHash",
                value: "AQAAAAEAACcQAAAAENIwPZvr7ApQRDJhZF223Y6n7hsTF0vHft1A0VcwQ46dn5J3vEeP7MX8tNSjT3uv6A==");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Title",
                table: "Dreams");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1L,
                column: "PasswordHash",
                value: "ANsulF8MvjDYgyN8kMxN8a4Rljleu+uCtWbPlTpy33vZfIeSK+fYUms+nz6lfSGQUg==");
        }
    }
}
