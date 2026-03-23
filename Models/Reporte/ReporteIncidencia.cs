using System.ComponentModel.DataAnnotations;

namespace backend_agua.Models;

public class ReporteIncidencia
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    public Guid ReporteId { get; set; }
    public Reporte Reporte { get; set; } = null!;

    // Venta Ilegal
    public bool TieneVentaIlegal { get; set; }
    public string? ChoferNombreApellido { get; set; }
    public string? ChoferCedula { get; set; }
    public string? VehiculoMarcaModelo { get; set; }
    public string? VehiculoPlaca { get; set; }
    public string? VehiculoColor { get; set; }

    // Trancas
    public bool TieneTrancas { get; set; }
    public string? TrancaPropiciaNombre { get; set; }
    public string? TrancaLugar { get; set; }
    public TimeSpan? TrancaDuracion { get; set; }

    // Conflictos
    public bool TieneConflictos { get; set; }
    public string? ConflictosExplicacion { get; set; }

    // Fugas
    public bool TieneFugas { get; set; }
    public string? FugaLugar { get; set; }
    public CaudalAgua? FugaTipo { get; set; }
}
