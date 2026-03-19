namespace backend_agua.Models;

public class Comuna
{
    public Guid Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string? LiderNombre { get; set; }
    public string? LiderCedula { get; set; }

    // Relación: Una Comuna pertenece a una Parroquia
    public Guid ParroquiaId { get; set; }
    public Parroquia Parroquia { get; set; } = null!;

    // Relación: Una Comuna tiene muchas Comunidades
    public ICollection<Comunidad> Comunidades { get; set; } = new List<Comunidad>();
}