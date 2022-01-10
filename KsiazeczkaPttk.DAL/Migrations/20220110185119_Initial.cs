using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace KsiazeczkaPttk.DAL.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "GotPttk",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nazwa = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Poziom = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GotPttk", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "GrupyGorskie",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nazwa = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GrupyGorskie", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "RoleUzytkownikow",
                columns: table => new
                {
                    Nazwa = table.Column<string>(type: "varchar(40)", maxLength: 40, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleUzytkownikow", x => x.Nazwa);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "PasmaGorskie",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nazwa = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Grupa = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PasmaGorskie", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PasmaGorskie_GrupyGorskie_Grupa",
                        column: x => x.Grupa,
                        principalTable: "GrupyGorskie",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Uzytkownicy",
                columns: table => new
                {
                    Login = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Haslo = table.Column<string>(type: "varchar(160)", maxLength: 160, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Email = table.Column<string>(type: "varchar(160)", maxLength: 160, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Imie = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Nazwisko = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Rola = table.Column<string>(type: "varchar(40)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Uzytkownicy", x => x.Login);
                    table.ForeignKey(
                        name: "FK_Uzytkownicy_RoleUzytkownikow_Rola",
                        column: x => x.Rola,
                        principalTable: "RoleUzytkownikow",
                        principalColumn: "Nazwa",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Ksiazeczki",
                columns: table => new
                {
                    Wlasciciel = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Niepelnosprawnosc = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Punkty = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ksiazeczki", x => x.Wlasciciel);
                    table.ForeignKey(
                        name: "FK_Ksiazeczki_Uzytkownicy_Wlasciciel",
                        column: x => x.Wlasciciel,
                        principalTable: "Uzytkownicy",
                        principalColumn: "Login",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "PosiadaneGotPttk",
                columns: table => new
                {
                    Wlasciciel = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Odznaka = table.Column<int>(type: "int", nullable: false),
                    DataRozpoczeciaZdobywania = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    DataZakonczeniaZdobywania = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    DataPrzyznania = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PosiadaneGotPttk", x => new { x.Wlasciciel, x.Odznaka });
                    table.ForeignKey(
                        name: "FK_PosiadaneGotPttk_GotPttk_Odznaka",
                        column: x => x.Odznaka,
                        principalTable: "GotPttk",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PosiadaneGotPttk_Ksiazeczki_Wlasciciel",
                        column: x => x.Wlasciciel,
                        principalTable: "Ksiazeczki",
                        principalColumn: "Wlasciciel",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "PunktyTerenowe",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nazwa = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Lat = table.Column<double>(type: "double", nullable: false),
                    Lng = table.Column<double>(type: "double", nullable: false),
                    Mnpm = table.Column<double>(type: "double", nullable: false),
                    Wlasciciel = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PunktyTerenowe", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PunktyTerenowe_Ksiazeczki_Wlasciciel",
                        column: x => x.Wlasciciel,
                        principalTable: "Ksiazeczki",
                        principalColumn: "Wlasciciel",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Wycieczki",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nazwa = table.Column<string>(type: "varchar(250)", maxLength: 250, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Wlasciciel = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wycieczki", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Wycieczki_Ksiazeczki_Wlasciciel",
                        column: x => x.Wlasciciel,
                        principalTable: "Ksiazeczki",
                        principalColumn: "Wlasciciel",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Odcinki",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Wersja = table.Column<int>(type: "int", nullable: false),
                    Nazwa = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Punkty = table.Column<int>(type: "int", nullable: false),
                    PunktyPowrot = table.Column<int>(type: "int", nullable: false),
                    Od = table.Column<int>(type: "int", nullable: false),
                    Do = table.Column<int>(type: "int", nullable: false),
                    Pasmo = table.Column<int>(type: "int", nullable: false),
                    Wlasciciel = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Odcinki", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Odcinki_Ksiazeczki_Wlasciciel",
                        column: x => x.Wlasciciel,
                        principalTable: "Ksiazeczki",
                        principalColumn: "Wlasciciel",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Odcinki_PasmaGorskie_Pasmo",
                        column: x => x.Pasmo,
                        principalTable: "PasmaGorskie",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Odcinki_PunktyTerenowe_Do",
                        column: x => x.Do,
                        principalTable: "PunktyTerenowe",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Odcinki_PunktyTerenowe_Od",
                        column: x => x.Od,
                        principalTable: "PunktyTerenowe",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "PotwierdzeniaTerenowe",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Typ = table.Column<int>(type: "int", nullable: false),
                    Url = table.Column<string>(type: "varchar(250)", maxLength: 250, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Punkt = table.Column<int>(type: "int", nullable: false),
                    Data = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Administracyjny = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PotwierdzeniaTerenowe", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PotwierdzeniaTerenowe_PunktyTerenowe_Punkt",
                        column: x => x.Punkt,
                        principalTable: "PunktyTerenowe",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Weryfikacje",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Wycieczka = table.Column<int>(type: "int", nullable: false),
                    Przodownik = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Data = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Zaakceptiowana = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    PowodOdrzucenia = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Weryfikacje", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Weryfikacje_Uzytkownicy_Przodownik",
                        column: x => x.Przodownik,
                        principalTable: "Uzytkownicy",
                        principalColumn: "Login",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Weryfikacje_Wycieczki_Wycieczka",
                        column: x => x.Wycieczka,
                        principalTable: "Wycieczki",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "PrzebyteOdcinki",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Kolejnosc = table.Column<int>(type: "int", nullable: false),
                    Wycieczka = table.Column<int>(type: "int", nullable: false),
                    OdcinekId = table.Column<int>(type: "int", nullable: false),
                    Powrot = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrzebyteOdcinki", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PrzebyteOdcinki_Odcinki_OdcinekId",
                        column: x => x.OdcinekId,
                        principalTable: "Odcinki",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PrzebyteOdcinki_Wycieczki_Wycieczka",
                        column: x => x.Wycieczka,
                        principalTable: "Wycieczki",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ZamknieciaOdcinkow",
                columns: table => new
                {
                    OdcinekId = table.Column<int>(type: "int", nullable: false),
                    DataZamkniecia = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    DataOtwarcia = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    Przyczyna = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ZamknieciaOdcinkow", x => new { x.OdcinekId, x.DataZamkniecia });
                    table.ForeignKey(
                        name: "FK_ZamknieciaOdcinkow_Odcinki_OdcinekId",
                        column: x => x.OdcinekId,
                        principalTable: "Odcinki",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "PotwierdzeniaTerenowePrzebytychOdcinkow",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Potwierdzenie = table.Column<int>(type: "int", nullable: false),
                    PrzebytyOdcinekId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PotwierdzeniaTerenowePrzebytychOdcinkow", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PotwierdzeniaTerenowePrzebytychOdcinkow_PotwierdzeniaTerenow~",
                        column: x => x.Potwierdzenie,
                        principalTable: "PotwierdzeniaTerenowe",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PotwierdzeniaTerenowePrzebytychOdcinkow_PrzebyteOdcinki_Prze~",
                        column: x => x.PrzebytyOdcinekId,
                        principalTable: "PrzebyteOdcinki",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_GotPttk_Poziom",
                table: "GotPttk",
                column: "Poziom",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_GrupyGorskie_Nazwa",
                table: "GrupyGorskie",
                column: "Nazwa",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Odcinki_Do",
                table: "Odcinki",
                column: "Do");

            migrationBuilder.CreateIndex(
                name: "IX_Odcinki_Od",
                table: "Odcinki",
                column: "Od");

            migrationBuilder.CreateIndex(
                name: "IX_Odcinki_Pasmo",
                table: "Odcinki",
                column: "Pasmo");

            migrationBuilder.CreateIndex(
                name: "IX_Odcinki_Wlasciciel",
                table: "Odcinki",
                column: "Wlasciciel");

            migrationBuilder.CreateIndex(
                name: "IX_PasmaGorskie_Grupa",
                table: "PasmaGorskie",
                column: "Grupa");

            migrationBuilder.CreateIndex(
                name: "IX_PasmaGorskie_Nazwa",
                table: "PasmaGorskie",
                column: "Nazwa",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PosiadaneGotPttk_Odznaka",
                table: "PosiadaneGotPttk",
                column: "Odznaka");

            migrationBuilder.CreateIndex(
                name: "IX_PotwierdzeniaTerenowe_Punkt",
                table: "PotwierdzeniaTerenowe",
                column: "Punkt");

            migrationBuilder.CreateIndex(
                name: "IX_PotwierdzeniaTerenowePrzebytychOdcinkow_Potwierdzenie",
                table: "PotwierdzeniaTerenowePrzebytychOdcinkow",
                column: "Potwierdzenie");

            migrationBuilder.CreateIndex(
                name: "IX_PotwierdzeniaTerenowePrzebytychOdcinkow_PrzebytyOdcinekId",
                table: "PotwierdzeniaTerenowePrzebytychOdcinkow",
                column: "PrzebytyOdcinekId");

            migrationBuilder.CreateIndex(
                name: "IX_PrzebyteOdcinki_OdcinekId",
                table: "PrzebyteOdcinki",
                column: "OdcinekId");

            migrationBuilder.CreateIndex(
                name: "IX_PrzebyteOdcinki_Wycieczka",
                table: "PrzebyteOdcinki",
                column: "Wycieczka");

            migrationBuilder.CreateIndex(
                name: "IX_PunktyTerenowe_Nazwa",
                table: "PunktyTerenowe",
                column: "Nazwa",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PunktyTerenowe_Wlasciciel",
                table: "PunktyTerenowe",
                column: "Wlasciciel");

            migrationBuilder.CreateIndex(
                name: "IX_Uzytkownicy_Rola",
                table: "Uzytkownicy",
                column: "Rola");

            migrationBuilder.CreateIndex(
                name: "IX_Weryfikacje_Przodownik",
                table: "Weryfikacje",
                column: "Przodownik");

            migrationBuilder.CreateIndex(
                name: "IX_Weryfikacje_Wycieczka",
                table: "Weryfikacje",
                column: "Wycieczka");

            migrationBuilder.CreateIndex(
                name: "IX_Wycieczki_Wlasciciel",
                table: "Wycieczki",
                column: "Wlasciciel");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PosiadaneGotPttk");

            migrationBuilder.DropTable(
                name: "PotwierdzeniaTerenowePrzebytychOdcinkow");

            migrationBuilder.DropTable(
                name: "Weryfikacje");

            migrationBuilder.DropTable(
                name: "ZamknieciaOdcinkow");

            migrationBuilder.DropTable(
                name: "GotPttk");

            migrationBuilder.DropTable(
                name: "PotwierdzeniaTerenowe");

            migrationBuilder.DropTable(
                name: "PrzebyteOdcinki");

            migrationBuilder.DropTable(
                name: "Odcinki");

            migrationBuilder.DropTable(
                name: "Wycieczki");

            migrationBuilder.DropTable(
                name: "PasmaGorskie");

            migrationBuilder.DropTable(
                name: "PunktyTerenowe");

            migrationBuilder.DropTable(
                name: "GrupyGorskie");

            migrationBuilder.DropTable(
                name: "Ksiazeczki");

            migrationBuilder.DropTable(
                name: "Uzytkownicy");

            migrationBuilder.DropTable(
                name: "RoleUzytkownikow");
        }
    }
}
