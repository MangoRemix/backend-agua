using backend_agua.Models;
using System.ComponentModel.DataAnnotations;

namespace backend_agua.Dtos.Usuario;

public class UsuarioUpdateDto
{
    [Required]
    public string Nombre { get; set; } = string.Empty;
    
    [Required]
    public string Apellido { get; set; } = string.Empty;
    
    [Required]
    public string Cedula { get; set; } = string.Empty;
    
    [EmailAddress]
    public string? Email { get; set; }
    
    public string? Telefono { get; set; }
    
    public string? Password { get; set; } // Opcional al actualizar
    
    [Required]
    public UsuarioRol Rol { get; set; }
    
    [Required]
    public StatusUsuario Status { get; set; }
    
    public Guid? ComunidadId { get; set; }
}
