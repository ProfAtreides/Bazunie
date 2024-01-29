﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Meetings.Migrations
{
    /// <inheritdoc />
    public partial class initialsetup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "filie",
                columns: table => new
                {
                    idFilii = table.Column<int>(type: "int", nullable: false),
                    nazwa_Filii = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: true, collation: "utf8mb4_polish_ci")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.idFilii);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_polish_ci");

            migrationBuilder.CreateTable(
                name: "sala",
                columns: table => new
                {
                    idSali = table.Column<int>(type: "int", nullable: false),
                    pojemnosc = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.idSali);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_polish_ci");

            migrationBuilder.CreateTable(
                name: "działy",
                columns: table => new
                {
                    idDzialu = table.Column<int>(type: "int", nullable: false),
                    nazwa_Dzialu = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: true, collation: "utf8mb4_polish_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    idFilii = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.idDzialu);
                    table.ForeignKey(
                        name: "Działy_ibfk_1",
                        column: x => x.idFilii,
                        principalTable: "filie",
                        principalColumn: "idFilii");
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_polish_ci");

            migrationBuilder.CreateTable(
                name: "spotkania",
                columns: table => new
                {
                    idSpotkania = table.Column<int>(type: "int", nullable: false),
                    max_liczba_uczestnikow = table.Column<int>(type: "int", nullable: true),
                    godzina_spotkania = table.Column<TimeOnly>(type: "time", nullable: true),
                    dzien_tygodnia = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: true, collation: "utf8mb4_polish_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    idSali = table.Column<int>(type: "int", nullable: false),
                    idFilii = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.idSpotkania);
                    table.ForeignKey(
                        name: "fk_idFilii",
                        column: x => x.idFilii,
                        principalTable: "filie",
                        principalColumn: "idFilii");
                    table.ForeignKey(
                        name: "fk_idSali",
                        column: x => x.idSali,
                        principalTable: "sala",
                        principalColumn: "idSali");
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_polish_ci");

            migrationBuilder.CreateTable(
                name: "pracownicy",
                columns: table => new
                {
                    idPracownika = table.Column<int>(type: "int", nullable: false),
                    admin = table.Column<bool>(type: "tinyint(1)", nullable: false,defaultValue:false),
                    imie_pracownika = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: true, collation: "utf8mb4_polish_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    nazwisko_pracownika = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: true, collation: "utf8mb4_polish_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    stanowisko = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: true, collation: "utf8mb4_polish_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    idFilii = table.Column<int>(type: "int", nullable: false),
                    idDzialu = table.Column<int>(type: "int", nullable: false),
                    haslo = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: true, collation: "utf8mb4_polish_ci")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.idPracownika);
                    table.ForeignKey(
                        name: "Pracownicy_ibfk_2",
                        column: x => x.idDzialu,
                        principalTable: "działy",
                        principalColumn: "idDzialu");
                    table.ForeignKey(
                        name: "Pracownicy_ibfk_3",
                        column: x => x.idFilii,
                        principalTable: "filie",
                        principalColumn: "idFilii");
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_polish_ci");

            migrationBuilder.CreateTable(
                name: "grafik",
                columns: table => new
                {
                    idGrafiku = table.Column<int>(type: "int", nullable: false),
                    godzina_rozpoczecia = table.Column<TimeOnly>(type: "time", nullable: true),
                    godzina_zakonczenia = table.Column<TimeOnly>(type: "time", nullable: true),
                    ilosc_godzin = table.Column<int>(type: "int", nullable: true),
                    dzien_tygodnia = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: true, collation: "utf8mb4_polish_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    idPracownika = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.idGrafiku);
                    table.ForeignKey(
                        name: "fk_idPracownika",
                        column: x => x.idPracownika,
                        principalTable: "pracownicy",
                        principalColumn: "idPracownika");
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_polish_ci");

            migrationBuilder.CreateTable(
                name: "pracownicy_has_spotkania",
                columns: table => new
                {
                    idPracownika = table.Column<int>(type: "int", nullable: false),
                    idSpotkania = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => new { x.idPracownika, x.idSpotkania })
                        .Annotation("MySql:IndexPrefixLength", new[] { 0, 0 });
                    table.ForeignKey(
                        name: "Pracownicy_has_Spotkania_ibfk_1",
                        column: x => x.idSpotkania,
                        principalTable: "spotkania",
                        principalColumn: "idSpotkania");
                    table.ForeignKey(
                        name: "Pracownicy_has_Spotkania_ibfk_2",
                        column: x => x.idPracownika,
                        principalTable: "pracownicy",
                        principalColumn: "idPracownika");
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_polish_ci");

            migrationBuilder.CreateIndex(
                name: "idDzialu",
                table: "działy",
                column: "idDzialu",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "idFilii",
                table: "działy",
                column: "idFilii");

            migrationBuilder.CreateIndex(
                name: "nazwa_Filii",
                table: "filie",
                column: "nazwa_Filii",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "fk_idPracownika",
                table: "grafik",
                column: "idPracownika");

            migrationBuilder.CreateIndex(
                name: "idDzialu1",
                table: "pracownicy",
                column: "idDzialu");

            migrationBuilder.CreateIndex(
                name: "idFilii1",
                table: "pracownicy",
                column: "idFilii");

            migrationBuilder.CreateIndex(
                name: "idSpotkania",
                table: "pracownicy_has_spotkania",
                column: "idSpotkania");

            migrationBuilder.CreateIndex(
                name: "fk_idFilii",
                table: "spotkania",
                column: "idFilii");

            migrationBuilder.CreateIndex(
                name: "fk_idSali",
                table: "spotkania",
                column: "idSali");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "grafik");

            migrationBuilder.DropTable(
                name: "pracownicy_has_spotkania");

            migrationBuilder.DropTable(
                name: "spotkania");

            migrationBuilder.DropTable(
                name: "pracownicy");

            migrationBuilder.DropTable(
                name: "sala");

            migrationBuilder.DropTable(
                name: "działy");

            migrationBuilder.DropTable(
                name: "filie");
        }
    }
}
