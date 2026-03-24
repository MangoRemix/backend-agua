using System.ComponentModel.DataAnnotations;

namespace backend_agua.Dtos.Reporte;

public class ReporteSaludUpdateDto
{
    public bool TieneDiarrea { get; set; }
    public int CantidadCasosDiarrea { get; set; }
    
    public bool TieneVomitos { get; set; }
    public int CantidadCasosVomitos { get; set; }
    
    public bool TieneDolorAbdominal { get; set; }
    public int CantidadCasosDolorAbdominal { get; set; }

    public List<PersonaAfectadaDto> PersonasAfectadas { get; set; } = new();
}
