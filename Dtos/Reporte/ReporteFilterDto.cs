using backend_agua.Dtos.Common;

namespace backend_agua.Dtos.Reporte;

public class ReporteFilterDto : PaginationParams
{
    public Guid? ParroquiaId { get; set; }
    public Guid? ComunaId { get; set; }
    public Guid? ComunidadId { get; set; }
    public DateTime? FechaDesde { get; set; }
    public DateTime? FechaHasta { get; set; }
    public bool? IsLeido { get; set; }
}
