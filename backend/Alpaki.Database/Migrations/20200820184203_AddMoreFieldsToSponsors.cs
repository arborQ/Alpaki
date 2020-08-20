using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Alpaki.Database.Migrations
{
    public partial class AddMoreFieldsToSponsors : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Brand",
                table: "Sponsors",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CooperationType",
                table: "Sponsors",
                nullable: false,
                defaultValue: 0);
            
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1L,
                column: "PasswordHash",
                value: "AQAAAAEAACcQAAAAECr/OIe5ZOWc1QneV/VOAb0QmYk9XFr5njO6yZF6zLusZbS5XGK43ZbYWqy/MX1B/A==");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Brand",
                table: "Sponsors");

            migrationBuilder.DropColumn(
                name: "CooperationType",
                table: "Sponsors");
            
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1L,
                column: "PasswordHash",
                value: "AQAAAAEAACcQAAAAEJ5BTYGa2q23unNIBXbW7qHqqM0oQRqO5THSV7IIpUK2WxIV32pgW2BlBSi2dUZi7Q==");
        }
    }
}
