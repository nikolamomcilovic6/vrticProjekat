using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Projekat.Migrations
{
    public partial class V1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Administratori",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ime = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Prezime = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Sifra = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Administratori", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Aktivnosti",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Aktivnosti", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Vrtici",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    AdministratorID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vrtici", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Vrtici_Administratori_AdministratorID",
                        column: x => x.AdministratorID,
                        principalTable: "Administratori",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Deca",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ime = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Prezime = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    brojRoditelja = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VrticID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Deca", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Deca_Vrtici_VrticID",
                        column: x => x.VrticID,
                        principalTable: "Vrtici",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Odrzavaju",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    brojMesta = table.Column<int>(type: "int", nullable: false),
                    DatumOdrzavanja = table.Column<DateTime>(type: "datetime2", nullable: false),
                    VrticID = table.Column<int>(type: "int", nullable: true),
                    AktivnostID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Odrzavaju", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Odrzavaju_Aktivnosti_AktivnostID",
                        column: x => x.AktivnostID,
                        principalTable: "Aktivnosti",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Odrzavaju_Vrtici_VrticID",
                        column: x => x.VrticID,
                        principalTable: "Vrtici",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Vaspitaci",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ime = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Prezime = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    JMBG = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: false),
                    VrticID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vaspitaci", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Vaspitaci_Vrtici_VrticID",
                        column: x => x.VrticID,
                        principalTable: "Vrtici",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AktivnostDete",
                columns: table => new
                {
                    AktivnostDeteID = table.Column<int>(type: "int", nullable: false),
                    DeteAktivnostID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AktivnostDete", x => new { x.AktivnostDeteID, x.DeteAktivnostID });
                    table.ForeignKey(
                        name: "FK_AktivnostDete_Aktivnosti_DeteAktivnostID",
                        column: x => x.DeteAktivnostID,
                        principalTable: "Aktivnosti",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AktivnostDete_Deca_AktivnostDeteID",
                        column: x => x.AktivnostDeteID,
                        principalTable: "Deca",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AktivnostVaspitac",
                columns: table => new
                {
                    AktivnostVaspitacID = table.Column<int>(type: "int", nullable: false),
                    VaspitacAktivnostID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AktivnostVaspitac", x => new { x.AktivnostVaspitacID, x.VaspitacAktivnostID });
                    table.ForeignKey(
                        name: "FK_AktivnostVaspitac_Aktivnosti_VaspitacAktivnostID",
                        column: x => x.VaspitacAktivnostID,
                        principalTable: "Aktivnosti",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AktivnostVaspitac_Vaspitaci_AktivnostVaspitacID",
                        column: x => x.AktivnostVaspitacID,
                        principalTable: "Vaspitaci",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AktivnostDete_DeteAktivnostID",
                table: "AktivnostDete",
                column: "DeteAktivnostID");

            migrationBuilder.CreateIndex(
                name: "IX_AktivnostVaspitac_VaspitacAktivnostID",
                table: "AktivnostVaspitac",
                column: "VaspitacAktivnostID");

            migrationBuilder.CreateIndex(
                name: "IX_Deca_VrticID",
                table: "Deca",
                column: "VrticID");

            migrationBuilder.CreateIndex(
                name: "IX_Odrzavaju_AktivnostID",
                table: "Odrzavaju",
                column: "AktivnostID");

            migrationBuilder.CreateIndex(
                name: "IX_Odrzavaju_VrticID",
                table: "Odrzavaju",
                column: "VrticID");

            migrationBuilder.CreateIndex(
                name: "IX_Vaspitaci_VrticID",
                table: "Vaspitaci",
                column: "VrticID");

            migrationBuilder.CreateIndex(
                name: "IX_Vrtici_AdministratorID",
                table: "Vrtici",
                column: "AdministratorID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AktivnostDete");

            migrationBuilder.DropTable(
                name: "AktivnostVaspitac");

            migrationBuilder.DropTable(
                name: "Odrzavaju");

            migrationBuilder.DropTable(
                name: "Deca");

            migrationBuilder.DropTable(
                name: "Vaspitaci");

            migrationBuilder.DropTable(
                name: "Aktivnosti");

            migrationBuilder.DropTable(
                name: "Vrtici");

            migrationBuilder.DropTable(
                name: "Administratori");
        }
    }
}
