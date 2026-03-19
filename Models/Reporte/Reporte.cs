using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend_agua.Models;

public class Reporte
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    public Guid ComunidadId { get; set; }
    public Comunidad Comunidad { get; set; } = null!;

    [Required]
    public Guid UsuarioId { get; set; }
    public Usuario Usuario { get; set; } = null!;

    public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;
    
    [Required]
    public EstatusReporte Estatus { get; set; } = EstatusReporte.Borrador;

    // --- PASO 1: SUMINISTRO DE AGUA ---
    public bool LlegaPorTuberia { get; set; }
    public int? HorasSuministro { get; set; }
    public CaudalAgua? Caudal { get; set; }

    public bool RecibeCisterna { get; set; }
    public int? LitrosCisterna { get; set; }
    public TipoCisterna? TipoCisterna { get; set; }

    public bool TieneTanque { get; set; }
    public TipoTanque? TipoTanque { get; set; }

    public int FamiliasBeneficiadas { get; set; }
    
    public int? ApoyoAdicionalLitros { get; set; } // Opcional

    // --- PASO 2: MONITOREO DE INCIDENCIAS ---
    
    // Venta Ilegal
    public bool TieneVentaIlegal { get; set; }
    public string? ChoferNombreApellido { get; set; }
    public string? ChoferCedula { get; set; }
    public string? VehiculoMarcaModelo { get; set; }
    public string? VehiculoPlaca { get; set; }
    public string? VehiculoColor { get; set; }

    // Trancas
    public bool TieneTrancas { get; set; }
    public string? TrancaPropiciaNombre { get; set; }
    public string? TrancaLugar { get; set; }
    public string? TrancaDuracion { get; set; }

    // Conflictos
    public bool TieneConflictos { get; set; }
    public string? ConflictosExplicacion { get; set; }

    // Fugas
    public bool TieneFugas { get; set; }
    public string? FugaLugar { get; set; }
    public CaudalAgua? FugaTipo { get; set; } // Reutilizamos CaudalAgua (Fuerte/Debil)

    // --- PASO 3: MONITOREO DE SALUD ---
    public bool TieneDiarrea { get; set; }
    public int CantidadCasosDiarrea { get; set; }
    
    public bool TieneVomitos { get; set; }
    
    public bool TieneDolorAbdominal { get; set; }

    public ICollection<PersonaAfectada> PersonasAfectadas { get; set; } = new List<PersonaAfectada>();

    // --- PASO 4: PARTICIPACIÓN TERRITORIAL ---
    
    // Partido Político
    public bool TienePartido { get; set; }
    public PartidoPolitico? Partido { get; set; }

    // Gobierno
    public bool TieneAlcaldia { get; set; }
    public string? DetalleAlcaldia { get; set; }

    public bool TieneGobernacion { get; set; }
    public string? DetalleGobernacion { get; set; }

    public bool TieneInstitucionNacional { get; set; }
    public string? DetalleInstitucionNacional { get; set; }
}
