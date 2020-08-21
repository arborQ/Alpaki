using Microsoft.EntityFrameworkCore.Migrations;

namespace Alpaki.Database.Migrations
{
    public partial class AddSponsorsMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Sponsors",
                columns: table => new
                {
                    SponsorId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DisplayName = table.Column<string>(maxLength: 500, nullable: false),
                    ContactPersonName = table.Column<string>(maxLength: 500, nullable: true),
                    Email = table.Column<string>(maxLength: 500, nullable: false),
                    Brand = table.Column<string>(maxLength: 250, nullable: true),
                    CooperationType = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sponsors", x => x.SponsorId);
                });

            migrationBuilder.CreateTable(
                name: "AssignedSponsors",
                columns: table => new
                {
                    SponsorId = table.Column<long>(nullable: false),
                    DreamId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssignedSponsors", x => new { x.DreamId, x.SponsorId });
                    table.ForeignKey(
                        name: "FK_AssignedSponsors_Dreams_DreamId",
                        column: x => x.DreamId,
                        principalTable: "Dreams",
                        principalColumn: "DreamId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AssignedSponsors_Sponsors_SponsorId",
                        column: x => x.SponsorId,
                        principalTable: "Sponsors",
                        principalColumn: "SponsorId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AssignedSponsors_SponsorId",
                table: "AssignedSponsors",
                column: "SponsorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AssignedSponsors");

            migrationBuilder.DropTable(
                name: "Sponsors");
        }
    }
}
