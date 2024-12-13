using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    public partial class SeptimaMigracion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EsfuerzoEstimado",
                table: "Epicas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "EsfuerzoInvertido",
                table: "Epicas",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EsfuerzoEstimado",
                table: "Epicas");

            migrationBuilder.DropColumn(
                name: "EsfuerzoInvertido",
                table: "Epicas");
        }
    }
}
