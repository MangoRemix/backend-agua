using System.ComponentModel.DataAnnotations;

namespace backend_agua.Models;

public class PersonaAfectada
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    public Guid ReporteSaludId { get; set; }
    public ReporteSalud ReporteSalud { get; set; } = null!;

    [Required]
    public string Nombre { get; set; } = string.Empty;
    
    [Required]
    public string Apellido { get; set; } = string.Empty;
    
    public int Edad { get; set; }
    
    public string? Cedula { get; set; }

    public List<CondicionSalud> Condiciones { get; set; } = new();
}
