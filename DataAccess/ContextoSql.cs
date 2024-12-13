
using Domain;
using Microsoft.EntityFrameworkCore;
namespace DataAccess;

public class ContextoSql:DbContext
{
    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Equipo> Equipos { get; set; }
    public DbSet<Panel> Paneles { get; set; }
    public DbSet<Tarea> Tareas { get; set; }
    public DbSet<Comentario> Comentarios { get; set; }
    public DbSet<Papelera> Papeleras { get; set; }
    public DbSet<Notificacion> Notificaciones { get; set; }
    public DbSet<Epica> Epicas { get; set; }
    
    public ContextoSql(DbContextOptions<ContextoSql> options) : base(options)
    {
        if (!Database.IsInMemory())
        {
            Database.Migrate();
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Panel>()
            .HasMany(p => p.Tareas)
            .WithOne(t => t.PanelActual);
        
        modelBuilder.Entity<Usuario>()
            .HasMany(u => u.Equipos)
            .WithMany(e => e.Usuarios)
            .UsingEntity<Dictionary<string, object>>(
                "UsuarioEquipo",
                j => j.HasOne<Equipo>().WithMany().HasForeignKey("EquipoId"),
                j => j.HasOne<Usuario>().WithMany().HasForeignKey("UsuarioId"));

        modelBuilder.Entity<Tarea>()
            .HasMany(t => t.Comentarios)
            .WithOne(c => c.Tarea);
        
        modelBuilder.Entity<Equipo>()
            .HasMany(e => e.Paneles)
            .WithOne(p => p.Equipo)
            .HasForeignKey(p => p.EquipoId);
        
        modelBuilder.Entity<Notificacion>()
            .HasOne(n => n.Usuario)
            .WithMany(u => u.Notificaciones)
            .HasForeignKey(n => n.UsuarioId)
            .IsRequired();
        
        modelBuilder.Entity<Usuario>()
            .HasOne(u => u.Papelera)
            .WithOne(p => p.Usuario)
            .HasForeignKey<Papelera>(p => p.UsuarioId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Papelera>()
            .HasMany(p => p.Tareas)
            .WithOne()
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Papelera>()
            .HasMany(p => p.Paneles)
            .WithOne()
            .OnDelete(DeleteBehavior.Cascade);
        
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Papelera>().Property(p => p.CantidadObjetosEnPapelera).IsRequired();
        
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Epica>().Property(e => e.EsfuerzoEstimado).IsRequired();
        modelBuilder.Entity<Epica>().Property(e => e.EsfuerzoInvertido).IsRequired();
        
        base.OnModelCreating(modelBuilder);
    }
}