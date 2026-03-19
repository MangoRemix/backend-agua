using System.ComponentModel.DataAnnotations;

namespace backend_agua.Models;

public class PersonaAfectada
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    public Guid ReporteId { get; set; }
    public Reporte Reporte { get; set; } = null!;

    [Required]
    public string Nombre { get; set; } = string.Empty;
    
    [Required]
    public string Apellido { get; set; } = string.Empty;
    
    public int Edad { get; set; }
    
    public string Cedula { get; set; } = string.Empty;

    [Required]
    public CondicionSalud Condicion { get; set; }
}
