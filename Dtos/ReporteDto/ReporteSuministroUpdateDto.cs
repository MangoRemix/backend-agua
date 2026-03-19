using System.ComponentModel.DataAnnotations;
using backend_agua.Models;

namespace backend_agua.Dtos.Reporte;

public class ReporteSuministroUpdateDto
{
    public bool LlegaPorTuberia { get; set; }
    public int? HorasSuministro { get; set; }
    public CaudalAgua? Caudal { get; set; }

    public bool RecibeCisterna { get; set; }
    public int? LitrosCisterna { get; set; }
    public TipoCisterna? TipoCisterna { get; set; }

    public bool TieneTanque { get; set; }
    public TipoTanque? TipoTanque { get; set; }

    [Required]
    public int FamiliasBeneficiadas { get; set; }
    
    public int? ApoyoAdicionalLitros { get; set; }
}
