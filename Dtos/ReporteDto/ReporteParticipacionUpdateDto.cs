using backend_agua.Models;

namespace backend_agua.Dtos.Reporte;

public class ReporteParticipacionUpdateDto
{
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
