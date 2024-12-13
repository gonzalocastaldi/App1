using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    public partial class TerceraMigracion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            /*
            migrationBuilder.DropForeignKey(
                name: "FK_Comentarios_Usuarios_UsuarioResolvedorId",
                table: "Comentarios");
*/
            migrationBuilder.AddColumn<int>(
                name: "EsfuerzoInvertido",
                table: "Tareas",
                type: "int",
                nullable: false,
                defaultValue: 0);
/*
            migrationBuilder.AlterColumn<int>(
                name: "UsuarioResolvedorId",
                table: "Comentarios",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Comentarios_Usuarios_UsuarioResolvedorId",
                table: "Comentarios",
                column: "UsuarioResolvedorId",
                principalTable: "Usuarios",
                principalColumn: "Id");
                */
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            /*
            migrationBuilder.DropForeignKey(
                name: "FK_Comentarios_Usuarios_UsuarioResolvedorId",
                table: "Comentarios");
*/
            migrationBuilder.DropColumn(
                name: "EsfuerzoInvertido",
                table: "Tareas");
/*
            migrationBuilder.AlterColumn<int>(
                name: "UsuarioResolvedorId",
                table: "Comentarios",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Comentarios_Usuarios_UsuarioResolvedorId",
                table: "Comentarios",
                column: "UsuarioResolvedorId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
                */
        }
    }
}
