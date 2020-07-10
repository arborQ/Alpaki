using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Alpaki.Database.Migrations
{
    public partial class MergeDreamerAndDreamMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Dreams_Dreamers_DreamerId",
                table: "Dreams");

            migrationBuilder.DropTable(
                name: "Dreamers");

            migrationBuilder.DropIndex(
                name: "IX_Dreams_DreamerId",
                table: "Dreams");

            migrationBuilder.DeleteData(
                table: "Dreams",
                keyColumn: "DreamId",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 2L);

            migrationBuilder.DropColumn(
                name: "DreamerId",
                table: "Dreams");

            migrationBuilder.AddColumn<int>(
                name: "Age",
                table: "Dreams",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "DreamUrl",
                table: "Dreams",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Dreams",
                maxLength: 250,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Gender",
                table: "Dreams",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "Dreams",
                maxLength: 250,
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1L,
                column: "Role",
                value: 7);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Age",
                table: "Dreams");

            migrationBuilder.DropColumn(
                name: "DreamUrl",
                table: "Dreams");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Dreams");

            migrationBuilder.DropColumn(
                name: "Gender",
                table: "Dreams");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "Dreams");

            migrationBuilder.AddColumn<long>(
                name: "DreamerId",
                table: "Dreams",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateTable(
                name: "Dreamers",
                columns: table => new
                {
                    DreamerId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Age = table.Column<int>(type: "int", nullable: false),
                    DreamUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Gender = table.Column<int>(type: "int", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dreamers", x => x.DreamerId);
                });

            migrationBuilder.InsertData(
                table: "Dreamers",
                columns: new[] { "DreamerId", "Age", "DreamUrl", "FirstName", "Gender", "LastName" },
                values: new object[] { 1L, 35, "http://google.com", "Łukasz", 1, "Wójcik" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1L,
                column: "Role",
                value: 0);

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Brand", "Email", "FirstName", "LastName", "PhoneNumber", "Role" },
                values: new object[] { 2L, null, "volunteer@volunteer.pl", "volunteer", "volunteer", null, 1 });

            migrationBuilder.InsertData(
                table: "Dreams",
                columns: new[] { "DreamId", "DreamCategoryId", "DreamComeTrueDate", "DreamState", "DreamerId", "Tags" },
                values: new object[] { 1L, 1L, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), 0, 1L, "#fromSeed" });

            migrationBuilder.CreateIndex(
                name: "IX_Dreams_DreamerId",
                table: "Dreams",
                column: "DreamerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Dreams_Dreamers_DreamerId",
                table: "Dreams",
                column: "DreamerId",
                principalTable: "Dreamers",
                principalColumn: "DreamerId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
