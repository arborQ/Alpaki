using Microsoft.EntityFrameworkCore.Migrations;

namespace Alpaki.Moto.Database.Migrations
{
    public partial class DefaultBrandEventsMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO [Moto].[BrandDomainEvent] (BrandDomainEventId, DomainEventType, Created, BrandId) SELECT NEWID() as BrandDomainEventId, 1 as DomainEventType, GETDATE() as Created, BrandId FROM [Moto].[Brand]");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
