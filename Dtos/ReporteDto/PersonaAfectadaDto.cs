using backend_agua.Models;

namespace backend_agua.Dtos.Reporte;

public class PersonaAfectadaDto
{
    public string Nombre { get; set; } = string.Empty;
    public string Apellido { get; set; } = string.Empty;
    public int Edad { get; set; }
    public string? Cedula { get; set; }
    public CondicionSalud Condicion { get; set; }
}
