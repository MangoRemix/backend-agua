namespace backend_agua.Models;

public class Parroquia
{
    public Guid Id { get; set; }
    public string Nombre { get; set; } = string.Empty;

    public string? LiderNombre { get; set; }
    public string? LiderCedula { get; set; }
    public string? LiderTlf { get; set; }

    // Relación: Una Parroquia pertenece a un Municipio
    public Guid MunicipioId { get; set; }
    public Municipio Municipio { get; set; } = null!;

    // Relación: Una Parroquia tiene muchas Comunas
    public ICollection<Comuna> Comunas { get; set; } = new List<Comuna>();
}