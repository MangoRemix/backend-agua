using Microsoft.EntityFrameworkCore;
using backend_agua.Models;

namespace backend_agua.Infraestructure.Database;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Municipio> Municipios { get; set; }
    public DbSet<Parroquia> Parroquias { get; set; }
    public DbSet<Comuna> Comunas { get; set; }
    public DbSet<Comunidad> Comunidades { get; set; }
    public DbSet<Reporte> Reportes { get; set; }
    public DbSet<ReporteSuministro> ReporteSuministros { get; set; }
    public DbSet<ReporteIncidencia> ReporteIncidencias { get; set; }
    public DbSet<ReporteSalud> ReporteSalud { get; set; }
    public DbSet<ReporteParticipacion> ReporteParticipaciones { get; set; }
    public DbSet<PersonaAfectada> PersonasAfectadas { get; set; }
    public DbSet<Cisterna> Cisternas { get; set; }
    public DbSet<Tanque> Tanques { get; set; }
    public DbSet<Tranca> Trancas { get; set; }
    public DbSet<Conflicto> Conflictos { get; set; }
    public DbSet<Fuga> Fugas { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configurar Relaciones 1:1 para Reporte Modular
        modelBuilder.Entity<Reporte>()
            .HasOne(r => r.Suministro)
            .WithOne(s => s.Reporte)
            .HasForeignKey<ReporteSuministro>(s => s.ReporteId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Reporte>()
            .HasOne(r => r.Incidencia)
            .WithOne(i => i.Reporte)
            .HasForeignKey<ReporteIncidencia>(i => i.ReporteId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Reporte>()
            .HasOne(r => r.Salud)
            .WithOne(s => s.Reporte)
            .HasForeignKey<ReporteSalud>(s => s.ReporteId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Reporte>()
            .HasOne(r => r.Participacion)
            .WithOne(p => p.Reporte)
            .HasForeignKey<ReporteParticipacion>(p => p.ReporteId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}