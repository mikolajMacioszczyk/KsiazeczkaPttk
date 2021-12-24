using Microsoft.EntityFrameworkCore.Migrations;

namespace KsiazeczkaPttk.DAL.Migrations
{
    public partial class removeConstraint : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Odcinki_Uzytkownicy_Wlasciciel",
                table: "Odcinki");

            migrationBuilder.AlterColumn<string>(
                name: "Wlasciciel",
                table: "Odcinki",
                type: "character varying(30)",
                maxLength: 30,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(30)",
                oldMaxLength: 30);

            migrationBuilder.AddForeignKey(
                name: "FK_Odcinki_Uzytkownicy_Wlasciciel",
                table: "Odcinki",
                column: "Wlasciciel",
                principalTable: "Uzytkownicy",
                principalColumn: "Login",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Odcinki_Uzytkownicy_Wlasciciel",
                table: "Odcinki");

            migrationBuilder.AlterColumn<string>(
                name: "Wlasciciel",
                table: "Odcinki",
                type: "character varying(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(30)",
                oldMaxLength: 30,
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Odcinki_Uzytkownicy_Wlasciciel",
                table: "Odcinki",
                column: "Wlasciciel",
                principalTable: "Uzytkownicy",
                principalColumn: "Login",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
