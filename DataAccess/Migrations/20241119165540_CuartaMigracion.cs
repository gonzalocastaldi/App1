using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    public partial class CuartaMigracion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Epicas_Paneles_PanelId",
                table: "Epicas");

            migrationBuilder.DropForeignKey(
                name: "FK_Notificaciones_Usuarios_UsuarioId",
                table: "Notificaciones");

            migrationBuilder.DropForeignKey(
                name: "FK_Paneles_Equipos_EquipoId",
                table: "Paneles");

            migrationBuilder.AlterColumn<int>(
                name: "EquipoId",
                table: "Paneles",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "UsuarioId",
                table: "Notificaciones",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "PanelId",
                table: "Epicas",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Epicas_Paneles_PanelId",
                table: "Epicas",
                column: "PanelId",
                principalTable: "Paneles",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Notificaciones_Usuarios_UsuarioId",
                table: "Notificaciones",
                column: "UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Paneles_Equipos_EquipoId",
                table: "Paneles",
                column: "EquipoId",
                principalTable: "Equipos",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Epicas_Paneles_PanelId",
                table: "Epicas");

            migrationBuilder.DropForeignKey(
                name: "FK_Notificaciones_Usuarios_UsuarioId",
                table: "Notificaciones");

            migrationBuilder.DropForeignKey(
                name: "FK_Paneles_Equipos_EquipoId",
                table: "Paneles");

            migrationBuilder.AlterColumn<int>(
                name: "EquipoId",
                table: "Paneles",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "UsuarioId",
                table: "Notificaciones",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "PanelId",
                table: "Epicas",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Epicas_Paneles_PanelId",
                table: "Epicas",
                column: "PanelId",
                principalTable: "Paneles",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Notificaciones_Usuarios_UsuarioId",
                table: "Notificaciones",
                column: "UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Paneles_Equipos_EquipoId",
                table: "Paneles",
                column: "EquipoId",
                principalTable: "Equipos",
                principalColumn: "Id");
        }
    }
}
