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
    public ICollection<Tranca> Trancas { get; set; } = new List<Tranca>();

    // Conflictos
    public bool TieneConflictos { get; set; }
    public ICollection<Conflicto> Conflictos { get; set; } = new List<Conflicto>();

    // Fugas
    public bool TieneFugas { get; set; }
    public ICollection<Fuga> Fugas { get; set; } = new List<Fuga>();
}
