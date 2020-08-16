using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Alpaki.Database.Migrations
{
    public partial class AddImagesMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ProfileImageId",
                table: "Users",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    ImageId = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    ImageData = table.Column<byte[]>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.ImageId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_ProfileImageId",
                table: "Users",
                column: "ProfileImageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Images_ProfileImageId",
                table: "Users",
                column: "ProfileImageId",
                principalTable: "Images",
                principalColumn: "ImageId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Images_ProfileImageId",
                table: "Users");

            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.DropIndex(
                name: "IX_Users_ProfileImageId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ProfileImageId",
                table: "Users");
        }
    }
}
