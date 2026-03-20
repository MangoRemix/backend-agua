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

        // Si ya existen comunas, no re-ejecutamos para garantizar integridad
        if (context.Comunas.Any()) return;

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
            "SOCIO PRODUCTIVA Y DE SERVICIO 3 DE FEBRERO MARISCAL SUCRE",
            "COMUNA DE PRODUCCIÓN Y SERVICIO ROBERT SERRA",
            "SOCIOPRODUCTIVA EQUIDAD JUSTICIA SOCIAL SAN LUIS GONZAGA",
            "BOLIVARIANA REVOLUCIONARIA Y SOCIALISTA",
            "SIMON BOLIVAR",
            "SOCIOPRODUCTIVA Y DE SERVICIO ALIANZA REVOLUCIONARIA 2 Y 3",
            "SOCIOPRODUCTIVA EL LEGADO DEL COMANDANTE",
            "COMUNA SOCIO PRODUCTIVA ECOLOGICA TURISTICA Y DE SERVICIO CASCO CENTRAL UNIDO",
            "SOCIO PRODUCTIVA Y DE SERVICIOS BEBEDERO POTENCIA",
            "SOCIOPRODUCTIVA TURISTICA LOS HIJOS DE CHAVEZ",
            "SOCIOPRODUCTIVA DE BIENES Y SERVICIOS MAMÁ ROSA",
            "SOCIALISTA DE PRODUCCIÓN Y SERVICIOS UN NUEVO AMANECER REVOLUCIONARIO ERNESTO CHE GUEVARA",
            "SOCIOPRODUCTIVA LA PATRIA GRANDE DE SUCRE",
            "SOCIOPRODUCTIVA MAISANTA CORAZÓN DE CHÁVEZ",
            "COMUNA URBANA SOCIOPRODUCTIVA SEMILLEROS SOBERANO CANCAMURE",
            "SOCIOPRODUCTIVA COMANDANTE ETERNO POR SIEMPRE",
            "AGROECOLOGICA Y MINERA GRAN MARISCAL DE AYACUCHO",
            "SOCIALISTA DE PRODUCCION INTEGRAL DEL SUR MARISCAL SUCRE",
            "SOCIOPRODUCTIVA Y SERVICIOS TRES PICOS HUGO RAFAEL CHAVEZ FRÍAS"
        });

        // 2. Parroquia Ayacucho (COM021 - COM028)
        AddComunas(comunas, ayId, new[] {
            "SOCIOPRODUCTIVA INTEGRACION GUERRERO DE AYACUCHO",
            "SOCIOPRODUCTIVA TURISTICA Y PESQUERA VENCEDORES EN REVOLUCION DE SAN LUIS",
            "COMUNA SOCIALISTA SOCIOPRODUCTIVA CUMANAGOTO POTENCIA",
            "SOCIALISTA Y PRODUCTIVA LÁZARO HERNÁNDEZ",
            "AGROTURISTICA UNION DE ACTIVOS REPRESENTANTES DE BARBACOAS UDARBA",
            "AGRO-ARTE-TURISMO-PESQUERA VISTA MOCHIMA",
            "AGROTURISTICA Y DE SERVICIO UNIÓN EL TACAL",
            "AGROPECUARIA EZEQUIEL ZAMORA"
        });

        // 3. Parroquia San Juan (COM045 - COM050)
        AddComunas(comunas, sjId, new[] {
            "SOCIALISTA AGROTURÍSTICA SAN JUAN BAUTISTA",
            "SOCIALISTA AGROECOTURISTICA REVOLUCIONARIOS DE GUARANACHE",
            "INTEGRAL BOLIVARIANA EL ULTIMO HOMBRE A CABALLO",
            "EL DESPERTAR DE LOS PUEBLOS BOLIVARIANOS SAN JUAN DE MACARAPANA",
            "AGRO ECOTURÍSTICA Y MINERA RÍO MANZANARES",
            "AGROTURÍSTICA RIBERAS DE TATARACUAL"
        });

        // 4. Parroquia Santa Inés (COM029 - COM037)
        AddComunas(comunas, siId, new[] {
            "CAMPECHE SOMOS TODOS",
            "TURÍSTICA PESQUERA VENCEDORES DE SANTA INÉS",
            "AGROPRODUCTIVA TURISTICA DE SERVICIOS URBANA CENTRO HISTORICO",
            "AGRO-MINERA PRODUCTIVA Y DE SERVICIOS BOCA DE SABANA SOMOS TODOS",
            "AGRO-PRODUCTIVA Y DE SERVICIOS SUCRE POTENCIA",
            "AGRO-TURISTICA-MINERA NELLYS CALLES",
            "COMUNA DE SERVICIOS TURISTICOS AGROURBANA COMANDANTE ETERNO",
            "AGROTURISTICA Y DE SERVICIO LAS CATACUMBAS DE SANTA INES",
            "BOLIVARIANA SOCIALISTA AGROTURÍSTICA PRODUCTIVA EL ARBOL DE LAS 3 RAICES"
        });

        // 5. Parroquia Valentín Valiente (COM038 - COM044)
        AddComunas(comunas, vvId, new[] {
            "COMUNA SOCIO PRODUCTIVA GUERREROS EN VALENTIN GARCIA",
            "PUEBLO NUEVO",
            "COMUNA DE PRODUCCION INTEGRAL MARIA RODRIGUEZ",
            "SOCIOPRODUCTIVA BATALLADORES DE CAIGUIRE",
            "SOCIOPRODUCTIVA EL PEÑON ESFUERZO DE CHAVEZ",
            "SOCIOPRODUCTIVA BRISAS DEL GOLFO",
            "PRODUCTIVA INTEGRAL CACIQUE CAYAURIMA"
        });

        // 6. Parroquia Gran Mariscal (COM051 - COM055, COM061*)
        AddComunas(comunas, gmId, new[] {
            "INDIGENA KARIÑA AGROTURISTICA GRAN MARISCAL DE LOS ALTOS DE SUCRE",
            "AGRICOLA KARIÑA FRANJA DEL TURIMIQUIRE", // COM052
            "SOCIOPRODUCTIVA AGROTURISTICA UNIDOS DEL EJE COSTERO",
            "SOCIOPRODUCTIVA AGRÍCOLA TURÍSTICA DE PESCA PLAYA COLORADA",
            "AGROTURISTICA KARIÑA AMIGOS DE LA MONTAÑA",
            "AGROPRODUCTIVA INDÍGENA KARIÑA SERRANÍAS DEL TURIMIQUIRE" // COM061
        });

        // 7. Parroquia Raúl Leoni (COM056 - COM060, COM062 - COM064)
        // Nota: COM052 y COM061 ya se agregaron en Gran Mariscal según la lista duplicada
        AddComunas(comunas, rlId, new[] {
            "INDÍGENA KARIÑA COSTERA PESQUERA COMERCIAL TURÍSTICA", // COM056
            "COMUNA INDIGENA APACUANA", // COM057
            "AGROTURISTICA PESQUERA KARIÑA CACIQUE MARAGUEY", // COM058
            "INDIGENA AGROTURISTICA LA UNION DE SAN PEDRITO", // COM059
            "AGROTURISTICA PESQUERA INDIGENAS DE NURUCUAL", // COM060
            "LOS VALLES KARIÑAS DE LAS SERRANIAS DEL SILENCIO", // COM062
            "COMUNA INDIGENA KARIÑA AGROTURISTICA PESQUERA Y TRANSPORTE GUERRERA Y GUERREROS DE CHAVEZ", // COM063
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
