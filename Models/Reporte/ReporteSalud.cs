using System.ComponentModel.DataAnnotations;

namespace backend_agua.Models;

public class ReporteSalud
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    public Guid ReporteId { get; set; }
    public Reporte Reporte { get; set; } = null!;

    public bool TieneDiarrea { get; set; }
    public int CantidadCasosDiarrea { get; set; }
    
    public bool TieneVomitos { get; set; }
    
    public bool TieneDolorAbdominal { get; set; }

    public ICollection<PersonaAfectada> PersonasAfectadas { get; set; } = new List<PersonaAfectada>();
}
