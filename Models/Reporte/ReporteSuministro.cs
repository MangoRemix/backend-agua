using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend_agua.Models;

public class ReporteSuministro
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    public Guid ReporteId { get; set; }
    public Reporte Reporte { get; set; } = null!;

    public bool LlegaPorTuberia { get; set; }
    public TimeSpan? HorasSuministro { get; set; }
    public CaudalAgua? Caudal { get; set; }

    public bool RecibeCisterna { get; set; }
    public int? LitrosCisterna { get; set; }
    public TipoCisterna? TipoCisterna { get; set; }

    public bool TieneTanque { get; set; }
    public TipoTanque? TipoTanque { get; set; }

    public int FamiliasBeneficiadas { get; set; }
    
    public int? ApoyoAdicionalLitros { get; set; }
}
