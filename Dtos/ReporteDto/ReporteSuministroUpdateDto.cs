using System.ComponentModel.DataAnnotations;
using backend_agua.Models;

namespace backend_agua.Dtos.Reporte;

public class ReporteSuministroUpdateDto
{
    public bool LlegaPorTuberia { get; set; }
    public TimeSpan? HorasSuministro { get; set; }
    public CaudalAgua? Caudal { get; set; }

    public bool RecibeCisterna { get; set; }
    public List<CisternaUpdateDto> Cisternas { get; set; } = new();

    public bool TieneTanque { get; set; }
    public List<TanqueUpdateDto> Tanques { get; set; } = new();

    [Required]
    public int FamiliasBeneficiadas { get; set; }
    
    public int? ApoyoAdicionalLitros { get; set; }
}

public class CisternaUpdateDto
{
    public Guid? Id { get; set; }
    public int Litros { get; set; }
    public TipoCisterna Tipo { get; set; }
}

public class TanqueUpdateDto
{
    public Guid? Id { get; set; }
    public TipoTanque Tipo { get; set; }
}
