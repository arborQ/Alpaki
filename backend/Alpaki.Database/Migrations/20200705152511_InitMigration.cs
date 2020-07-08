using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Alpaki.Database.Migrations
{
    public partial class InitMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DreamCategories",
                columns: table => new
                {
                    DreamCategoryId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(maxLength: 250, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DreamCategories", x => x.DreamCategoryId);
                });

            migrationBuilder.CreateTable(
                name: "Dreamers",
                columns: table => new
                {
                    DreamerId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(maxLength: 250, nullable: false),
                    LastName = table.Column<string>(maxLength: 250, nullable: false),
                    Age = table.Column<int>(nullable: false),
                    Gender = table.Column<int>(nullable: false),
                    DreamUrl = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dreamers", x => x.DreamerId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(maxLength: 250, nullable: false),
                    FirstName = table.Column<string>(maxLength: 250, nullable: false),
                    LastName = table.Column<string>(maxLength: 250, nullable: false),
                    Brand = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    Role = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Dreams",
                columns: table => new
                {
                    DreamId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tags = table.Column<string>(nullable: true),
                    DreamerId = table.Column<long>(nullable: false),
                    DreamCategoryId = table.Column<long>(nullable: false),
                    DreamComeTrueDate = table.Column<DateTimeOffset>(nullable: false),
                    DreamState = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dreams", x => x.DreamId);
                    table.ForeignKey(
                        name: "FK_Dreams_DreamCategories_DreamCategoryId",
                        column: x => x.DreamCategoryId,
                        principalTable: "DreamCategories",
                        principalColumn: "DreamCategoryId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Dreams_Dreamers_DreamerId",
                        column: x => x.DreamerId,
                        principalTable: "Dreamers",
                        principalColumn: "DreamerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AssignedDreams",
                columns: table => new
                {
                    VolunteerId = table.Column<long>(nullable: false),
                    DreamId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssignedDreams", x => new { x.DreamId, x.VolunteerId });
                    table.ForeignKey(
                        name: "FK_AssignedDreams_Dreams_DreamId",
                        column: x => x.DreamId,
                        principalTable: "Dreams",
                        principalColumn: "DreamId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AssignedDreams_Users_VolunteerId",
                        column: x => x.VolunteerId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DreamSteps",
                columns: table => new
                {
                    DreamStepId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StepDescription = table.Column<string>(nullable: false),
                    StepState = table.Column<int>(nullable: false),
                    DreamId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DreamSteps", x => x.DreamStepId);
                    table.ForeignKey(
                        name: "FK_DreamSteps_Dreams_DreamId",
                        column: x => x.DreamId,
                        principalTable: "Dreams",
                        principalColumn: "DreamId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "DreamCategories",
                columns: new[] { "DreamCategoryId", "CategoryName" },
                values: new object[,]
                {
                    { 1L, "Chcę dostać" },
                    { 2L, "Chcę poznać" },
                    { 3L, "Chcę pojechać" },
                    { 4L, "Chcę kimś się stać" },
                    { 5L, "Chcę komuś coś dać" }
                });

            migrationBuilder.InsertData(
                table: "Dreamers",
                columns: new[] { "DreamerId", "Age", "DreamUrl", "FirstName", "Gender", "LastName" },
                values: new object[] { 1L, 35, "http://google.com", "Łukasz", 1, "Wójcik" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Brand", "Email", "FirstName", "LastName", "PhoneNumber", "Role" },
                values: new object[,]
                {
                    { 1L, null, "admin@admin.pl", "admin", "admin", null, 0 },
                    { 2L, null, "volunteer@volunteer.pl", "volunteer", "volunteer", null, 1 }
                });

            migrationBuilder.InsertData(
                table: "Dreams",
                columns: new[] { "DreamId", "DreamCategoryId", "DreamComeTrueDate", "DreamState", "DreamerId", "Tags" },
                values: new object[] { 1L, 1L, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), 0, 1L, "#fromSeed" });

            migrationBuilder.CreateIndex(
                name: "IX_AssignedDreams_VolunteerId",
                table: "AssignedDreams",
                column: "VolunteerId");

            migrationBuilder.CreateIndex(
                name: "IX_Dreams_DreamCategoryId",
                table: "Dreams",
                column: "DreamCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Dreams_DreamerId",
                table: "Dreams",
                column: "DreamerId");

            migrationBuilder.CreateIndex(
                name: "IX_DreamSteps_DreamId",
                table: "DreamSteps",
                column: "DreamId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AssignedDreams");

            migrationBuilder.DropTable(
                name: "DreamSteps");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Dreams");

            migrationBuilder.DropTable(
                name: "DreamCategories");

            migrationBuilder.DropTable(
                name: "Dreamers");
        }
    }
}
