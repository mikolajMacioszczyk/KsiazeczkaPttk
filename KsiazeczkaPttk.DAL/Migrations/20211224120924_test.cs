using Microsoft.EntityFrameworkCore.Migrations;

namespace KsiazeczkaPttk.DAL.Migrations
{
    public partial class test : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Uzytkownicy_RoleUzytkownikow_RolaNazwa",
                table: "Uzytkownicy");

            migrationBuilder.DropIndex(
                name: "IX_Uzytkownicy_RolaNazwa",
                table: "Uzytkownicy");

            migrationBuilder.DropColumn(
                name: "RolaNazwa",
                table: "Uzytkownicy");

            migrationBuilder.AddColumn<string>(
                name: "Rola",
                table: "Uzytkownicy",
                type: "character varying(40)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Uzytkownicy_Rola",
                table: "Uzytkownicy",
                column: "Rola");

            migrationBuilder.AddForeignKey(
                name: "FK_Uzytkownicy_RoleUzytkownikow_Rola",
                table: "Uzytkownicy",
                column: "Rola",
                principalTable: "RoleUzytkownikow",
                principalColumn: "Nazwa",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Uzytkownicy_RoleUzytkownikow_Rola",
                table: "Uzytkownicy");

            migrationBuilder.DropIndex(
                name: "IX_Uzytkownicy_Rola",
                table: "Uzytkownicy");

            migrationBuilder.DropColumn(
                name: "Rola",
                table: "Uzytkownicy");

            migrationBuilder.AddColumn<string>(
                name: "RolaNazwa",
                table: "Uzytkownicy",
                type: "character varying(40)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Uzytkownicy_RolaNazwa",
                table: "Uzytkownicy",
                column: "RolaNazwa");

            migrationBuilder.AddForeignKey(
                name: "FK_Uzytkownicy_RoleUzytkownikow_RolaNazwa",
                table: "Uzytkownicy",
                column: "RolaNazwa",
                principalTable: "RoleUzytkownikow",
                principalColumn: "Nazwa",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
