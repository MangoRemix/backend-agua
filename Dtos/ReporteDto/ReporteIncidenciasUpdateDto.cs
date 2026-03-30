using backend_agua.Models;

namespace backend_agua.Dtos.Reporte;

public class ReporteIncidenciasUpdateDto
{
    // Venta Ilegal
    public bool TieneVentaIlegal { get; set; }
    public string? ChoferNombreApellido { get; set; }
    public string? ChoferCedula { get; set; }
    public string? VehiculoMarcaModelo { get; set; }
    public string? VehiculoPlaca { get; set; }
    public string? VehiculoColor { get; set; }

    // Trancas
    public bool TieneTrancas { get; set; }
    public List<TrancaDto> Trancas { get; set; } = new();

    // Conflictos
    public bool TieneConflictos { get; set; }
    public List<ConflictoDto> Conflictos { get; set; } = new();

    // Fugas
    public bool TieneFugas { get; set; }
    public List<FugaDto> Fugas { get; set; } = new();
}

public class TrancaDto
{
    public Guid? Id { get; set; }
    public string? PropiciaNombre { get; set; }
    public string? Lugar { get; set; }
    public TimeSpan? Duracion { get; set; }
}

public class ConflictoDto
{
    public Guid? Id { get; set; }
    public string? Explicacion { get; set; }
}

public class FugaDto
{
    public Guid? Id { get; set; }
    public string? Lugar { get; set; }
    public CaudalAgua? Tipo { get; set; }
}
