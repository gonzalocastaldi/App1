using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    public partial class PrimeraMigracion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Equipos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CantidadMaxima = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Equipos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Papeleras",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Papeleras", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Apellido = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaNacimiento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Contrasena = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EsAdmin = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Paneles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EquipoAlQuePertenece = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EquipoId = table.Column<int>(type: "int", nullable: true),
                    PapeleraId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Paneles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Paneles_Equipos_EquipoId",
                        column: x => x.EquipoId,
                        principalTable: "Equipos",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Paneles_Papeleras_PapeleraId",
                        column: x => x.PapeleraId,
                        principalTable: "Papeleras",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UsuarioEquipo",
                columns: table => new
                {
                    EquipoId = table.Column<int>(type: "int", nullable: false),
                    UsuarioId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsuarioEquipo", x => new { x.EquipoId, x.UsuarioId });
                    table.ForeignKey(
                        name: "FK_UsuarioEquipo_Equipos_EquipoId",
                        column: x => x.EquipoId,
                        principalTable: "Equipos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UsuarioEquipo_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Epicas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titulo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Prioridad = table.Column<int>(type: "int", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaVencimiento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PanelId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Epicas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Epicas_Paneles_PanelId",
                        column: x => x.PanelId,
                        principalTable: "Paneles",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Tareas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titulo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaExpiracion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Prioridad = table.Column<int>(type: "int", nullable: false),
                    PanelActualId = table.Column<int>(type: "int", nullable: false),
                    PanelOriginalId = table.Column<int>(type: "int", nullable: false),
                    EsfuerzoEstimado = table.Column<int>(type: "int", nullable: false),
                    EpicaId = table.Column<int>(type: "int", nullable: true),
                    PapeleraId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tareas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tareas_Epicas_EpicaId",
                        column: x => x.EpicaId,
                        principalTable: "Epicas",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Tareas_Paneles_PanelActualId",
                        column: x => x.PanelActualId,
                        principalTable: "Paneles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tareas_Paneles_PanelOriginalId",
                        column: x => x.PanelOriginalId,
                        principalTable: "Paneles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Tareas_Papeleras_PapeleraId",
                        column: x => x.PapeleraId,
                        principalTable: "Papeleras",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Comentarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titulo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Estado = table.Column<bool>(type: "bit", nullable: false),
                    UsuarioCreadorId = table.Column<int>(type: "int", nullable: false),
                    UsuarioResolvedorId = table.Column<int>(type: "int", nullable: false),
                    FechaResolucion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TareaId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comentarios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comentarios_Tareas_TareaId",
                        column: x => x.TareaId,
                        principalTable: "Tareas",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Comentarios_Usuarios_UsuarioCreadorId",
                        column: x => x.UsuarioCreadorId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Comentarios_Usuarios_UsuarioResolvedorId",
                        column: x => x.UsuarioResolvedorId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Notificaciones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ComentarioId = table.Column<int>(type: "int", nullable: false),
                    UsuarioId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notificaciones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Notificaciones_Comentarios_ComentarioId",
                        column: x => x.ComentarioId,
                        principalTable: "Comentarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Notificaciones_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comentarios_TareaId",
                table: "Comentarios",
                column: "TareaId");

            migrationBuilder.CreateIndex(
                name: "IX_Comentarios_UsuarioCreadorId",
                table: "Comentarios",
                column: "UsuarioCreadorId");

            migrationBuilder.CreateIndex(
                name: "IX_Comentarios_UsuarioResolvedorId",
                table: "Comentarios",
                column: "UsuarioResolvedorId");

            migrationBuilder.CreateIndex(
                name: "IX_Epicas_PanelId",
                table: "Epicas",
                column: "PanelId");

            migrationBuilder.CreateIndex(
                name: "IX_Notificaciones_ComentarioId",
                table: "Notificaciones",
                column: "ComentarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Notificaciones_UsuarioId",
                table: "Notificaciones",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Paneles_EquipoId",
                table: "Paneles",
                column: "EquipoId");

            migrationBuilder.CreateIndex(
                name: "IX_Paneles_PapeleraId",
                table: "Paneles",
                column: "PapeleraId");

            migrationBuilder.CreateIndex(
                name: "IX_Tareas_EpicaId",
                table: "Tareas",
                column: "EpicaId");

            migrationBuilder.CreateIndex(
                name: "IX_Tareas_PanelActualId",
                table: "Tareas",
                column: "PanelActualId");

            migrationBuilder.CreateIndex(
                name: "IX_Tareas_PanelOriginalId",
                table: "Tareas",
                column: "PanelOriginalId");

            migrationBuilder.CreateIndex(
                name: "IX_Tareas_PapeleraId",
                table: "Tareas",
                column: "PapeleraId");

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioEquipo_UsuarioId",
                table: "UsuarioEquipo",
                column: "UsuarioId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Notificaciones");

            migrationBuilder.DropTable(
                name: "UsuarioEquipo");

            migrationBuilder.DropTable(
                name: "Comentarios");

            migrationBuilder.DropTable(
                name: "Tareas");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "Epicas");

            migrationBuilder.DropTable(
                name: "Paneles");

            migrationBuilder.DropTable(
                name: "Equipos");

            migrationBuilder.DropTable(
                name: "Papeleras");
        }
    }
}
