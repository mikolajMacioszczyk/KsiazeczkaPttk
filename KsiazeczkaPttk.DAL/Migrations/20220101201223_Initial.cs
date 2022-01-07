using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace KsiazeczkaPttk.DAL.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GotPttk",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nazwa = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Poziom = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GotPttk", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GrupyGorskie",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nazwa = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GrupyGorskie", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RoleUzytkownikow",
                columns: table => new
                {
                    Nazwa = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleUzytkownikow", x => x.Nazwa);
                });

            migrationBuilder.CreateTable(
                name: "StatusyWycieczek",
                columns: table => new
                {
                    Status = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatusyWycieczek", x => x.Status);
                });

            migrationBuilder.CreateTable(
                name: "TypyPotwierdzenTerenowych",
                columns: table => new
                {
                    Typ = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypyPotwierdzenTerenowych", x => x.Typ);
                });

            migrationBuilder.CreateTable(
                name: "PasmaGorskie",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nazwa = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Grupa = table.Column<int>(type: "integer", nullable: false)
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
                });

            migrationBuilder.CreateTable(
                name: "Uzytkownicy",
                columns: table => new
                {
                    Login = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    Haslo = table.Column<string>(type: "character varying(160)", maxLength: 160, nullable: false),
                    Email = table.Column<string>(type: "character varying(160)", maxLength: 160, nullable: false),
                    Imie = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    Nazwisko = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Rola = table.Column<string>(type: "character varying(40)", nullable: false)
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
                });

            migrationBuilder.CreateTable(
                name: "Ksiazeczki",
                columns: table => new
                {
                    Wlasciciel = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    Niepelnosprawnosc = table.Column<bool>(type: "boolean", nullable: false)
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
                });

            migrationBuilder.CreateTable(
                name: "PosiadaneGotPttk",
                columns: table => new
                {
                    Wlasciciel = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    Odznaka = table.Column<int>(type: "integer", nullable: false),
                    DataRozpoczeciaZdobywania = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    DataZakonczeniaZdobywania = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    DataPrzyznania = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
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
                });

            migrationBuilder.CreateTable(
                name: "PunktyTerenowe",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nazwa = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Lat = table.Column<double>(type: "double precision", nullable: false),
                    Lng = table.Column<double>(type: "double precision", nullable: false),
                    Mnpm = table.Column<double>(type: "double precision", nullable: false),
                    Wlasciciel = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: true)
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
                });

            migrationBuilder.CreateTable(
                name: "Wycieczki",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Wlasciciel = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    Status = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
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
                    table.ForeignKey(
                        name: "FK_Wycieczki_StatusyWycieczek_Status",
                        column: x => x.Status,
                        principalTable: "StatusyWycieczek",
                        principalColumn: "Status",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Odcinki",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Wersja = table.Column<int>(type: "integer", nullable: false),
                    Nazwa = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Punkty = table.Column<int>(type: "integer", nullable: false),
                    PunktyPowrot = table.Column<int>(type: "integer", nullable: false),
                    Od = table.Column<int>(type: "integer", nullable: false),
                    Do = table.Column<int>(type: "integer", nullable: false),
                    Pasmo = table.Column<int>(type: "integer", nullable: false),
                    Wlasciciel = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: true)
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
                });

            migrationBuilder.CreateTable(
                name: "PotwierdzeniaTerenowe",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Typ = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    Url = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    Punkt = table.Column<int>(type: "integer", nullable: false),
                    Administracyjny = table.Column<bool>(type: "boolean", nullable: false)
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
                    table.ForeignKey(
                        name: "FK_PotwierdzeniaTerenowe_TypyPotwierdzenTerenowych_Typ",
                        column: x => x.Typ,
                        principalTable: "TypyPotwierdzenTerenowych",
                        principalColumn: "Typ",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Weryfikacje",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Wycieczka = table.Column<int>(type: "integer", nullable: false),
                    Przodownik = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    Data = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Zaakceptiowana = table.Column<bool>(type: "boolean", nullable: false),
                    PowodOdrzucenia = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true)
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
                });

            migrationBuilder.CreateTable(
                name: "PrzebyteOdcinki",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Kolejnosc = table.Column<int>(type: "integer", nullable: false),
                    Wycieczka = table.Column<int>(type: "integer", nullable: false),
                    OdcinekId = table.Column<int>(type: "integer", nullable: false),
                    Powrot = table.Column<bool>(type: "boolean", nullable: false)
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
                });

            migrationBuilder.CreateTable(
                name: "ZamknieciaOdcinkow",
                columns: table => new
                {
                    OdcinekId = table.Column<int>(type: "integer", nullable: false),
                    DataZamkniecia = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    DataOtwarcia = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Przyczyna = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true)
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
                });

            migrationBuilder.CreateTable(
                name: "PotwierdzeniaTerenowePrzebytychOdcinkow",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Potwierdzenie = table.Column<int>(type: "integer", nullable: false),
                    PrzebytyOdcinekId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PotwierdzeniaTerenowePrzebytychOdcinkow", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PotwierdzeniaTerenowePrzebytychOdcinkow_PotwierdzeniaTereno~",
                        column: x => x.Potwierdzenie,
                        principalTable: "PotwierdzeniaTerenowe",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PotwierdzeniaTerenowePrzebytychOdcinkow_PrzebyteOdcinki_Prz~",
                        column: x => x.PrzebytyOdcinekId,
                        principalTable: "PrzebyteOdcinki",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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
                name: "IX_PotwierdzeniaTerenowe_Typ",
                table: "PotwierdzeniaTerenowe",
                column: "Typ");

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
                name: "IX_Wycieczki_Status",
                table: "Wycieczki",
                column: "Status");

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
                name: "TypyPotwierdzenTerenowych");

            migrationBuilder.DropTable(
                name: "Odcinki");

            migrationBuilder.DropTable(
                name: "Wycieczki");

            migrationBuilder.DropTable(
                name: "PasmaGorskie");

            migrationBuilder.DropTable(
                name: "PunktyTerenowe");

            migrationBuilder.DropTable(
                name: "StatusyWycieczek");

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
