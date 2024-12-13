using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    public partial class QuintaMigracion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Paneles_Papeleras_PapeleraId",
                table: "Paneles");

            migrationBuilder.DropForeignKey(
                name: "FK_Tareas_Papeleras_PapeleraId",
                table: "Tareas");

            migrationBuilder.AddColumn<int>(
                name: "UsuarioId",
                table: "Papeleras",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Papeleras_UsuarioId",
                table: "Papeleras",
                column: "UsuarioId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Paneles_Papeleras_PapeleraId",
                table: "Paneles",
                column: "PapeleraId",
                principalTable: "Papeleras",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Papeleras_Usuarios_UsuarioId",
                table: "Papeleras",
                column: "UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Tareas_Papeleras_PapeleraId",
                table: "Tareas",
                column: "PapeleraId",
                principalTable: "Papeleras",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Paneles_Papeleras_PapeleraId",
                table: "Paneles");

            migrationBuilder.DropForeignKey(
                name: "FK_Papeleras_Usuarios_UsuarioId",
                table: "Papeleras");

            migrationBuilder.DropForeignKey(
                name: "FK_Tareas_Papeleras_PapeleraId",
                table: "Tareas");

            migrationBuilder.DropIndex(
                name: "IX_Papeleras_UsuarioId",
                table: "Papeleras");

            migrationBuilder.DropColumn(
                name: "UsuarioId",
                table: "Papeleras");

            migrationBuilder.AddForeignKey(
                name: "FK_Paneles_Papeleras_PapeleraId",
                table: "Paneles",
                column: "PapeleraId",
                principalTable: "Papeleras",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tareas_Papeleras_PapeleraId",
                table: "Tareas",
                column: "PapeleraId",
                principalTable: "Papeleras",
                principalColumn: "Id");
        }
    }
}
