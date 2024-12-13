using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    public partial class SegundaMigracion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
           // Drop foreign keys and primary key
            migrationBuilder.DropForeignKey(
                name: "FK_UsuarioEquipo_Equipos_EquipoId",
                table: "UsuarioEquipo");

            migrationBuilder.DropForeignKey(
                name: "FK_Paneles_Equipos_EquipoId",
                table: "Paneles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Equipos",
                table: "Equipos");

            // Ensure all existing records in UsuarioEquipo have valid references
            migrationBuilder.Sql("DELETE FROM UsuarioEquipo WHERE EquipoId NOT IN (SELECT Id FROM Equipos)");

            // Drop the existing column
            migrationBuilder.DropColumn(
                name: "Id",
                table: "Equipos");

            // Recreate the column with the new IDENTITY property
            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Equipos",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            // Make UsuarioResolvedorId nullable
            migrationBuilder.AlterColumn<int>(
                name: "UsuarioResolvedorId",
                table: "Comentarios",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            // Drop existing foreign key if it exists
            migrationBuilder.Sql(@"
                IF EXISTS (SELECT 1 FROM sys.foreign_keys WHERE name = 'FK_Comentarios_Usuarios_UsuarioResolvedorId')
                BEGIN
                    ALTER TABLE [Comentarios] DROP CONSTRAINT [FK_Comentarios_Usuarios_UsuarioResolvedorId];
                END
            ");

            // Add primary key and foreign keys
            migrationBuilder.AddPrimaryKey(
                name: "PK_Equipos",
                table: "Equipos",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UsuarioEquipo_Equipos_EquipoId",
                table: "UsuarioEquipo",
                column: "EquipoId",
                principalTable: "Equipos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Paneles_Equipos_EquipoId",
                table: "Paneles",
                column: "EquipoId",
                principalTable: "Equipos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Comentarios_Usuarios_UsuarioResolvedorId",
                table: "Comentarios",
                column: "UsuarioResolvedorId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Drop foreign keys and primary key
            migrationBuilder.DropForeignKey(
                name: "FK_UsuarioEquipo_Equipos_EquipoId",
                table: "UsuarioEquipo");

            migrationBuilder.DropForeignKey(
                name: "FK_Paneles_Equipos_EquipoId",
                table: "Paneles");

            migrationBuilder.DropForeignKey(
                name: "FK_Comentarios_Usuarios_UsuarioResolvedorId",
                table: "Comentarios");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Equipos",
                table: "Equipos");

            // Drop the recreated column
            migrationBuilder.DropColumn(
                name: "Id",
                table: "Equipos");

            // Recreate the original column
            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Equipos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            // Revert UsuarioResolvedorId to non-nullable
            migrationBuilder.AlterColumn<int>(
                name: "UsuarioResolvedorId",
                table: "Comentarios",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            // Re-add primary key and foreign keys
            migrationBuilder.AddPrimaryKey(
                name: "PK_Equipos",
                table: "Equipos",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UsuarioEquipo_Equipos_EquipoId",
                table: "UsuarioEquipo",
                column: "EquipoId",
                principalTable: "Equipos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Paneles_Equipos_EquipoId",
                table: "Paneles",
                column: "EquipoId",
                principalTable: "Equipos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Comentarios_Usuarios_UsuarioResolvedorId",
                table: "Comentarios",
                column: "UsuarioResolvedorId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
