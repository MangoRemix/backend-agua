using backend_agua.Infraestructure.Database;
using backend_agua.Models;
using Microsoft.Extensions.DependencyInjection;

namespace backend_agua.Infraestructure.Seeders;

public static class ParroquiaSeeder
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        var sucreId = Guid.Parse("7b2e8a15-4c0a-4b7d-9a8f-2e5b8c1a9d3e");

        var parroquias = new List<Parroquia>
        {
            new Parroquia { Id = Guid.Parse("d4a8f1b2-3c4d-4e5f-9a8b-7c6d5e4f3a2b"), Nombre = "Altagracia", MunicipioId = sucreId },
            new Parroquia { Id = Guid.Parse("e5b9c2d3-4e5f-6a7b-8c9d-0e1f2a3b4c5d"), Nombre = "Santa Inés", MunicipioId = sucreId },
            new Parroquia { Id = Guid.Parse("f6c0d3e4-5f60-7b8c-9d0e-1f2a3b4c5d6e"), Nombre = "Valentín Valiente", MunicipioId = sucreId },
            new Parroquia { Id = Guid.Parse("a1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c6d"), Nombre = "Ayacucho", MunicipioId = sucreId },
            new Parroquia { Id = Guid.Parse("b2c3d4e5-f6a7-8b9c-0d1e-2f3a4b5c6d7e"), Nombre = "San Juan", MunicipioId = sucreId },
            new Parroquia { Id = Guid.Parse("c3d4e5f6-a7b8-9c0d-1e2f-3a4b5c6d7e8f"), Nombre = "Raúl Leoni", MunicipioId = sucreId },
            new Parroquia { Id = Guid.Parse("d4e5f6a7-b8c9-0d1e-2f3a-4b5c6d7e8f9a"), Nombre = "Gran Mariscal", MunicipioId = sucreId }
        };

        foreach (var parroquia in parroquias)
        {
            if (!context.Parroquias.Any(p => p.Nombre == parroquia.Nombre && p.MunicipioId == sucreId))
            {
                context.Parroquias.Add(parroquia);
            }
        }

        context.SaveChanges();
    }
}
