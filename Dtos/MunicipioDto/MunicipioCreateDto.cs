using System.ComponentModel.DataAnnotations;

namespace backend_agua.Dtos.Municipio;

public class MunicipioCreateDto
{
    [Required]
    public string Nombre { get; set; } = string.Empty;
}
