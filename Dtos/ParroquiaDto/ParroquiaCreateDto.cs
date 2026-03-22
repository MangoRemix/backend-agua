using System.ComponentModel.DataAnnotations;

namespace backend_agua.Dtos.Parroquia;

public class ParroquiaCreateDto
{
    [Required]
    public string Nombre { get; set; } = string.Empty;

    [Required]
    public Guid MunicipioId { get; set; }

    public string? LiderNombre { get; set; }
    public string? LiderCedula { get; set; }
    public string? LiderTlf { get; set; }
}
