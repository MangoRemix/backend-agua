namespace backend_agua.Models;

public class Municipio
{
    public Guid Id { get; set; }
    public string Nombre { get; set; } = string.Empty;

    // Relación: Un Municipio tiene muchas Parroquias
    public ICollection<Parroquia> Parroquias { get; set; } = new List<Parroquia>();
}