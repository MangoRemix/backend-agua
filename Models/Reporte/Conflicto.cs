using System.ComponentModel.DataAnnotations;

namespace backend_agua.Models;

public class Conflicto
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    public Guid ReporteIncidenciaId { get; set; }
    public ReporteIncidencia ReporteIncidencia { get; set; } = null!;

    public string? Explicacion { get; set; }
}
