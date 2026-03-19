using backend_agua.Models;

namespace backend_agua.Dtos.Usuario;

public class UsuarioDto
{
    public Guid Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string Apellido { get; set; } = string.Empty;
    public string Cedula { get; set; } = string.Empty;
    public string? Email { get; set; }
    public string? Telefono { get; set; }
    public UsuarioRol Rol { get; set; }
    public StatusUsuario Status { get; set; }
    public Guid? ComunidadId { get; set; }
    public string? ComunidadNombre { get; set; }
}
