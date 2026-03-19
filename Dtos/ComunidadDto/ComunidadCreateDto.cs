using System.ComponentModel.DataAnnotations;

namespace backend_agua.Dtos.Comunidad;

public class ComunidadCreateDto
{
    [Required]
    public string Nombre { get; set; } = string.Empty;

    [Required]
    public Guid ComunaId { get; set; }
}
