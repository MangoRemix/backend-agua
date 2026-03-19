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
    public DbSet<PersonaAfectada> PersonasAfectadas { get; set; }
}