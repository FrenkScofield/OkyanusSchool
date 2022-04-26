using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OkyanusSchool.Migrations
{
    public partial class OgretmenTableAppended : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OgretmenBilgis",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AD = table.Column<string>(nullable: true),
                    SOYAD = table.Column<string>(nullable: true),
                    TC_KIMLIK = table.Column<string>(nullable: true),
                    DOGUM_TARIHI = table.Column<DateTime>(nullable: false),
                    DOGUM_YERI = table.Column<string>(nullable: true),
                    CEP_TELEFONU_1 = table.Column<string>(nullable: true),
                    CEP_TELEFONU_2 = table.Column<string>(nullable: true),
                    EMAIL = table.Column<string>(nullable: true),
                    KONUM = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OgretmenBilgis", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OgretmenBilgis");
        }
    }
}
