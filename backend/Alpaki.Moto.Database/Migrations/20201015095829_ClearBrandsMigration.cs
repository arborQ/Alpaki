using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Alpaki.Moto.Database.Migrations
{
    public partial class ClearBrandsMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var file = File();

            var index = 0;
            var moedlIndex = 0;
            foreach (var brand in file)
            {
                migrationBuilder.InsertData(
                    schema: "Moto",
                    table: "Brand",
                    columns: new[] { "BrandId", "BrandName", "IsActive" },
                    values: new object[,]
                        {
                            { ++index, brand.brand, true },
                        }
                );

                foreach (var model in brand.models)
                {
                    migrationBuilder.InsertData(
                    schema: "Moto",
                    table: "Model",
                    columns: new[] { "BrandId", "ModelName", "ModelId" },
                    values: new object[,]
                        {
                            { index, model, ++moedlIndex },
                        }
                    );
                }
            }


        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("TRUNCATE TABLE [Moto].[Model]");
            migrationBuilder.Sql("TRUNCATE TABLE [Moto].[Brand]");
        }

        private BrandMigrationItem[] File()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = "Alpaki.Moto.Database.Json.brands.json";
            var names = assembly.GetManifestResourceNames();

            foreach (var name in names)
            {
                Console.WriteLine(name);
            }

            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            using (StreamReader reader = new StreamReader(stream))
            {
                var file = reader.ReadToEnd();

                return JsonSerializer.Deserialize<BrandMigrationItem[]>(file);
            }
        }

        class BrandMigrationItem
        {
            public string brand { get; set; }

            public string[] models { get; set; }
        }
    }
}
