using System.ComponentModel.DataAnnotations;

namespace backend_agua.Dtos.Usuario;

public class CambioPasswordDto
{
    [Required(ErrorMessage = "La contraseña actual es requerida")]
    public string ActualPassword { get; set; } = string.Empty;

    [Required(ErrorMessage = "La nueva contraseña es requerida")]
    [MinLength(6, ErrorMessage = "La contraseña debe tener al menos 6 caracteres")]
    public string NuevaPassword { get; set; } = string.Empty;
}
