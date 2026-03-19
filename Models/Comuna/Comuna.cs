namespace backend_agua.Models;

public class Comuna
{
    public Guid Id { get; set; }
    public string Nombre { get; set; } = string.Empty;

    // Relación: Una Comuna pertenece a una Parroquia
    public Guid ParroquiaId { get; set; }
    public Parroquia Parroquia { get; set; } = null!;
}