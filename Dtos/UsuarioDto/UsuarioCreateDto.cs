using backend_agua.Models;
using System.ComponentModel.DataAnnotations;

namespace backend_agua.Dtos.Usuario;

public class UsuarioCreateDto
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
    
    [Required]
    public string Password { get; set; } = string.Empty;
    
    [Required]
    public UsuarioRol Rol { get; set; }
    
    [Required]
    public StatusUsuario Status { get; set; }
}
