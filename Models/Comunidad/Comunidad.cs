namespace backend_agua.Models;

public class Comunidad
{
    public Guid Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string? LiderNombre { get; set; }
    public string? LiderCedula { get; set; }
    public string? LiderTlf { get; set; }

    // Relación: Una Comunidad pertenece a una Comuna
    public Guid ComunaId { get; set; }
    public Comuna Comuna { get; set; } = null!;

    // Relación: Una Comunidad tiene muchos Usuarios y Reportes
    public ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
}