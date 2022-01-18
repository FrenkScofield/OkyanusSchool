using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OkyanusSchool.Migrations
{
    public partial class OgrenviVeliBilgi_appended_to_DB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OgrenciVeliBilgis",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    YASAM_DURUMU = table.Column<bool>(nullable: false),
                    TIPI = table.Column<string>(nullable: true),
                    AD = table.Column<string>(nullable: true),
                    SOYAD = table.Column<string>(nullable: true),
                    TC_KIMLIK = table.Column<string>(nullable: true),
                    ILISKI_DURUMU = table.Column<string>(nullable: true),
                    DOGUM_TARIHI = table.Column<DateTime>(nullable: false),
                    DOGUM_YERI = table.Column<string>(nullable: true),
                    CEP_TELEFONU_1 = table.Column<string>(nullable: true),
                    CEP_TELEFONU_2 = table.Column<string>(nullable: true),
                    IS_TELEFONU = table.Column<string>(nullable: true),
                    EMAIL = table.Column<string>(nullable: true),
                    UYRUK = table.Column<string>(nullable: true),
                    MESLEK = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OgrenciVeliBilgis", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OgrenciVeliBilgis");
        }
    }
}
