using Microsoft.EntityFrameworkCore.Migrations;

namespace Projekat.Migrations
{
    public partial class V6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "brojMesta",
                table: "Odrzavaju");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "brojMesta",
                table: "Odrzavaju",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
