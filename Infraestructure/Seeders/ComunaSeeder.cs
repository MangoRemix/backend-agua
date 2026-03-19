using backend_agua.Infraestructure.Database;
using backend_agua.Models;
using Microsoft.Extensions.DependencyInjection;

namespace backend_agua.Infraestructure.Seeders;

public static class ComunaSeeder
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        // Parish IDs from ParroquiaSeeder
        var alId = Guid.Parse("d4a8f1b2-3c4d-4e5f-9a8b-7c6d5e4f3a2b"); // Altagracia
        var siId = Guid.Parse("e5b9c2d3-4e5f-6a7b-8c9d-0e1f2a3b4c5d"); // Santa Inés
        var vvId = Guid.Parse("f6c0d3e4-5f60-7b8c-9d0e-1f2a3b4c5d6e"); // Valentín Valiente
        var ayId = Guid.Parse("a1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c6d"); // Ayacucho
        var sjId = Guid.Parse("b2c3d4e5-f6a7-8b9c-0d1e-2f3a4b5c6d7e"); // San Juan
        var rlId = Guid.Parse("c3d4e5f6-a7b8-9c0d-1e2f-3a4b5c6d7e8f"); // Raúl Leoni
        var gmId = Guid.Parse("d4e5f6a7-b8c9-0d1e-2f3a-4b5c6d7e8f9a"); // Gran Mariscal

        var comunas = new List<Comuna>();

        // 1. Parroquia Altagracia (COM001 - COM020)
        AddComunas(comunas, alId, new[] {
            "SOCIALISTA DE PRODUCCIÓN Y SERVICIOS LA LUCHA ESFUERZO SOBERANO POR VENEZUELA",
            "SOCIOPRODUCTIVA Y DE SERVICIO 3 DE FEBRERO MARISCAL SUCRE",
            "COMUNA DE PRODUCCIÓN Y SERVICIO ROBERT SERRA",
            "SOCIOPRODUCTIVA EQUIDAD JUSTICIA SOCIAL SAN LUIS GONZAGA",
            "BOLIVARIANA REVOLUCIONARIA Y SOCIALISTA",
            "SIMÓN BOLÍVAR",
            "SOCIOPRODUCTIVA Y DE SERVICIO ALIANZA REVOLUCIONARIA 2 Y 3",
            "SOCIOPRODUCTIVA EL LEGADO DEL COMANDANTE",
            "COMUNA SOCIOPRODUCTIVA ECOLÓGICA TURÍSTICA Y DE SERVICIO CASCO CENTRAL UNIDO",
            "SOCIOPRODUCTIVA Y DE SERVICIOS BEBEDERO POTENCIA",
            "SOCIOPRODUCTIVA TURÍSTICA LOS HIJOS DE CHÁVEZ",
            "SOCIOPRODUCTIVA DE BIENES Y SERVICIOS MAMÁ ROSA",
            "SOCIALISTA DE PRODUCCIÓN Y SERVICIOS UN NUEVO AMANECER REVOLUCIONARIO ERNESTO CHE GUEVARA",
            "SOCIOPRODUCTIVA LA PATRIA GRANDE DE SUCRE",
            "SOCIOPRODUCTIVA MAISANTA CORAZÓN DE CHÁVEZ",
            "COMUNA URBANA SOCIOPRODUCTIVA SEMILLEROS SOBERANO CANCAMURE",
            "SOCIOPRODUCTIVA COMANDANTE ETERNO POR SIEMPRE",
            "AGROECOLÓGICA Y MINERA GRAN MARISCAL DE AYACUCHO",
            "SOCIALISTA DE PRODUCCIÓN INTEGRAL DEL SUR MARISCAL SUCRE",
            "SOCIOPRODUCTIVA Y SERVICIOS TRES PICOS HUGO RAFAEL CHÁVEZ FRÍAS"
        });

        // 2. Parroquia Ayacucho (COM021 - COM028)
        AddComunas(comunas, ayId, new[] {
            "SOCIOPRODUCTIVA INTEGRACIÓN GUERRERO DE AYACUCHO",
            "SOCIOPRODUCTIVA TURÍSTICA Y PESQUERA VENCEDORES EN REVOLUCIÓN DE SAN LUIS",
            "COMUNA SOCIALISTA SOCIOPRODUCTIVA CUMANAGOTO POTENCIA",
            "SOCIALISTA Y PRODUCTIVA LÁZARO HERNÁNDEZ",
            "AGROTURÍSTICA UNIÓN DE ACTIVOS REPRESENTANTES DE BARBACOAS (UDARBA)",
            "AGRO-ARTE-TURISMO-PESQUERA VISTA MOCHIMA",
            "AGROTURÍSTICA Y DE SERVICIO UNIÓN EL TACAL",
            "AGROPECUARIA EZEQUIEL ZAMORA"
        });

        // 3. Parroquia San Juan (COM045 - COM050)
        AddComunas(comunas, sjId, new[] {
            "SOCIALISTA AGROTURÍSTICA SAN JUAN BAUTISTA",
            "SOCIALISTA AGROECOTURÍSTICA REVOLUCIONARIOS DE GUARANACHE",
            "INTEGRAL BOLIVARIANA EL ÚLTIMO HOMBRE A CABALLO",
            "EL DESPERTAR DE LOS PUEBLOS BOLIVARIANOS SAN JUAN DE MACARAPANA",
            "AGROECOTURÍSTICA Y MINERA RÍO MANZANARES",
            "AGROTURÍSTICA RIBERAS DE TATARACUAL"
        });

        // 4. Parroquia Santa Inés (COM029 - COM037)
        AddComunas(comunas, siId, new[] {
            "CAMPECHE SOMOS TODOS",
            "TURÍSTICA PESQUERA VENCEDORES DE SANTA INÉS",
            "AGROPRODUCTIVA TURÍSTICA DE SERVICIOS URBANA CENTRO HISTÓRICO",
            "AGRO-MINERA PRODUCTIVA Y DE SERVICIOS BOCA DE SABANA SOMOS TODOS",
            "AGROPRODUCTIVA Y DE SERVICIOS SUCRE POTENCIA",
            "AGRO-TURÍSTICA-MINERA NELLYS CALLES",
            "COMUNA DE SERVICIOS TURÍSTICOS AGROURBANA COMANDANTE ETERNO",
            "AGROTURÍSTICA Y DE SERVICIO LAS CATACUMBAS DE SANTA INÉS",
            "BOLIVARIANA SOCIALISTA AGROTURÍSTICA PRODUCTIVA EL ÁRBOL DE LAS 3 RAÍCES"
        });

        // 5. Parroquia Valentín Valiente (COM038 - COM044)
        AddComunas(comunas, vvId, new[] {
            "COMUNA SOCIOPRODUCTIVA GUERREROS EN VALENTÍN GARCÍA",
            "PUEBLO NUEVO",
            "COMUNA DE PRODUCCIÓN INTEGRAL MARÍA RODRÍGUEZ",
            "SOCIOPRODUCTIVA BATALLADORES DE CAIGÜIRE",
            "SOCIOPRODUCTIVA EL PEÑÓN ESFUERZO DE CHÁVEZ",
            "SOCIOPRODUCTIVA BRISAS DEL GOLFO",
            "PRODUCTIVA INTEGRAL CACIQUE CAYAURIMA"
        });

        // 6. Parroquia Gran Mariscal (COM051 - COM055, COM061*)
        AddComunas(comunas, gmId, new[] {
            "INDÍGENA KARIÑA AGROTURÍSTICA GRAN MARISCAL DE LOS ALTOS DE SUCRE",
            "AGRÍCOLA KARIÑA FRANJA DEL TURIMIQUIRE", // COM052
            "SOCIOPRODUCTIVA AGROTURÍSTICA UNIDOS DEL EJE COSTERO",
            "SOCIOPRODUCTIVA AGRÍCOLA TURÍSTICA DE PESCA PLAYA COLORADA",
            "AGROTURÍSTICA KARIÑA AMIGOS DE LA MONTAÑA",
            "AGROPRODUCTIVA INDÍGENA KARIÑA SERRANÍAS DEL TURIMIQUIRE" // COM061
        });

        // 7. Parroquia Raúl Leoni (COM056 - COM060, COM062 - COM064)
        // Nota: COM052 y COM061 ya se agregaron en Gran Mariscal según la lista duplicada
        AddComunas(comunas, rlId, new[] {
            "INDÍGENA KARIÑA COSTERA PESQUERA COMERCIAL TURÍSTICA", // COM056
            "COMUNA INDÍGENA APACUANA", // COM057
            "AGROTURÍSTICA PESQUERA KARIÑA CACIQUE MARAGUEY", // COM058
            "INDÍGENA AGROTURÍSTICA LA UNIÓN DE SAN PEDRITO", // COM059
            "AGROTURÍSTICA PESQUERA INDÍGENAS DE NURUCUAL", // COM060
            "LOS VALLES KARIÑAS DE LAS SERRANÍAS DEL SILENCIO", // COM062
            "COMUNA INDÍGENA KARIÑA AGROTURÍSTICA PESQUERA Y TRANSPORTE GUERRERAS Y GUERREROS DE CHÁVEZ", // COM063
            "AGROPRODUCTIVA INDÍGENA KARIÑA SANTA CRUZ" // COM064
        });

        foreach (var comuna in comunas)
        {
            if (!context.Comunas.Any(c => c.Nombre == comuna.Nombre && c.ParroquiaId == comuna.ParroquiaId))
            {
                context.Comunas.Add(comuna);
            }
        }

        context.SaveChanges();
    }

    private static void AddComunas(List<Comuna> list, Guid parroquiaId, string[] nombres)
    {
        foreach (var nombre in nombres)
        {
            // Generar un Guid determinístico basado en el nombre para mantenerlo "fijo"
            // Para simplicidad en este script, usaré Guid.NewGuid pero el usuario pidió "haslo fijo".
            // Una forma simple de hacerlo "fijo" sin tablas de mapeo gigantes es usar el Hash del nombre.
            var deterministicGuid = CreateGuidFromName(nombre + parroquiaId.ToString());
            list.Add(new Comuna 
            { 
                Id = deterministicGuid, 
                Nombre = nombre, 
                ParroquiaId = parroquiaId 
            });
        }
    }

    private static Guid CreateGuidFromName(string name)
    {
        using (var md5 = System.Security.Cryptography.MD5.Create())
        {
            byte[] hash = md5.ComputeHash(System.Text.Encoding.UTF8.GetBytes(name));
            return new Guid(hash);
        }
    }
}
