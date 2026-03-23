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
    public EstatusReporte Estatus { get; set; } = EstatusReporte.Incompleto;

    // --- PROPIEDADES DE NAVEGACIÓN MODULARES ---
    public virtual ReporteSuministro Suministro { get; set; } = null!;
    public virtual ReporteIncidencia Incidencia { get; set; } = null!;
    public virtual ReporteSalud Salud { get; set; } = null!;
    public virtual ReporteParticipacion Participacion { get; set; } = null!;
}
