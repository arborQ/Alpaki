using Microsoft.EntityFrameworkCore.Migrations;

namespace Alpaki.Moto.Database.Migrations
{
    public partial class AddMotoEventsMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
  INSERT INTO [Moto].[BrandDomainEvent] (
	[BrandDomainEventId]
    ,[DomainEventType]
    ,[Created]
    ,[Processed]
    ,[BrandId]
  ) SELECT NEWID() as [BrandDomainEventId]
      ,1 as [DomainEventType]
      ,GETDATE() as [Created]
      ,null as [Processed]
      ,[BrandId] FROM [Moto].[Brand]
");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
