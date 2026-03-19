namespace backend_agua.Dtos.Parroquia;

public class ParroquiaDto
{
    public Guid Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public Guid MunicipioId { get; set; }
    public string? MunicipioNombre { get; set; }
}
