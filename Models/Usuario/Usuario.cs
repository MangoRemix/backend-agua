namespace backend_agua.Models;

public class Usuario
{
    public Guid Id { get; set; }
    public string Nombre { get; set; }
    public string Apellido { get; set; }
    public string Cedula { get; set; }
    public string? Email { get; set; }
    public string? Telefono { get; set; }
    public string Password { get; set; }
    public UsuarioRol Rol { get; set; }
    public StatusUsuario Status { get; set; }

    // Relación opcional: Un usuario puede pertenecer a una comunidad
    public Guid? ComunidadId { get; set; }
    public Comunidad? Comunidad { get; set; }
}