using Microsoft.EntityFrameworkCore.Migrations;

namespace Alpaki.Database.Migrations
{
    public partial class MoveSchemasMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AssignedDreams_Dreams_DreamId",
                table: "AssignedDreams");

            migrationBuilder.DropForeignKey(
                name: "FK_AssignedDreams_Users_VolunteerId",
                table: "AssignedDreams");

            migrationBuilder.DropForeignKey(
                name: "FK_AssignedSponsors_Dreams_DreamId",
                table: "AssignedSponsors");

            migrationBuilder.DropForeignKey(
                name: "FK_AssignedSponsors_Sponsors_SponsorId",
                table: "AssignedSponsors");

            migrationBuilder.DropForeignKey(
                name: "FK_DreamCategoryDefaultSteps_DreamCategories_DreamCategoryId",
                table: "DreamCategoryDefaultSteps");

            migrationBuilder.DropForeignKey(
                name: "FK_Dreams_DreamCategories_DreamCategoryId",
                table: "Dreams");

            migrationBuilder.DropForeignKey(
                name: "FK_Dreams_Images_DreamImageId",
                table: "Dreams");

            migrationBuilder.DropForeignKey(
                name: "FK_DreamSteps_Dreams_DreamId",
                table: "DreamSteps");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Images_ProfileImageId",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Sponsors",
                table: "Sponsors");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Images",
                table: "Images");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DreamSteps",
                table: "DreamSteps");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Dreams",
                table: "Dreams");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DreamCategoryDefaultSteps",
                table: "DreamCategoryDefaultSteps");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DreamCategories",
                table: "DreamCategories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AssignedSponsors",
                table: "AssignedSponsors");

            migrationBuilder.EnsureSchema(
                name: "Dreams");

            migrationBuilder.EnsureSchema(
                name: "Shared");

            migrationBuilder.RenameTable(
                name: "AssignedDreams",
                newName: "AssignedDreams",
                newSchema: "Dreams");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "User",
                newSchema: "Shared");

            migrationBuilder.RenameTable(
                name: "Sponsors",
                newName: "Sponsor",
                newSchema: "Dreams");

            migrationBuilder.RenameTable(
                name: "Images",
                newName: "Image",
                newSchema: "Shared");

            migrationBuilder.RenameTable(
                name: "DreamSteps",
                newName: "DreamStep",
                newSchema: "Dreams");

            migrationBuilder.RenameTable(
                name: "Dreams",
                newName: "Dream",
                newSchema: "Dreams");

            migrationBuilder.RenameTable(
                name: "DreamCategoryDefaultSteps",
                newName: "DreamCategoryDefaultStep",
                newSchema: "Dreams");

            migrationBuilder.RenameTable(
                name: "DreamCategories",
                newName: "DreamCategory",
                newSchema: "Dreams");

            migrationBuilder.RenameTable(
                name: "AssignedSponsors",
                newName: "AssignedSponsor",
                newSchema: "Dreams");

            migrationBuilder.RenameIndex(
                name: "IX_Users_ProfileImageId",
                schema: "Shared",
                table: "User",
                newName: "IX_User_ProfileImageId");

            migrationBuilder.RenameIndex(
                name: "IX_DreamSteps_DreamId",
                schema: "Dreams",
                table: "DreamStep",
                newName: "IX_DreamStep_DreamId");

            migrationBuilder.RenameIndex(
                name: "IX_Dreams_DreamImageId",
                schema: "Dreams",
                table: "Dream",
                newName: "IX_Dream_DreamImageId");

            migrationBuilder.RenameIndex(
                name: "IX_Dreams_DreamCategoryId",
                schema: "Dreams",
                table: "Dream",
                newName: "IX_Dream_DreamCategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_DreamCategoryDefaultSteps_DreamCategoryId",
                schema: "Dreams",
                table: "DreamCategoryDefaultStep",
                newName: "IX_DreamCategoryDefaultStep_DreamCategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_AssignedSponsors_SponsorId",
                schema: "Dreams",
                table: "AssignedSponsor",
                newName: "IX_AssignedSponsor_SponsorId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_User",
                schema: "Shared",
                table: "User",
                column: "UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Sponsor",
                schema: "Dreams",
                table: "Sponsor",
                column: "SponsorId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Image",
                schema: "Shared",
                table: "Image",
                column: "ImageId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DreamStep",
                schema: "Dreams",
                table: "DreamStep",
                column: "DreamStepId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Dream",
                schema: "Dreams",
                table: "Dream",
                column: "DreamId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DreamCategoryDefaultStep",
                schema: "Dreams",
                table: "DreamCategoryDefaultStep",
                column: "DreamCategoryDefaultStepId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DreamCategory",
                schema: "Dreams",
                table: "DreamCategory",
                column: "DreamCategoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AssignedSponsor",
                schema: "Dreams",
                table: "AssignedSponsor",
                columns: new[] { "DreamId", "SponsorId" });

            migrationBuilder.AddForeignKey(
                name: "FK_AssignedDreams_Dream_DreamId",
                schema: "Dreams",
                table: "AssignedDreams",
                column: "DreamId",
                principalSchema: "Dreams",
                principalTable: "Dream",
                principalColumn: "DreamId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AssignedDreams_User_VolunteerId",
                schema: "Dreams",
                table: "AssignedDreams",
                column: "VolunteerId",
                principalSchema: "Shared",
                principalTable: "User",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AssignedSponsor_Dream_DreamId",
                schema: "Dreams",
                table: "AssignedSponsor",
                column: "DreamId",
                principalSchema: "Dreams",
                principalTable: "Dream",
                principalColumn: "DreamId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AssignedSponsor_Sponsor_SponsorId",
                schema: "Dreams",
                table: "AssignedSponsor",
                column: "SponsorId",
                principalSchema: "Dreams",
                principalTable: "Sponsor",
                principalColumn: "SponsorId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Dream_DreamCategory_DreamCategoryId",
                schema: "Dreams",
                table: "Dream",
                column: "DreamCategoryId",
                principalSchema: "Dreams",
                principalTable: "DreamCategory",
                principalColumn: "DreamCategoryId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Dream_Image_DreamImageId",
                schema: "Dreams",
                table: "Dream",
                column: "DreamImageId",
                principalSchema: "Shared",
                principalTable: "Image",
                principalColumn: "ImageId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DreamCategoryDefaultStep_DreamCategory_DreamCategoryId",
                schema: "Dreams",
                table: "DreamCategoryDefaultStep",
                column: "DreamCategoryId",
                principalSchema: "Dreams",
                principalTable: "DreamCategory",
                principalColumn: "DreamCategoryId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DreamStep_Dream_DreamId",
                schema: "Dreams",
                table: "DreamStep",
                column: "DreamId",
                principalSchema: "Dreams",
                principalTable: "Dream",
                principalColumn: "DreamId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_User_Image_ProfileImageId",
                schema: "Shared",
                table: "User",
                column: "ProfileImageId",
                principalSchema: "Shared",
                principalTable: "Image",
                principalColumn: "ImageId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AssignedDreams_Dream_DreamId",
                schema: "Dreams",
                table: "AssignedDreams");

            migrationBuilder.DropForeignKey(
                name: "FK_AssignedDreams_User_VolunteerId",
                schema: "Dreams",
                table: "AssignedDreams");

            migrationBuilder.DropForeignKey(
                name: "FK_AssignedSponsor_Dream_DreamId",
                schema: "Dreams",
                table: "AssignedSponsor");

            migrationBuilder.DropForeignKey(
                name: "FK_AssignedSponsor_Sponsor_SponsorId",
                schema: "Dreams",
                table: "AssignedSponsor");

            migrationBuilder.DropForeignKey(
                name: "FK_Dream_DreamCategory_DreamCategoryId",
                schema: "Dreams",
                table: "Dream");

            migrationBuilder.DropForeignKey(
                name: "FK_Dream_Image_DreamImageId",
                schema: "Dreams",
                table: "Dream");

            migrationBuilder.DropForeignKey(
                name: "FK_DreamCategoryDefaultStep_DreamCategory_DreamCategoryId",
                schema: "Dreams",
                table: "DreamCategoryDefaultStep");

            migrationBuilder.DropForeignKey(
                name: "FK_DreamStep_Dream_DreamId",
                schema: "Dreams",
                table: "DreamStep");

            migrationBuilder.DropForeignKey(
                name: "FK_User_Image_ProfileImageId",
                schema: "Shared",
                table: "User");

            migrationBuilder.DropPrimaryKey(
                name: "PK_User",
                schema: "Shared",
                table: "User");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Image",
                schema: "Shared",
                table: "Image");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Sponsor",
                schema: "Dreams",
                table: "Sponsor");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DreamStep",
                schema: "Dreams",
                table: "DreamStep");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DreamCategoryDefaultStep",
                schema: "Dreams",
                table: "DreamCategoryDefaultStep");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DreamCategory",
                schema: "Dreams",
                table: "DreamCategory");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Dream",
                schema: "Dreams",
                table: "Dream");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AssignedSponsor",
                schema: "Dreams",
                table: "AssignedSponsor");

            migrationBuilder.RenameTable(
                name: "AssignedDreams",
                schema: "Dreams",
                newName: "AssignedDreams");

            migrationBuilder.RenameTable(
                name: "User",
                schema: "Shared",
                newName: "Users");

            migrationBuilder.RenameTable(
                name: "Image",
                schema: "Shared",
                newName: "Images");

            migrationBuilder.RenameTable(
                name: "Sponsor",
                schema: "Dreams",
                newName: "Sponsors");

            migrationBuilder.RenameTable(
                name: "DreamStep",
                schema: "Dreams",
                newName: "DreamSteps");

            migrationBuilder.RenameTable(
                name: "DreamCategoryDefaultStep",
                schema: "Dreams",
                newName: "DreamCategoryDefaultSteps");

            migrationBuilder.RenameTable(
                name: "DreamCategory",
                schema: "Dreams",
                newName: "DreamCategories");

            migrationBuilder.RenameTable(
                name: "Dream",
                schema: "Dreams",
                newName: "Dreams");

            migrationBuilder.RenameTable(
                name: "AssignedSponsor",
                schema: "Dreams",
                newName: "AssignedSponsors");

            migrationBuilder.RenameIndex(
                name: "IX_User_ProfileImageId",
                table: "Users",
                newName: "IX_Users_ProfileImageId");

            migrationBuilder.RenameIndex(
                name: "IX_DreamStep_DreamId",
                table: "DreamSteps",
                newName: "IX_DreamSteps_DreamId");

            migrationBuilder.RenameIndex(
                name: "IX_DreamCategoryDefaultStep_DreamCategoryId",
                table: "DreamCategoryDefaultSteps",
                newName: "IX_DreamCategoryDefaultSteps_DreamCategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Dream_DreamImageId",
                table: "Dreams",
                newName: "IX_Dreams_DreamImageId");

            migrationBuilder.RenameIndex(
                name: "IX_Dream_DreamCategoryId",
                table: "Dreams",
                newName: "IX_Dreams_DreamCategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_AssignedSponsor_SponsorId",
                table: "AssignedSponsors",
                newName: "IX_AssignedSponsors_SponsorId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Images",
                table: "Images",
                column: "ImageId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Sponsors",
                table: "Sponsors",
                column: "SponsorId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DreamSteps",
                table: "DreamSteps",
                column: "DreamStepId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DreamCategoryDefaultSteps",
                table: "DreamCategoryDefaultSteps",
                column: "DreamCategoryDefaultStepId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DreamCategories",
                table: "DreamCategories",
                column: "DreamCategoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Dreams",
                table: "Dreams",
                column: "DreamId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AssignedSponsors",
                table: "AssignedSponsors",
                columns: new[] { "DreamId", "SponsorId" });

            migrationBuilder.AddForeignKey(
                name: "FK_AssignedDreams_Dreams_DreamId",
                table: "AssignedDreams",
                column: "DreamId",
                principalTable: "Dreams",
                principalColumn: "DreamId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AssignedDreams_Users_VolunteerId",
                table: "AssignedDreams",
                column: "VolunteerId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AssignedSponsors_Dreams_DreamId",
                table: "AssignedSponsors",
                column: "DreamId",
                principalTable: "Dreams",
                principalColumn: "DreamId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AssignedSponsors_Sponsors_SponsorId",
                table: "AssignedSponsors",
                column: "SponsorId",
                principalTable: "Sponsors",
                principalColumn: "SponsorId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DreamCategoryDefaultSteps_DreamCategories_DreamCategoryId",
                table: "DreamCategoryDefaultSteps",
                column: "DreamCategoryId",
                principalTable: "DreamCategories",
                principalColumn: "DreamCategoryId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Dreams_DreamCategories_DreamCategoryId",
                table: "Dreams",
                column: "DreamCategoryId",
                principalTable: "DreamCategories",
                principalColumn: "DreamCategoryId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Dreams_Images_DreamImageId",
                table: "Dreams",
                column: "DreamImageId",
                principalTable: "Images",
                principalColumn: "ImageId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DreamSteps_Dreams_DreamId",
                table: "DreamSteps",
                column: "DreamId",
                principalTable: "Dreams",
                principalColumn: "DreamId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Images_ProfileImageId",
                table: "Users",
                column: "ProfileImageId",
                principalTable: "Images",
                principalColumn: "ImageId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
