using backend_agua.Infraestructure.Database;
using backend_agua.Models;
using Microsoft.Extensions.DependencyInjection;

namespace backend_agua.Infraestructure.Seeders;

public static class MunicipioSeeder
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        // Si ya existen municipios, no re-ejecutamos para garantizar integridad
        if (context.Municipios.Any()) return;

        var sucreId = Guid.Parse("7b2e8a15-4c0a-4b7d-9a8f-2e5b8c1a9d3e");

        // Crear Municipio Sucre si no existe
        if (!context.Municipios.Any(m => m.Nombre == "Sucre"))
        {
            context.Municipios.Add(new Municipio
            {
                Id = sucreId,
                Nombre = "Sucre"
            });
            context.SaveChanges();
        }
    }
}
