using Microsoft.EntityFrameworkCore.Migrations;

namespace Alpaki.Moto.Database.Migrations
{
    public partial class SeedBrandsMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                schema: "Moto",
                table: "Brand",
                columns: new[] { "BrandId", "BrandName" },
                values: new object[,]
                {
                    { 1L, "AC" },
                    { 172L, "NOBLE" },
                    { 173L, "NOTA" },
                    { 174L, "OLDSMOBILE" },
                    { 175L, "OPEL" },
                    { 176L, "OPTIMAL ENERGY" },
                    { 177L, "ORCA" },
                    { 178L, "OLTCIT" },
                    { 179L, "PAGANI" },
                    { 180L, "PANHARD" },
                    { 181L, "PANOZ" },
                    { 182L, "PERANA" },
                    { 183L, "PERODUA" },
                    { 184L, "PEUGEOT" },
                    { 185L, "P.G.O." },
                    { 171L, "NISSAN" },
                    { 186L, "POLARSUN" },
                    { 188L, "PORSCHE" },
                    { 189L, "PROTO" },
                    { 190L, "OULLIM" },
                    { 191L, "PROTON" },
                    { 192L, "PURITALIA" },
                    { 193L, "QOROS" },
                    { 194L, "QVALE" },
                    { 195L, "RADICAL" },
                    { 196L, "RELIANT" },
                    { 197L, "RENAULT" },
                    { 198L, "REVA" },
                    { 199L, "RIMAC" },
                    { 200L, "RINSPEED" },
                    { 201L, "RODING" },
                    { 187L, "PLYMOUTH" },
                    { 202L, "ROEWE" },
                    { 170L, "MYVI-PERODUA" },
                    { 168L, "MULLEN" },
                    { 138L, "LARAKI" },
                    { 139L, "LEBLANC" },
                    { 140L, "LEITCH" },
                    { 141L, "LEOPARD" },
                    { 142L, "LEXUS" },
                    { 143L, "LI-ION" },
                    { 144L, "LIFAN" },
                    { 145L, "LIGHTNING" },
                    { 146L, "LINCOLN" },
                    { 147L, "LISTER" },
                    { 148L, "LOCAL MOTORS" },
                    { 149L, "LOBINI" },
                    { 150L, "LOTEC" },
                    { 151L, "LOTUS CARS" },
                    { 169L, "MYCAR" },
                    { 152L, "LUCRA CARS" },
                    { 154L, "MAHINDRA" },
                    { 155L, "MARUSSIA" },
                    { 156L, "MARUTI SUZUKI" },
                    { 157L, "MASERATI" },
                    { 158L, "MASTRETTA" },
                    { 159L, "MAZDA" },
                    { 160L, "MCLAREN" },
                    { 161L, "MERCEDES-BENZ" },
                    { 162L, "MG" },
                    { 163L, "MICRO" },
                    { 164L, "MINI" },
                    { 165L, "MITSUBISHI" },
                    { 166L, "MITSUOKA" },
                    { 167L, "MORGAN" },
                    { 153L, "LUXGEN" },
                    { 137L, "LANDWIND" },
                    { 203L, "ROLLS-ROYCE" },
                    { 205L, "ROSSION" },
                    { 240L, "TRUMPCHI" },
                    { 241L, "TUSHEK" },
                    { 242L, "TVR" },
                    { 243L, "TVS" },
                    { 244L, "ULTIMA" },
                    { 245L, "UMM" },
                    { 246L, "UEV" },
                    { 247L, "URI" },
                    { 248L, "UAZ" },
                    { 249L, "VAUXHALL MOTORS" },
                    { 250L, "VECTOR" },
                    { 251L, "VENCER" },
                    { 252L, "VENIRAUTO" },
                    { 253L, "VENTURI" },
                    { 239L, "TROLLER" },
                    { 254L, "VEPR" },
                    { 256L, "VOLVO" },
                    { 257L, "VINFAST" },
                    { 258L, "W MOTORS" },
                    { 259L, "WALLYSCAR" },
                    { 260L, "WESTFIELD" },
                    { 261L, "WHEEGO" },
                    { 262L, "WIESMANN" },
                    { 263L, "XENIA" },
                    { 264L, "YES!" },
                    { 265L, "YOUABIAN PUMA" },
                    { 266L, "ZASTAVA AUTOMOBILES" },
                    { 267L, "ZENDER CARS" },
                    { 268L, "ZENOS CARS" },
                    { 269L, "ZENVO" },
                    { 255L, "VOLKSWAGEN" },
                    { 204L, "ROSSIN-BERTIN" },
                    { 238L, "TRIUMPH" },
                    { 236L, "TREKKA" },
                    { 206L, "ROVER" },
                    { 207L, "SAAB" },
                    { 208L, "SALEEN" },
                    { 209L, "SAIC-GM-WULING" },
                    { 210L, "SAIPA" },
                    { 211L, "SAKER" },
                    { 212L, "SAMSUNG" },
                    { 213L, "SAN" },
                    { 214L, "SBARRO" },
                    { 215L, "SCION" },
                    { 216L, "SEAT" },
                    { 217L, "SHANGHAI MAPLE" },
                    { 218L, "SIN" },
                    { 219L, "ŠKODA" },
                    { 237L, "TRIDENT" },
                    { 220L, "SMART" },
                    { 222L, "SPYKER" },
                    { 223L, "SSANGYONG" },
                    { 224L, "SSC NORTH AMERICA" },
                    { 225L, "STREET & RACING TECHNOLOGY" },
                    { 226L, "SUBARU" },
                    { 227L, "SUZUKI" },
                    { 228L, "TANOM" },
                    { 229L, "TATA" },
                    { 230L, "TAURO" },
                    { 231L, "TAWON CAR" },
                    { 232L, "TD CARS" },
                    { 233L, "TESLA" },
                    { 234L, "THAI RUNG" },
                    { 235L, "TOYOTA" },
                    { 221L, "SPADA VETTURE SPORT" },
                    { 270L, "ZIL" },
                    { 136L, "LAND ROVER" },
                    { 134L, "LAMBORGHINI" },
                    { 36L, "CARBONTECH" },
                    { 37L, "CARICE" },
                    { 38L, "CHANG'AN" },
                    { 39L, "CHANGHE" },
                    { 40L, "CHERY" },
                    { 41L, "CHEVROLET" },
                    { 42L, "CHEVRON" },
                    { 43L, "CITROËN" },
                    { 44L, "CHRYSLER" },
                    { 45L, "COMMUTER CARS" },
                    { 46L, "CONNAUGHT" },
                    { 47L, "COVINI" },
                    { 48L, "DACIA" },
                    { 49L, "DAIHATSU" },
                    { 35L, "CAPARO" },
                    { 50L, "DATSUN" },
                    { 52L, "DMC" },
                    { 53L, "DIARDI" },
                    { 54L, "DODGE" },
                    { 55L, "DONKERVOORT" },
                    { 56L, "DONGFENG" },
                    { 57L, "DONTO" },
                    { 58L, "DS AUTOMOBILES" },
                    { 59L, "DYNASTI ELECTRIC CAR CORP." },
                    { 60L, "E-VADE" },
                    { 61L, "EFFEDI" },
                    { 62L, "EGY-TECH ENGINEERING" },
                    { 63L, "ELECTRIC RACEABOUT" },
                    { 64L, "ELFIN" },
                    { 65L, "EMGRAND" },
                    { 51L, "DE LA CHAPELLE" },
                    { 66L, "ENGLON" },
                    { 34L, "CADILLAC" },
                    { 32L, "BUICK" },
                    { 2L, "AC PROPULSION" },
                    { 3L, "ACURA" },
                    { 4L, "A.D. TRAMONTANA" },
                    { 5L, "ALFA ROMEO" },
                    { 6L, "ALMAC" },
                    { 7L, "ALTERNATIVE CARS" },
                    { 8L, "AMUZA" },
                    { 9L, "ANTEROS" },
                    { 10L, "ARASH" },
                    { 11L, "ARIEL" },
                    { 12L, "ARRINERA" },
                    { 13L, "ASL" },
                    { 14L, "ASTERIO" },
                    { 15L, "ASTON MARTIN" },
                    { 33L, "BYD" },
                    { 16L, "AUDI" },
                    { 18L, "BAJAJ" },
                    { 19L, "BEIJING AUTOMOBILE WORKS" },
                    { 20L, "BENTLEY" },
                    { 21L, "BMW" },
                    { 22L, "BOLLORÉ" },
                    { 23L, "BOLWELL" },
                    { 24L, "BRILLIANCE / HUACHEN" },
                    { 25L, "BRISTOL" },
                    { 26L, "BRITISH LEYLAND" },
                    { 27L, "BRM BUGGY" },
                    { 28L, "BROOKE" },
                    { 29L, "BUDDY" },
                    { 30L, "BUFORI" },
                    { 31L, "BUGATTI" },
                    { 17L, "BAC" },
                    { 135L, "LANCIA" },
                    { 67L, "ETERNITI" },
                    { 69L, "EQUUS" },
                    { 104L, "HTT TECHNOLOGIES" },
                    { 105L, "HULME" },
                    { 106L, "HYUNDAI" },
                    { 107L, "ICML" },
                    { 108L, "IFR" },
                    { 109L, "IRAN KHODRO" },
                    { 110L, "IKCO" },
                    { 111L, "IMPERIA" },
                    { 112L, "INFINITI" },
                    { 113L, "IVM" },
                    { 114L, "INVICTA" },
                    { 115L, "ISDERA" },
                    { 116L, "ISUZU" },
                    { 117L, "JAC" },
                    { 103L, "HRADYESH" },
                    { 118L, "JAGUAR" },
                    { 120L, "JENSEN MOTORS" },
                    { 121L, "JETCAR" },
                    { 122L, "JONWAY" },
                    { 123L, "JOSS" },
                    { 124L, "KAIPAN" },
                    { 125L, "KANTANKA" },
                    { 126L, "KARMA" },
                    { 127L, "KOENIGSEGG" },
                    { 128L, "KORRES" },
                    { 129L, "KIA" },
                    { 130L, "KIAT" },
                    { 131L, "KISH KHODRO" },
                    { 132L, "KTM" },
                    { 133L, "LADA" },
                    { 119L, "JEEP" },
                    { 68L, "ETOX" },
                    { 102L, "HONGQI" },
                    { 100L, "HOLDEN" },
                    { 70L, "EXAGON" },
                    { 71L, "FARALLI & MAZZANTI" },
                    { 72L, "FAW" },
                    { 73L, "FERRARI" },
                    { 74L, "FIAT" },
                    { 75L, "FISKER" },
                    { 76L, "FODAY" },
                    { 77L, "FORCE" },
                    { 78L, "FORD" },
                    { 79L, "FORD AUSTRALIA" },
                    { 80L, "FORD GERMANY" },
                    { 81L, "FORNASARI" },
                    { 82L, "FRASER" },
                    { 83L, "GAC GROUP" },
                    { 101L, "HONDA" },
                    { 84L, "GALPIN" },
                    { 86L, "GENESIS" },
                    { 87L, "GIBBS" },
                    { 88L, "GILLET" },
                    { 89L, "GINETTA" },
                    { 90L, "GMC" },
                    { 91L, "GONOW" },
                    { 92L, "GREAT WALL / CHANGCHENG" },
                    { 93L, "GREENTECH AUTOMOTIVE" },
                    { 94L, "GRINNALL" },
                    { 95L, "GTA MOTOR" },
                    { 96L, "GUMPERT" },
                    { 97L, "GURGEL" },
                    { 98L, "HENNESSEY" },
                    { 99L, "HINDUSTAN" },
                    { 85L, "GEELY" },
                    { 271L, "ZX AUTO" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "Moto",
                table: "Brand",
                keyColumn: "BrandId",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                schema: "Moto",
                table: "Brand",
                keyColumn: "BrandId",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                schema: "Moto",
                table: "Brand",
                keyColumn: "BrandId",
                keyValue: 3L);

            migrationBuilder.DeleteData(
                schema: "Moto",
                table: "Brand",
                keyColumn: "BrandId",
                keyValue: 4L);

            migrationBuilder.DeleteData(
                schema: "Moto",
                table: "Brand",
                keyColumn: "BrandId",
                keyValue: 5L);

            migrationBuilder.DeleteData(
                schema: "Moto",
                table: "Brand",
                keyColumn: "BrandId",
                keyValue: 6L);

            migrationBuilder.DeleteData(
                schema: "Moto",
                table: "Brand",
                keyColumn: "BrandId",
                keyValue: 7L);

            migrationBuilder.DeleteData(
                schema: "Moto",
                table: "Brand",
                keyColumn: "BrandId",
                keyValue: 8L);

            migrationBuilder.DeleteData(
                schema: "Moto",
                table: "Brand",
                keyColumn: "BrandId",
                keyValue: 9L);

            migrationBuilder.DeleteData(
                schema: "Moto",
                table: "Brand",
                keyColumn: "BrandId",
                keyValue: 10L);

            migrationBuilder.DeleteData(
                schema: "Moto",
                table: "Brand",
                keyColumn: "BrandId",
                keyValue: 11L);

            migrationBuilder.DeleteData(
                schema: "Moto",
                table: "Brand",
                keyColumn: "BrandId",
                keyValue: 12L);

            migrationBuilder.DeleteData(
                schema: "Moto",
                table: "Brand",
                keyColumn: "BrandId",
                keyValue: 13L);

            migrationBuilder.DeleteData(
                schema: "Moto",
                table: "Brand",
                keyColumn: "BrandId",
                keyValue: 14L);

            migrationBuilder.DeleteData(
                schema: "Moto",
                table: "Brand",
                keyColumn: "BrandId",
                keyValue: 15L);

            migrationBuilder.DeleteData(
                schema: "Moto",
                table: "Brand",
                keyColumn: "BrandId",
                keyValue: 16L);

            migrationBuilder.DeleteData(
                schema: "Moto",
                table: "Brand",
                keyColumn: "BrandId",
                keyValue: 17L);

            migrationBuilder.DeleteData(
                schema: "Moto",
                table: "Brand",
                keyColumn: "BrandId",
                keyValue: 18L);

            migrationBuilder.DeleteData(
                schema: "Moto",
                table: "Brand",
                keyColumn: "BrandId",
                keyValue: 19L);

            migrationBuilder.DeleteData(
                schema: "Moto",
                table: "Brand",
                keyColumn: "BrandId",
                keyValue: 20L);

            migrationBuilder.DeleteData(
                schema: "Moto",
                table: "Brand",
                keyColumn: "BrandId",
                keyValue: 21L);

            migrationBuilder.DeleteData(
                schema: "Moto",
                table: "Brand",
                keyColumn: "BrandId",
                keyValue: 22L);

            migrationBuilder.DeleteData(
                schema: "Moto",
                table: "Brand",
                keyColumn: "BrandId",
                keyValue: 23L);

            migrationBuilder.DeleteData(
                schema: "Moto",
                table: "Brand",
                keyColumn: "BrandId",
                keyValue: 24L);

            migrationBuilder.DeleteData(
                schema: "Moto",
                table: "Brand",
                keyColumn: "BrandId",
                keyValue: 25L);

            migrationBuilder.DeleteData(
                schema: "Moto",
                table: "Brand",
                keyColumn: "BrandId",
                keyValue: 26L);

            migrationBuilder.DeleteData(
                schema: "Moto",
                table: "Brand",
                keyColumn: "BrandId",
                keyValue: 27L);

            migrationBuilder.DeleteData(
                schema: "Moto",
                table: "Brand",
                keyColumn: "BrandId",
                keyValue: 28L);

            migrationBuilder.DeleteData(
                schema: "Moto",
                table: "Brand",
                keyColumn: "BrandId",
                keyValue: 29L);

            migrationBuilder.DeleteData(
                schema: "Moto",
                table: "Brand",
                keyColumn: "BrandId",
                keyValue: 30L);

            migrationBuilder.DeleteData(
                schema: "Moto",
                table: "Brand",
                keyColumn: "BrandId",
                keyValue: 31L);

            migrationBuilder.DeleteData(
                schema: "Moto",
                table: "Brand",
                keyColumn: "BrandId",
                keyValue: 32L);

            migrationBuilder.DeleteData(
                schema: "Moto",
                table: "Brand",
                keyColumn: "BrandId",
                keyValue: 33L);

            migrationBuilder.DeleteData(
                schema: "Moto",
                table: "Brand",
                keyColumn: "BrandId",
                keyValue: 34L);

            migrationBuilder.DeleteData(
                schema: "Moto",
                table: "Brand",
                keyColumn: "BrandId",
                keyValue: 35L);

            migrationBuilder.DeleteData(
                schema: "Moto",
                table: "Brand",
                keyColumn: "BrandId",
                keyValue: 36L);

            migrationBuilder.DeleteData(
                schema: "Moto",
                table: "Brand",
                keyColumn: "BrandId",
                keyValue: 37L);

            migrationBuilder.DeleteData(
                schema: "Moto",
                table: "Brand",
                keyColumn: "BrandId",
                keyValue: 38L);

            migrationBuilder.DeleteData(
                schema: "Moto",
                table: "Brand",
                keyColumn: "BrandId",
                keyValue: 39L);

            migrationBuilder.DeleteData(
                schema: "Moto",
                table: "Brand",
                keyColumn: "BrandId",
                keyValue: 40L);

            migrationBuilder.DeleteData(
                schema: "Moto",
                table: "Brand",
                keyColumn: "BrandId",
                keyValue: 41L);

            migrationBuilder.DeleteData(
                schema: "Moto",
                table: "Brand",
                keyColumn: "BrandId",
                keyValue: 42L);

            migrationBuilder.DeleteData(
                schema: "Moto",
                table: "Brand",
                keyColumn: "BrandId",
                keyValue: 43L);

            migrationBuilder.DeleteData(
                schema: "Moto",
                table: "Brand",
                keyColumn: "BrandId",
                keyValue: 44L);

            migrationBuilder.DeleteData(
                schema: "Moto",
                table: "Brand",
                keyColumn: "BrandId",
                keyValue: 45L);

            migrationBuilder.DeleteData(
                schema: "Moto",
                table: "Brand",
                keyColumn: "BrandId",
                keyValue: 46L);

            migrationBuilder.DeleteData(
                schema: "Moto",
                table: "Brand",
                keyColumn: "BrandId",
                keyValue: 47L);

            migrationBuilder.DeleteData(
                schema: "Moto",
                table: "Brand",
                keyColumn: "BrandId",
                keyValue: 48L);

            migrationBuilder.DeleteData(
                schema: "Moto",
                table: "Brand",
                keyColumn: "BrandId",
                keyValue: 49L);

            migrationBuilder.DeleteData(
                schema: "Moto",
                table: "Brand",
                keyColumn: "BrandId",
                keyValue: 50L);

            migrationBuilder.DeleteData(
                schema: "Moto",
                table: "Brand",
                keyColumn: "BrandId",
                keyValue: 51L);

            migrationBuilder.DeleteData(
                schema: "Moto",
                table: "Brand",
                keyColumn: "BrandId",
                keyValue: 52L);

            migrationBuilder.DeleteData(
                schema: "Moto",
                table: "Brand",
                keyColumn: "BrandId",
                keyValue: 53L);

            migrationBuilder.DeleteData(
                schema: "Moto",
                table: "Brand",
                keyColumn: "BrandId",
                keyValue: 54L);

            migrationBuilder.DeleteData(
                schema: "Moto",
                table: "Brand",
                keyColumn: "BrandId",
                keyValue: 55L);

            migrationBuilder.DeleteData(
                schema: "Moto",
                table: "Brand",
                keyColumn: "BrandId",
                keyValue: 56L);

            migrationBuilder.DeleteData(
                schema: "Moto",
                table: "Brand",
                keyColumn: "BrandId",
                keyValue: 57L);

            migrationBuilder.DeleteData(
                schema: "Moto",
                table: "Brand",
                keyColumn: "BrandId",
                keyValue: 58L);

            migrationBuilder.DeleteData(
                schema: "Moto",
                table: "Brand",
                keyColumn: "BrandId",
                keyValue: 59L);

            migrationBuilder.DeleteData(
                schema: "Moto",
                table: "Brand",
                keyColumn: "BrandId",
                keyValue: 60L);

            migrationBuilder.DeleteData(
                schema: "Moto",
                table: "Brand",
                keyColumn: "BrandId",
                keyValue: 61L);

            migrationBuilder.DeleteData(
                schema: "Moto",
                table: "Brand",
                keyColumn: "BrandId",
                keyValue: 62L);

            migrationBuilder.DeleteData(
                schema: "Moto",
                table: "Brand",
                keyColumn: "BrandId",
                keyValue: 63L);

            migrationBuilder.DeleteData(
                schema: "Moto",
                table: "Brand",
                keyColumn: "BrandId",
                keyValue: 64L);

            migrationBuilder.DeleteData(
                schema: "Moto",
                table: "Brand",
                keyColumn: "BrandId",
                keyValue: 65L);

            migrationBuilder.DeleteData(
                schema: "Moto",
                table: "Brand",
                keyColumn: "BrandId",
                keyValue: 66L);

            migrationBuilder.DeleteData(
                schema: "Moto",
                table: "Brand",
                keyColumn: "BrandId",
                keyValue: 67L);

            migrationBuilder.DeleteData(
                schema: "Moto",
                table: "Brand",
                keyColumn: "BrandId",
                keyValue: 68L);

            migrationBuilder.DeleteData(
                schema: "Moto",
                table: "Brand",
                keyColumn: "BrandId",
                keyValue: 69L);

            migrationBuilder.DeleteData(
                schema: "Moto",
                table: "Brand",
                keyColumn: "BrandId",
                keyValue: 70L);

            migrationBuilder.DeleteData(
                schema: "Moto",
                table: "Brand",
                keyColumn: "BrandId",
                keyValue: 71L);

            migrationBuilder.DeleteData(
                schema: "Moto",
                table: "Brand",
                keyColumn: "BrandId",
                keyValue: 72L);

            migrationBuilder.DeleteData(
                schema: "Moto",
                table: "Brand",
                keyColumn: "BrandId",
                keyValue: 73L);

            migrationBuilder.DeleteData(
                schema: "Moto",
                table: "Brand",
                keyColumn: "BrandId",
                keyValue: 74L);

            migrationBuilder.DeleteData(
                schema: "Moto",
                table: "Brand",
                keyColumn: "BrandId",
                keyValue: 75L);

            migrationBuilder.DeleteData(
                schema: "Moto",
                table: "Brand",
                keyColumn: "BrandId",
                keyValue: 76L);

            migrationBuilder.DeleteData(
                schema: "Moto",
                table: "Brand",
                keyColumn: "BrandId",
                keyValue: 77L);

            migrationBuilder.DeleteData(
                schema: "Moto",
                table: "Brand",
                keyColumn: "BrandId",
                keyValue: 78L);

            migrationBuilder.DeleteData(
                schema: "Moto",
                table: "Brand",
                keyColumn: "BrandId",
                keyValue: 79L);

            migrationBuilder.DeleteData(
                schema: "Moto",
                table: "Brand",
                keyColumn: "BrandId",
                keyValue: 80L);

            migrationBuilder.DeleteData(
                schema: "Moto",
                table: "Brand",
                keyColumn: "BrandId",
                keyValue: 81L);

            migrationBuilder.DeleteData(
                schema: "Moto",
                table: "Brand",
                keyColumn: "BrandId",
                keyValue: 82L);

            migrationBuilder.DeleteData(
                schema: "Moto",
                table: "Brand",
                keyColumn: "BrandId",
                keyValue: 83L);

            migrationBuilder.DeleteData(
                schema: "Moto",
                table: "Brand",
                keyColumn: "BrandId",
                keyValue: 84L);

            migrationBuilder.DeleteData(
                schema: "Moto",
                table: "Brand",
                keyColumn: "BrandId",
                keyValue: 85L);

            migrationBuilder.DeleteData(
                schema: "Moto",
                table: "Brand",
                keyColumn: "BrandId",
                keyValue: 86L);

            migrationBuilder.DeleteData(
                schema: "Moto",
                table: "Brand",
                keyColumn: "BrandId",
                keyValue: 87L);

            migrationBuilder.DeleteData(
                schema: "Moto",
                table: "Brand",
                keyColumn: "BrandId",
                keyValue: 88L);

            migrationBuilder.DeleteData(
                schema: "Moto",
                table: "Brand",
                keyColumn: "BrandId",
                keyValue: 89L);

            migrationBuilder.DeleteData(
                schema: "Moto",
                table: "Brand",
                keyColumn: "BrandId",
                keyValue: 90L);

            migrationBuilder.DeleteData(
                schema: "Moto",
                table: "Brand",
                keyColumn: "BrandId",
                keyValue: 91L);

            migrationBuilder.DeleteData(
                schema: "Moto",
                table: "Brand",
                keyColumn: "BrandId",
                keyValue: 92L);

            migrationBuilder.DeleteData(
                schema: "Moto",
                table: "Brand",
                keyColumn: "BrandId",
                keyValue: 93L);

            migrationBuilder.DeleteData(
                schema: "Moto",
                table: "Brand",
                keyColumn: "BrandId",
                keyValue: 94L);

            migrationBuilder.DeleteData(
                schema: "Moto",
                table: "Brand",
                keyColumn: "BrandId",
                keyValue: 95L);

            migrationBuilder.DeleteData(
                schema: "Moto",
                table: "Brand",
                keyColumn: "BrandId",
                keyValue: 96L);

            migrationBuilder.DeleteData(
                schema: "Moto",
                table: "Brand",
                keyColumn: "BrandId",
                keyValue: 97L);

            migrationBuilder.DeleteData(
                schema: "Moto",
                table: "Brand",
                keyColumn: "BrandId",
                keyValue: 98L);

            migrationBuilder.DeleteData(
                schema: "Moto",
                table: "Brand",
                keyColumn: "BrandId",
                keyValue: 99L);

            migrationBuilder.DeleteData(
                schema: "Moto",
                table: "Brand",
                keyColumn: "BrandId",
                keyValue: 100L);

            migrationBuilder.DeleteData(
                schema: "Moto",
                table: "Brand",
                keyColumn: "BrandId",
                keyValue: 101L);

            migrationBuilder.DeleteData(
                schema: "Moto",
                table: "Brand",
                keyColumn: "BrandId",
                keyValue: 102L);

            migrationBuilder.DeleteData(
                schema: "Moto",
                table: "Brand",
                keyColumn: "BrandId",
                keyValue: 103L);

            migrationBuilder.DeleteData(
                schema: "Moto",
                table: "Brand",
                keyColumn: "BrandId",
                keyValue: 104L);

            migrationBuilder.DeleteData(
                schema: "Moto",
                table: "Brand",
                keyColumn: "BrandId",
                keyValue: 105L);

            migrationBuilder.DeleteData(
                schema: "Moto",
                table: "Brand",
                keyColumn: "BrandId",
                keyValue: 106L);

            migrationBuilder.DeleteData(
                schema: "Moto",
                table: "Brand",
                keyColumn: "BrandId",
                keyValue: 107L);

            migrationBuilder.DeleteData(
                schema: "Moto",
                table: "Brand",
                keyColumn: "BrandId",
                keyValue: 108L);

            migrationBuilder.DeleteData(
                schema: "Moto",
                table: "Brand",
                keyColumn: "BrandId",
                keyValue: 109L);

            migrationBuilder.DeleteData(
                schema: "Moto",
                table: "Brand",
                keyColumn: "BrandId",
                keyValue: 110L);

            migrationBuilder.DeleteData(
                schema: "Moto",
                table: "Brand",
                keyColumn: "BrandId",
                keyValue: 111L);

            migrationBuilder.DeleteData(
                schema: "Moto",
                table: "Brand",
                keyColumn: "BrandId",
                keyValue: 112L);

            migrationBuilder.DeleteData(
                schema: "Moto",
                table: "Brand",
                keyColumn: "BrandId",
                keyValue: 113L);

            migrationBuilder.DeleteData(
                schema: "Moto",
                table: "Brand",
                keyColumn: "BrandId",
                keyValue: 114L);

            migrationBuilder.DeleteData(
                schema: "Moto",
                table: "Brand",
                keyColumn: "BrandId",
                keyValue: 115L);

            migrationBuilder.DeleteData(
                schema: "Moto",
                table: "Brand",
                keyColumn: "BrandId",
                keyValue: 116L);

            migrationBuilder.DeleteData(
                schema: "Moto",
                table: "Brand",
                keyColumn: "BrandId",
                keyValue: 117L);

            migrationBuilder.DeleteData(
                schema: "Moto",
                table: "Brand",
                keyColumn: "BrandId",
                keyValue: 118L);

            migrationBuilder.DeleteData(
                schema: "Moto",
                table: "Brand",
                keyColumn: "BrandId",
                keyValue: 119L);

            migrationBuilder.DeleteData(
                schema: "Moto",
                table: "Brand",
                keyColumn: "BrandId",
                keyValue: 120L);

            migrationBuilder.DeleteData(
                schema: "Moto",
                table: "Brand",
                keyColumn: "BrandId",
                keyValue: 121L);

            migrationBuilder.DeleteData(
                schema: "Moto",
                table: "Brand",
                keyColumn: "BrandId",
                keyValue: 122L);

            migrationBuilder.DeleteData(
                schema: "Moto",
                table: "Brand",
                keyColumn: "BrandId",
                keyValue: 123L);

            migrationBuilder.DeleteData(
                schema: "Moto",
                table: "Brand",
                keyColumn: "BrandId",
                keyValue: 124L);

            migrationBuilder.DeleteData(
                schema: "Moto",
                table: "Brand",
                keyColumn: "BrandId",
                keyValue: 125L);

            migrationBuilder.DeleteData(
                schema: "Moto",
                table: "Brand",
                keyColumn: "BrandId",
                keyValue: 126L);

            migrationBuilder.DeleteData(
                schema: "Moto",
                table: "Brand",
                keyColumn: "BrandId",
                keyValue: 127L);

            migrationBuilder.DeleteData(
                schema: "Moto",
                table: "Brand",
                keyColumn: "BrandId",
                keyValue: 128L);

            migrationBuilder.DeleteData(
                schema: "Moto",
                table: "Brand",
                keyColumn: "BrandId",
                keyValue: 129L);

            migrationBuilder.DeleteData(
                schema: "Moto",
                table: "Brand",
                keyColumn: "BrandId",
                keyValue: 130L);

            migrationBuilder.DeleteData(
                schema: "Moto",
                table: "Brand",
                keyColumn: "BrandId",
                keyValue: 131L);

            migrationBuilder.DeleteData(
                schema: "Moto",
                table: "Brand",
                keyColumn: "BrandId",
                keyValue: 132L);

            migrationBuilder.DeleteData(
                schema: "Moto",
                table: "Brand",
                keyColumn: "BrandId",
                keyValue: 133L);

            migrationBuilder.DeleteData(
                schema: "Moto",
                table: "Brand",
                keyColumn: "BrandId",
                keyValue: 134L);

            migrationBuilder.DeleteData(
                schema: "Moto",
                table: "Brand",
                keyColumn: "BrandId",
                keyValue: 135L);

            migrationBuilder.DeleteData(
                schema: "Moto",
                table: "Brand",
                keyColumn: "BrandId",
                keyValue: 136L);

            migrationBuilder.DeleteData(
                schema: "Moto",
                table: "Brand",
                keyColumn: "BrandId",
                keyValue: 137L);

            migrationBuilder.DeleteData(
                schema: "Moto",
                table: "Brand",
                keyColumn: "BrandId",
                keyValue: 138L);

            migrationBuilder.DeleteData(
                schema: "Moto",
                table: "Brand",
                keyColumn: "BrandId",
                keyValue: 139L);

            migrationBuilder.DeleteData(
                schema: "Moto",
                table: "Brand",
                keyColumn: "BrandId",
                keyValue: 140L);

            migrationBuilder.DeleteData(
                schema: "Moto",
                table: "Brand",
                keyColumn: "BrandId",
                keyValue: 141L);

            migrationBuilder.DeleteData(
                schema: "Moto",
                table: "Brand",
                keyColumn: "BrandId",
                keyValue: 142L);

            migrationBuilder.DeleteData(
                schema: "Moto",
                table: "Brand",
                keyColumn: "BrandId",
                keyValue: 143L);

            migrationBuilder.DeleteData(
                schema: "Moto",
                table: "Brand",
                keyColumn: "BrandId",
                keyValue: 144L);

            migrationBuilder.DeleteData(
                schema: "Moto",
                table: "Brand",
                keyColumn: "BrandId",
                keyValue: 145L);

            migrationBuilder.DeleteData(
                schema: "Moto",
                table: "Brand",
                keyColumn: "BrandId",
                keyValue: 146L);

            migrationBuilder.DeleteData(
                schema: "Moto",
                table: "Brand",
                keyColumn: "BrandId",
                keyValue: 147L);

            migrationBuilder.DeleteData(
                schema: "Moto",
                table: "Brand",
                keyColumn: "BrandId",
                keyValue: 148L);

            migrationBuilder.DeleteData(
                schema: "Moto",
                table: "Brand",
                keyColumn: "BrandId",
                keyValue: 149L);

            migrationBuilder.DeleteData(
                schema: "Moto",
                table: "Brand",
                keyColumn: "BrandId",
                keyValue: 150L);

            migrationBuilder.DeleteData(
                schema: "Moto",
                table: "Brand",
                keyColumn: "BrandId",
                keyValue: 151L);

            migrationBuilder.DeleteData(
                schema: "Moto",
                table: "Brand",
                keyColumn: "BrandId",
                keyValue: 152L);

            migrationBuilder.DeleteData(
                schema: "Moto",
                table: "Brand",
                keyColumn: "BrandId",
                keyValue: 153L);

            migrationBuilder.DeleteData(
                schema: "Moto",
                table: "Brand",
                keyColumn: "BrandId",
                keyValue: 154L);

            migrationBuilder.DeleteData(
                schema: "Moto",
                table: "Brand",
                keyColumn: "BrandId",
                keyValue: 155L);

            migrationBuilder.DeleteData(
                schema: "Moto",
                table: "Brand",
                keyColumn: "BrandId",
                keyValue: 156L);

            migrationBuilder.DeleteData(
                schema: "Moto",
                table: "Brand",
                keyColumn: "BrandId",
                keyValue: 157L);

            migrationBuilder.DeleteData(
                schema: "Moto",
                table: "Brand",
                keyColumn: "BrandId",
                keyValue: 158L);

            migrationBuilder.DeleteData(
                schema: "Moto",
                table: "Brand",
                keyColumn: "BrandId",
                keyValue: 159L);

            migrationBuilder.DeleteData(
                schema: "Moto",
                table: "Brand",
                keyColumn: "BrandId",
                keyValue: 160L);

            migrationBuilder.DeleteData(
                schema: "Moto",
                table: "Brand",
                keyColumn: "BrandId",
                keyValue: 161L);

            migrationBuilder.DeleteData(
                schema: "Moto",
                table: "Brand",
                keyColumn: "BrandId",
                keyValue: 162L);

            migrationBuilder.DeleteData(
                schema: "Moto",
                table: "Brand",
                keyColumn: "BrandId",
                keyValue: 163L);

            migrationBuilder.DeleteData(
                schema: "Moto",
                table: "Brand",
                keyColumn: "BrandId",
                keyValue: 164L);

            migrationBuilder.DeleteData(
                schema: "Moto",
                table: "Brand",
                keyColumn: "BrandId",
                keyValue: 165L);

            migrationBuilder.DeleteData(
                schema: "Moto",
                table: "Brand",
                keyColumn: "BrandId",
                keyValue: 166L);

            migrationBuilder.DeleteData(
                schema: "Moto",
                table: "Brand",
                keyColumn: "BrandId",
                keyValue: 167L);

            migrationBuilder.DeleteData(
                schema: "Moto",
                table: "Brand",
                keyColumn: "BrandId",
                keyValue: 168L);

            migrationBuilder.DeleteData(
                schema: "Moto",
                table: "Brand",
                keyColumn: "BrandId",
                keyValue: 169L);

            migrationBuilder.DeleteData(
                schema: "Moto",
                table: "Brand",
                keyColumn: "BrandId",
                keyValue: 170L);

            migrationBuilder.DeleteData(
                schema: "Moto",
                table: "Brand",
                keyColumn: "BrandId",
                keyValue: 171L);

            migrationBuilder.DeleteData(
                schema: "Moto",
                table: "Brand",
                keyColumn: "BrandId",
                keyValue: 172L);

            migrationBuilder.DeleteData(
                schema: "Moto",
                table: "Brand",
                keyColumn: "BrandId",
                keyValue: 173L);

            migrationBuilder.DeleteData(
                schema: "Moto",
                table: "Brand",
                keyColumn: "BrandId",
                keyValue: 174L);

            migrationBuilder.DeleteData(
                schema: "Moto",
                table: "Brand",
                keyColumn: "BrandId",
                keyValue: 175L);

            migrationBuilder.DeleteData(
                schema: "Moto",
                table: "Brand",
                keyColumn: "BrandId",
                keyValue: 176L);

            migrationBuilder.DeleteData(
                schema: "Moto",
                table: "Brand",
                keyColumn: "BrandId",
                keyValue: 177L);

            migrationBuilder.DeleteData(
                schema: "Moto",
                table: "Brand",
                keyColumn: "BrandId",
                keyValue: 178L);

            migrationBuilder.DeleteData(
                schema: "Moto",
                table: "Brand",
                keyColumn: "BrandId",
                keyValue: 179L);

            migrationBuilder.DeleteData(
                schema: "Moto",
                table: "Brand",
                keyColumn: "BrandId",
                keyValue: 180L);

            migrationBuilder.DeleteData(
                schema: "Moto",
                table: "Brand",
                keyColumn: "BrandId",
                keyValue: 181L);

            migrationBuilder.DeleteData(
                schema: "Moto",
                table: "Brand",
                keyColumn: "BrandId",
                keyValue: 182L);

            migrationBuilder.DeleteData(
                schema: "Moto",
                table: "Brand",
                keyColumn: "BrandId",
                keyValue: 183L);

            migrationBuilder.DeleteData(
                schema: "Moto",
                table: "Brand",
                keyColumn: "BrandId",
                keyValue: 184L);

            migrationBuilder.DeleteData(
                schema: "Moto",
                table: "Brand",
                keyColumn: "BrandId",
                keyValue: 185L);

            migrationBuilder.DeleteData(
                schema: "Moto",
                table: "Brand",
                keyColumn: "BrandId",
                keyValue: 186L);

            migrationBuilder.DeleteData(
                schema: "Moto",
                table: "Brand",
                keyColumn: "BrandId",
                keyValue: 187L);

            migrationBuilder.DeleteData(
                schema: "Moto",
                table: "Brand",
                keyColumn: "BrandId",
                keyValue: 188L);

            migrationBuilder.DeleteData(
                schema: "Moto",
                table: "Brand",
                keyColumn: "BrandId",
                keyValue: 189L);

            migrationBuilder.DeleteData(
                schema: "Moto",
                table: "Brand",
                keyColumn: "BrandId",
                keyValue: 190L);

            migrationBuilder.DeleteData(
                schema: "Moto",
                table: "Brand",
                keyColumn: "BrandId",
                keyValue: 191L);

            migrationBuilder.DeleteData(
                schema: "Moto",
                table: "Brand",
                keyColumn: "BrandId",
                keyValue: 192L);

            migrationBuilder.DeleteData(
                schema: "Moto",
                table: "Brand",
                keyColumn: "BrandId",
                keyValue: 193L);

            migrationBuilder.DeleteData(
                schema: "Moto",
                table: "Brand",
                keyColumn: "BrandId",
                keyValue: 194L);

            migrationBuilder.DeleteData(
                schema: "Moto",
                table: "Brand",
                keyColumn: "BrandId",
                keyValue: 195L);

            migrationBuilder.DeleteData(
                schema: "Moto",
                table: "Brand",
                keyColumn: "BrandId",
                keyValue: 196L);

            migrationBuilder.DeleteData(
                schema: "Moto",
                table: "Brand",
                keyColumn: "BrandId",
                keyValue: 197L);

            migrationBuilder.DeleteData(
                schema: "Moto",
                table: "Brand",
                keyColumn: "BrandId",
                keyValue: 198L);

            migrationBuilder.DeleteData(
                schema: "Moto",
                table: "Brand",
                keyColumn: "BrandId",
                keyValue: 199L);

            migrationBuilder.DeleteData(
                schema: "Moto",
                table: "Brand",
                keyColumn: "BrandId",
                keyValue: 200L);

            migrationBuilder.DeleteData(
                schema: "Moto",
                table: "Brand",
                keyColumn: "BrandId",
                keyValue: 201L);

            migrationBuilder.DeleteData(
                schema: "Moto",
                table: "Brand",
                keyColumn: "BrandId",
                keyValue: 202L);

            migrationBuilder.DeleteData(
                schema: "Moto",
                table: "Brand",
                keyColumn: "BrandId",
                keyValue: 203L);

            migrationBuilder.DeleteData(
                schema: "Moto",
                table: "Brand",
                keyColumn: "BrandId",
                keyValue: 204L);

            migrationBuilder.DeleteData(
                schema: "Moto",
                table: "Brand",
                keyColumn: "BrandId",
                keyValue: 205L);

            migrationBuilder.DeleteData(
                schema: "Moto",
                table: "Brand",
                keyColumn: "BrandId",
                keyValue: 206L);

            migrationBuilder.DeleteData(
                schema: "Moto",
                table: "Brand",
                keyColumn: "BrandId",
                keyValue: 207L);

            migrationBuilder.DeleteData(
                schema: "Moto",
                table: "Brand",
                keyColumn: "BrandId",
                keyValue: 208L);

            migrationBuilder.DeleteData(
                schema: "Moto",
                table: "Brand",
                keyColumn: "BrandId",
                keyValue: 209L);

            migrationBuilder.DeleteData(
                schema: "Moto",
                table: "Brand",
                keyColumn: "BrandId",
                keyValue: 210L);

            migrationBuilder.DeleteData(
                schema: "Moto",
                table: "Brand",
                keyColumn: "BrandId",
                keyValue: 211L);

            migrationBuilder.DeleteData(
                schema: "Moto",
                table: "Brand",
                keyColumn: "BrandId",
                keyValue: 212L);

            migrationBuilder.DeleteData(
                schema: "Moto",
                table: "Brand",
                keyColumn: "BrandId",
                keyValue: 213L);

            migrationBuilder.DeleteData(
                schema: "Moto",
                table: "Brand",
                keyColumn: "BrandId",
                keyValue: 214L);

            migrationBuilder.DeleteData(
                schema: "Moto",
                table: "Brand",
                keyColumn: "BrandId",
                keyValue: 215L);

            migrationBuilder.DeleteData(
                schema: "Moto",
                table: "Brand",
                keyColumn: "BrandId",
                keyValue: 216L);

            migrationBuilder.DeleteData(
                schema: "Moto",
                table: "Brand",
                keyColumn: "BrandId",
                keyValue: 217L);

            migrationBuilder.DeleteData(
                schema: "Moto",
                table: "Brand",
                keyColumn: "BrandId",
                keyValue: 218L);

            migrationBuilder.DeleteData(
                schema: "Moto",
                table: "Brand",
                keyColumn: "BrandId",
                keyValue: 219L);

            migrationBuilder.DeleteData(
                schema: "Moto",
                table: "Brand",
                keyColumn: "BrandId",
                keyValue: 220L);

            migrationBuilder.DeleteData(
                schema: "Moto",
                table: "Brand",
                keyColumn: "BrandId",
                keyValue: 221L);

            migrationBuilder.DeleteData(
                schema: "Moto",
                table: "Brand",
                keyColumn: "BrandId",
                keyValue: 222L);

            migrationBuilder.DeleteData(
                schema: "Moto",
                table: "Brand",
                keyColumn: "BrandId",
                keyValue: 223L);

            migrationBuilder.DeleteData(
                schema: "Moto",
                table: "Brand",
                keyColumn: "BrandId",
                keyValue: 224L);

            migrationBuilder.DeleteData(
                schema: "Moto",
                table: "Brand",
                keyColumn: "BrandId",
                keyValue: 225L);

            migrationBuilder.DeleteData(
                schema: "Moto",
                table: "Brand",
                keyColumn: "BrandId",
                keyValue: 226L);

            migrationBuilder.DeleteData(
                schema: "Moto",
                table: "Brand",
                keyColumn: "BrandId",
                keyValue: 227L);

            migrationBuilder.DeleteData(
                schema: "Moto",
                table: "Brand",
                keyColumn: "BrandId",
                keyValue: 228L);

            migrationBuilder.DeleteData(
                schema: "Moto",
                table: "Brand",
                keyColumn: "BrandId",
                keyValue: 229L);

            migrationBuilder.DeleteData(
                schema: "Moto",
                table: "Brand",
                keyColumn: "BrandId",
                keyValue: 230L);

            migrationBuilder.DeleteData(
                schema: "Moto",
                table: "Brand",
                keyColumn: "BrandId",
                keyValue: 231L);

            migrationBuilder.DeleteData(
                schema: "Moto",
                table: "Brand",
                keyColumn: "BrandId",
                keyValue: 232L);

            migrationBuilder.DeleteData(
                schema: "Moto",
                table: "Brand",
                keyColumn: "BrandId",
                keyValue: 233L);

            migrationBuilder.DeleteData(
                schema: "Moto",
                table: "Brand",
                keyColumn: "BrandId",
                keyValue: 234L);

            migrationBuilder.DeleteData(
                schema: "Moto",
                table: "Brand",
                keyColumn: "BrandId",
                keyValue: 235L);

            migrationBuilder.DeleteData(
                schema: "Moto",
                table: "Brand",
                keyColumn: "BrandId",
                keyValue: 236L);

            migrationBuilder.DeleteData(
                schema: "Moto",
                table: "Brand",
                keyColumn: "BrandId",
                keyValue: 237L);

            migrationBuilder.DeleteData(
                schema: "Moto",
                table: "Brand",
                keyColumn: "BrandId",
                keyValue: 238L);

            migrationBuilder.DeleteData(
                schema: "Moto",
                table: "Brand",
                keyColumn: "BrandId",
                keyValue: 239L);

            migrationBuilder.DeleteData(
                schema: "Moto",
                table: "Brand",
                keyColumn: "BrandId",
                keyValue: 240L);

            migrationBuilder.DeleteData(
                schema: "Moto",
                table: "Brand",
                keyColumn: "BrandId",
                keyValue: 241L);

            migrationBuilder.DeleteData(
                schema: "Moto",
                table: "Brand",
                keyColumn: "BrandId",
                keyValue: 242L);

            migrationBuilder.DeleteData(
                schema: "Moto",
                table: "Brand",
                keyColumn: "BrandId",
                keyValue: 243L);

            migrationBuilder.DeleteData(
                schema: "Moto",
                table: "Brand",
                keyColumn: "BrandId",
                keyValue: 244L);

            migrationBuilder.DeleteData(
                schema: "Moto",
                table: "Brand",
                keyColumn: "BrandId",
                keyValue: 245L);

            migrationBuilder.DeleteData(
                schema: "Moto",
                table: "Brand",
                keyColumn: "BrandId",
                keyValue: 246L);

            migrationBuilder.DeleteData(
                schema: "Moto",
                table: "Brand",
                keyColumn: "BrandId",
                keyValue: 247L);

            migrationBuilder.DeleteData(
                schema: "Moto",
                table: "Brand",
                keyColumn: "BrandId",
                keyValue: 248L);

            migrationBuilder.DeleteData(
                schema: "Moto",
                table: "Brand",
                keyColumn: "BrandId",
                keyValue: 249L);

            migrationBuilder.DeleteData(
                schema: "Moto",
                table: "Brand",
                keyColumn: "BrandId",
                keyValue: 250L);

            migrationBuilder.DeleteData(
                schema: "Moto",
                table: "Brand",
                keyColumn: "BrandId",
                keyValue: 251L);

            migrationBuilder.DeleteData(
                schema: "Moto",
                table: "Brand",
                keyColumn: "BrandId",
                keyValue: 252L);

            migrationBuilder.DeleteData(
                schema: "Moto",
                table: "Brand",
                keyColumn: "BrandId",
                keyValue: 253L);

            migrationBuilder.DeleteData(
                schema: "Moto",
                table: "Brand",
                keyColumn: "BrandId",
                keyValue: 254L);

            migrationBuilder.DeleteData(
                schema: "Moto",
                table: "Brand",
                keyColumn: "BrandId",
                keyValue: 255L);

            migrationBuilder.DeleteData(
                schema: "Moto",
                table: "Brand",
                keyColumn: "BrandId",
                keyValue: 256L);

            migrationBuilder.DeleteData(
                schema: "Moto",
                table: "Brand",
                keyColumn: "BrandId",
                keyValue: 257L);

            migrationBuilder.DeleteData(
                schema: "Moto",
                table: "Brand",
                keyColumn: "BrandId",
                keyValue: 258L);

            migrationBuilder.DeleteData(
                schema: "Moto",
                table: "Brand",
                keyColumn: "BrandId",
                keyValue: 259L);

            migrationBuilder.DeleteData(
                schema: "Moto",
                table: "Brand",
                keyColumn: "BrandId",
                keyValue: 260L);

            migrationBuilder.DeleteData(
                schema: "Moto",
                table: "Brand",
                keyColumn: "BrandId",
                keyValue: 261L);

            migrationBuilder.DeleteData(
                schema: "Moto",
                table: "Brand",
                keyColumn: "BrandId",
                keyValue: 262L);

            migrationBuilder.DeleteData(
                schema: "Moto",
                table: "Brand",
                keyColumn: "BrandId",
                keyValue: 263L);

            migrationBuilder.DeleteData(
                schema: "Moto",
                table: "Brand",
                keyColumn: "BrandId",
                keyValue: 264L);

            migrationBuilder.DeleteData(
                schema: "Moto",
                table: "Brand",
                keyColumn: "BrandId",
                keyValue: 265L);

            migrationBuilder.DeleteData(
                schema: "Moto",
                table: "Brand",
                keyColumn: "BrandId",
                keyValue: 266L);

            migrationBuilder.DeleteData(
                schema: "Moto",
                table: "Brand",
                keyColumn: "BrandId",
                keyValue: 267L);

            migrationBuilder.DeleteData(
                schema: "Moto",
                table: "Brand",
                keyColumn: "BrandId",
                keyValue: 268L);

            migrationBuilder.DeleteData(
                schema: "Moto",
                table: "Brand",
                keyColumn: "BrandId",
                keyValue: 269L);

            migrationBuilder.DeleteData(
                schema: "Moto",
                table: "Brand",
                keyColumn: "BrandId",
                keyValue: 270L);

            migrationBuilder.DeleteData(
                schema: "Moto",
                table: "Brand",
                keyColumn: "BrandId",
                keyValue: 271L);
        }
    }
}
