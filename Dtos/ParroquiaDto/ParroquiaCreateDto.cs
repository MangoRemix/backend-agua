using System.ComponentModel.DataAnnotations;

namespace backend_agua.Dtos.Parroquia;

public class ParroquiaCreateDto
{
    [Required]
    public string Nombre { get; set; } = string.Empty;

    [Required]
    public Guid MunicipioId { get; set; }
}
