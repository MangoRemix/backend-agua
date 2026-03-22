namespace backend_agua.Dtos.Comunidad;

public class ComunidadDto
{
    public Guid Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string? LiderNombre { get; set; }
    public string? LiderCedula { get; set; }
    public string? LiderTlf { get; set; }
    public Guid ComunaId { get; set; }
    public string? ComunaNombre { get; set; }
}
