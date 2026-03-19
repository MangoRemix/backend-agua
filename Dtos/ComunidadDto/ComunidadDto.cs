namespace backend_agua.Dtos.Comunidad;

public class ComunidadDto
{
    public Guid Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public Guid ComunaId { get; set; }
    public string? ComunaNombre { get; set; }
}
