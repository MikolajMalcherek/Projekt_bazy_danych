using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace Projekt_bazy_danych.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Miejscowosci",
                columns: table => new
                {
                    idmiejscowosci = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    nazwa_miejscowosci = table.Column<string>(type: "longtext", nullable: false),
                    kraj_miejscowosci = table.Column<string>(type: "longtext", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Miejscowosci", x => x.idmiejscowosci);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Zawodnicy",
                columns: table => new
                {
                    idzawodnicy = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    imie_zawodnika = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    nazwisko_zawodnika = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    kraj_pochodzenia = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Zawodnicy", x => x.idzawodnicy);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Wyniki",
                columns: table => new
                {
                    idwyniki = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    wynik = table.Column<int>(type: "int", nullable: false),
                    idzawodnicy = table.Column<int>(type: "int", nullable: false),
                    idmiejscowosci = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wyniki", x => x.idwyniki);
                    table.ForeignKey(
                        name: "FK_Wyniki_Miejscowosci_idmiejscowosci",
                        column: x => x.idmiejscowosci,
                        principalTable: "Miejscowosci",
                        principalColumn: "idmiejscowosci",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Wyniki_Zawodnicy_idzawodnicy",
                        column: x => x.idzawodnicy,
                        principalTable: "Zawodnicy",
                        principalColumn: "idzawodnicy",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Wyniki_idmiejscowosci",
                table: "Wyniki",
                column: "idmiejscowosci");

            migrationBuilder.CreateIndex(
                name: "IX_Wyniki_idzawodnicy",
                table: "Wyniki",
                column: "idzawodnicy");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Wyniki");

            migrationBuilder.DropTable(
                name: "Miejscowosci");

            migrationBuilder.DropTable(
                name: "Zawodnicy");
        }
    }
}
