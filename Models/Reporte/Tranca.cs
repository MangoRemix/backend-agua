using System.ComponentModel.DataAnnotations;

namespace backend_agua.Models;

public class Tranca
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    public Guid ReporteIncidenciaId { get; set; }
    public ReporteIncidencia ReporteIncidencia { get; set; } = null!;

    public string? PropiciaNombre { get; set; }
    public string? Lugar { get; set; }
    public TimeSpan? Duracion { get; set; }
}
