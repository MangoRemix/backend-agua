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
}