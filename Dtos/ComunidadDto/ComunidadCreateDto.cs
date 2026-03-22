using System.ComponentModel.DataAnnotations;

namespace backend_agua.Dtos.Comunidad;

public class ComunidadCreateDto
{
    [Required]
    public string Nombre { get; set; } = string.Empty;

    public string? LiderNombre { get; set; }
    public string? LiderCedula { get; set; }
    public string? LiderTlf { get; set; }

    [Required]
    public Guid ComunaId { get; set; }
}
