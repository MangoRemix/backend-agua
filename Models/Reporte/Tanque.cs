using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend_agua.Models;

public class Tanque
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    public Guid ReporteSuministroId { get; set; }
    
    [ForeignKey("ReporteSuministroId")]
    public ReporteSuministro ReporteSuministro { get; set; } = null!;

    [Required]
    public TipoTanque Tipo { get; set; }
}
