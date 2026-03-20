using backend_agua.Models;

namespace backend_agua.Models;

public class Usuario
{
    public Guid Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string Apellido { get; set; } = string.Empty;
    public string Cedula { get; set; } = string.Empty;
    public string? Email { get; set; }
    public string? Telefono { get; set; }
    public string Password { get; set; } = string.Empty;
    public UsuarioRol Rol { get; set; }
    public StatusUsuario Status { get; set; }

    // Relación opcional: territorial (un usuario puede ser admin de comunidad, comuna o parroquia)
    public Guid? ComunidadId { get; set; }
    public virtual Comunidad? Comunidad { get; set; }

    public Guid? ComunaId { get; set; }
    public virtual Comuna? Comuna { get; set; }

    public Guid? ParroquiaId { get; set; }
    public virtual Parroquia? Parroquia { get; set; }
}