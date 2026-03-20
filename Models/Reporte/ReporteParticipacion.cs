using System.ComponentModel.DataAnnotations;

namespace backend_agua.Models;

public class ReporteParticipacion
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    public Guid ReporteId { get; set; }
    public Reporte Reporte { get; set; } = null!;

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
