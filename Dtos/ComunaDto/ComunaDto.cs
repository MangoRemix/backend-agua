namespace backend_agua.Dtos.Comuna;

public class ComunaDto
{
    public Guid Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string? LiderNombre { get; set; }
    public string? LiderCedula { get; set; }
    public string? LiderTlf { get; set; }
    public Guid ParroquiaId { get; set; }
    public string? ParroquiaNombre { get; set; }
}
