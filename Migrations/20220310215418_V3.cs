using Microsoft.EntityFrameworkCore.Migrations;

namespace Projekat.Migrations
{
    public partial class V3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AktivnostDete");

            migrationBuilder.DropTable(
                name: "AktivnostVaspitac");

            migrationBuilder.CreateTable(
                name: "Nadgledaju",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VaspitacID = table.Column<int>(type: "int", nullable: true),
                    AktivnostID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Nadgledaju", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Nadgledaju_Aktivnosti_AktivnostID",
                        column: x => x.AktivnostID,
                        principalTable: "Aktivnosti",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Nadgledaju_Vaspitaci_VaspitacID",
                        column: x => x.VaspitacID,
                        principalTable: "Vaspitaci",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Ucestvuju",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DeteID = table.Column<int>(type: "int", nullable: true),
                    AktivnostID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ucestvuju", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Ucestvuju_Aktivnosti_AktivnostID",
                        column: x => x.AktivnostID,
                        principalTable: "Aktivnosti",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Ucestvuju_Deca_DeteID",
                        column: x => x.DeteID,
                        principalTable: "Deca",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Nadgledaju_AktivnostID",
                table: "Nadgledaju",
                column: "AktivnostID");

            migrationBuilder.CreateIndex(
                name: "IX_Nadgledaju_VaspitacID",
                table: "Nadgledaju",
                column: "VaspitacID");

            migrationBuilder.CreateIndex(
                name: "IX_Ucestvuju_AktivnostID",
                table: "Ucestvuju",
                column: "AktivnostID");

            migrationBuilder.CreateIndex(
                name: "IX_Ucestvuju_DeteID",
                table: "Ucestvuju",
                column: "DeteID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Nadgledaju");

            migrationBuilder.DropTable(
                name: "Ucestvuju");

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
        }
    }
}
