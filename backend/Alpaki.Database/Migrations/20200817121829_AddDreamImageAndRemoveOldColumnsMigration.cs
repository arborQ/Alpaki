using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Alpaki.Database.Migrations
{
    public partial class AddDreamImageAndRemoveOldColumnsMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Dreams");

            migrationBuilder.DropColumn(
                name: "Gender",
                table: "Dreams");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "Dreams");

            migrationBuilder.AddColumn<Guid>(
                name: "DreamImageId",
                table: "Dreams",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Dreams_DreamImageId",
                table: "Dreams",
                column: "DreamImageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Dreams_Images_DreamImageId",
                table: "Dreams",
                column: "DreamImageId",
                principalTable: "Images",
                principalColumn: "ImageId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Dreams_Images_DreamImageId",
                table: "Dreams");

            migrationBuilder.DropIndex(
                name: "IX_Dreams_DreamImageId",
                table: "Dreams");

            migrationBuilder.DropColumn(
                name: "DreamImageId",
                table: "Dreams");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Dreams",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Gender",
                table: "Dreams",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "Dreams",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: false,
                defaultValue: "");
        }
    }
}
