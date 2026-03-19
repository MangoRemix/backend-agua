using System.ComponentModel.DataAnnotations;

namespace backend_agua.Dtos.Comuna;

public class ComunaCreateDto
{
    [Required]
    public string Nombre { get; set; } = string.Empty;

    [Required]
    public Guid ParroquiaId { get; set; }
}
