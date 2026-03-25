using backend_agua.Dtos.Common;

namespace backend_agua.Dtos.ComunidadDto;

public class ComunidadFilterDto : PaginationParams
{
    public Guid? ComunaId { get; set; }
    public Guid? ComunidadId { get; set; }
    public string? CedulaLider { get; set; }
    public string? TlfLider { get; set; }
    public string? NombreLider { get; set; }
    public bool Paginar { get; set; } = true;
}
