using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Alpaki.Moto.Database.Migrations
{
    public partial class BrandDomainEventMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BrandDomainEvent",
                schema: "Moto",
                columns: table => new
                {
                    BrandDomainEventId = table.Column<Guid>(nullable: false),
                    DomainEventType = table.Column<int>(nullable: false),
                    Created = table.Column<DateTimeOffset>(nullable: false),
                    Processed = table.Column<DateTimeOffset>(nullable: true),
                    BrandId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BrandDomainEvent", x => x.BrandDomainEventId);
                    table.ForeignKey(
                        name: "FK_BrandDomainEvent_Brand_BrandId",
                        column: x => x.BrandId,
                        principalSchema: "Moto",
                        principalTable: "Brand",
                        principalColumn: "BrandId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BrandDomainEvent_BrandId",
                schema: "Moto",
                table: "BrandDomainEvent",
                column: "BrandId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BrandDomainEvent",
                schema: "Moto");
        }
    }
}
