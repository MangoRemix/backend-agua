using System.ComponentModel.DataAnnotations;

namespace backend_agua.Models;

public class Fuga
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    public Guid ReporteIncidenciaId { get; set; }
    public ReporteIncidencia ReporteIncidencia { get; set; } = null!;

    public string? Lugar { get; set; }
    public CaudalAgua? Tipo { get; set; }
}
