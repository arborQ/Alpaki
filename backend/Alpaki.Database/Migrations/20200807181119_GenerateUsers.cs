﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace Alpaki.Database.Migrations
{
    public partial class GenerateUsers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1L,
                column: "PasswordHash",
                value: "AJKCznGWty5bOR9bS+5yHsv4fqYhi/WQR3GoGx08ILWeOJnttwR1ikC3Gb36QmBnpg==");
        }
    }
}
