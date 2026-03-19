using backend_agua.Infraestructure.Database;
using backend_agua.Models;

namespace backend_agua.Infraestructure.Seeders;

public static class UsuarioSeeder
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using var context = serviceProvider.GetRequiredService<ApplicationDbContext>();

        // Crear SuperAdmin si no existe
        if (!context.Usuarios.Any(u => u.Cedula == "24877124"))
        {
            context.Usuarios.Add(new Usuario
            {
                Id = Guid.NewGuid(),
                Nombre = "Super",
                Apellido = "Admin",
                Cedula = "24877124",
                Password = BCrypt.Net.BCrypt.HashPassword("123456"),
                Rol = UsuarioRol.SuperAdmin,
                Status = StatusUsuario.Activo
            });
            context.SaveChanges();
        }
    }
}
