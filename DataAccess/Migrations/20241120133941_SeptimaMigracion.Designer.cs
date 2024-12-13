﻿// <auto-generated />
using System;
using DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DataAccess.Migrations
{
    [DbContext(typeof(ContextoSql))]
    [Migration("20241120133941_SeptimaMigracion")]
    partial class SeptimaMigracion
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.35")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Domain.Comentario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Estado")
                        .HasColumnType("bit");

                    b.Property<DateTime>("FechaResolucion")
                        .HasColumnType("datetime2");

                    b.Property<int>("TareaId")
                        .HasColumnType("int");

                    b.Property<string>("Titulo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UsuarioCreadorId")
                        .HasColumnType("int");

                    b.Property<int?>("UsuarioResolvedorId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TareaId");

                    b.HasIndex("UsuarioCreadorId");

                    b.HasIndex("UsuarioResolvedorId");

                    b.ToTable("Comentarios");
                });

            modelBuilder.Entity("Domain.Epica", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("EsfuerzoEstimado")
                        .HasColumnType("int");

                    b.Property<int>("EsfuerzoInvertido")
                        .HasColumnType("int");

                    b.Property<DateTime>("FechaVencimiento")
                        .HasColumnType("datetime2");

                    b.Property<int>("PanelId")
                        .HasColumnType("int");

                    b.Property<int>("Prioridad")
                        .HasColumnType("int");

                    b.Property<string>("Titulo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("PanelId");

                    b.ToTable("Epicas");
                });

            modelBuilder.Entity("Domain.Equipo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("CantidadMaxima")
                        .HasColumnType("int");

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Equipos");
                });

            modelBuilder.Entity("Domain.Notificacion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("ComentarioId")
                        .HasColumnType("int");

                    b.Property<int>("UsuarioId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ComentarioId");

                    b.HasIndex("UsuarioId");

                    b.ToTable("Notificaciones");
                });

            modelBuilder.Entity("Domain.Panel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EquipoAlQuePertenece")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("EquipoId")
                        .HasColumnType("int");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("PapeleraId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("EquipoId");

                    b.HasIndex("PapeleraId");

                    b.ToTable("Paneles");
                });

            modelBuilder.Entity("Domain.Papelera", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("CantidadObjetosEnPapelera")
                        .HasColumnType("int");

                    b.Property<int>("UsuarioId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UsuarioId")
                        .IsUnique();

                    b.ToTable("Papeleras");
                });

            modelBuilder.Entity("Domain.Tarea", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("EpicaId")
                        .HasColumnType("int");

                    b.Property<int>("EsfuerzoEstimado")
                        .HasColumnType("int");

                    b.Property<int>("EsfuerzoInvertido")
                        .HasColumnType("int");

                    b.Property<DateTime>("FechaExpiracion")
                        .HasColumnType("datetime2");

                    b.Property<int>("PanelActualId")
                        .HasColumnType("int");

                    b.Property<int>("PanelOriginalId")
                        .HasColumnType("int");

                    b.Property<int?>("PapeleraId")
                        .HasColumnType("int");

                    b.Property<int>("Prioridad")
                        .HasColumnType("int");

                    b.Property<string>("Titulo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("EpicaId");

                    b.HasIndex("PanelActualId");

                    b.HasIndex("PanelOriginalId");

                    b.HasIndex("PapeleraId");

                    b.ToTable("Tareas");
                });

            modelBuilder.Entity("Domain.Usuario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Apellido")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Contrasena")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("EsAdmin")
                        .HasColumnType("bit");

                    b.Property<DateTime>("FechaNacimiento")
                        .HasColumnType("datetime2");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Usuarios");
                });

            modelBuilder.Entity("UsuarioEquipo", b =>
                {
                    b.Property<int>("EquipoId")
                        .HasColumnType("int");

                    b.Property<int>("UsuarioId")
                        .HasColumnType("int");

                    b.HasKey("EquipoId", "UsuarioId");

                    b.HasIndex("UsuarioId");

                    b.ToTable("UsuarioEquipo");
                });

            modelBuilder.Entity("Domain.Comentario", b =>
                {
                    b.HasOne("Domain.Tarea", "Tarea")
                        .WithMany("Comentarios")
                        .HasForeignKey("TareaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Usuario", "UsuarioCreador")
                        .WithMany()
                        .HasForeignKey("UsuarioCreadorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Usuario", "UsuarioResolvedor")
                        .WithMany()
                        .HasForeignKey("UsuarioResolvedorId");

                    b.Navigation("Tarea");

                    b.Navigation("UsuarioCreador");

                    b.Navigation("UsuarioResolvedor");
                });

            modelBuilder.Entity("Domain.Epica", b =>
                {
                    b.HasOne("Domain.Panel", "Panel")
                        .WithMany("Epicas")
                        .HasForeignKey("PanelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Panel");
                });

            modelBuilder.Entity("Domain.Notificacion", b =>
                {
                    b.HasOne("Domain.Comentario", "Comentario")
                        .WithMany()
                        .HasForeignKey("ComentarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Usuario", "Usuario")
                        .WithMany("Notificaciones")
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Comentario");

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("Domain.Panel", b =>
                {
                    b.HasOne("Domain.Equipo", "Equipo")
                        .WithMany("Paneles")
                        .HasForeignKey("EquipoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Papelera", null)
                        .WithMany("Paneles")
                        .HasForeignKey("PapeleraId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("Equipo");
                });

            modelBuilder.Entity("Domain.Papelera", b =>
                {
                    b.HasOne("Domain.Usuario", "Usuario")
                        .WithOne("Papelera")
                        .HasForeignKey("Domain.Papelera", "UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("Domain.Tarea", b =>
                {
                    b.HasOne("Domain.Epica", "Epica")
                        .WithMany("Tareas")
                        .HasForeignKey("EpicaId");

                    b.HasOne("Domain.Panel", "PanelActual")
                        .WithMany("Tareas")
                        .HasForeignKey("PanelActualId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Panel", "PanelOriginal")
                        .WithMany()
                        .HasForeignKey("PanelOriginalId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Papelera", null)
                        .WithMany("Tareas")
                        .HasForeignKey("PapeleraId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("Epica");

                    b.Navigation("PanelActual");

                    b.Navigation("PanelOriginal");
                });

            modelBuilder.Entity("UsuarioEquipo", b =>
                {
                    b.HasOne("Domain.Equipo", null)
                        .WithMany()
                        .HasForeignKey("EquipoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Usuario", null)
                        .WithMany()
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.Epica", b =>
                {
                    b.Navigation("Tareas");
                });

            modelBuilder.Entity("Domain.Equipo", b =>
                {
                    b.Navigation("Paneles");
                });

            modelBuilder.Entity("Domain.Panel", b =>
                {
                    b.Navigation("Epicas");

                    b.Navigation("Tareas");
                });

            modelBuilder.Entity("Domain.Papelera", b =>
                {
                    b.Navigation("Paneles");

                    b.Navigation("Tareas");
                });

            modelBuilder.Entity("Domain.Tarea", b =>
                {
                    b.Navigation("Comentarios");
                });

            modelBuilder.Entity("Domain.Usuario", b =>
                {
                    b.Navigation("Notificaciones");

                    b.Navigation("Papelera")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
