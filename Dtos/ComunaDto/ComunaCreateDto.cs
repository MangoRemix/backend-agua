using System.ComponentModel.DataAnnotations;

namespace backend_agua.Dtos.Comuna;

public class ComunaCreateDto
{
    [Required]
    public string Nombre { get; set; } = string.Empty;

    public string? LiderNombre { get; set; }
    public string? LiderCedula { get; set; }
    public string? LiderTlf { get; set; }

    [Required]
    public Guid ParroquiaId { get; set; }
}
