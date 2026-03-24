using backend_agua.Dtos.Common;

namespace backend_agua.Dtos.Reporte;

public class ComunaDashboardDto
{
    public string ComunaNombre { get; set; } = string.Empty;
    public int TotalComunidades { get; set; }
    public int EnviadosHoy { get; set; }
    public int PendientesHoy => TotalComunidades - EnviadosHoy;
    public double PorcentajeGlobal => TotalComunidades > 0 ? (double)EnviadosHoy / TotalComunidades * 100 : 0;
    
    public PagedResult<ComunidadStatusDto> ListadoComunidades { get; set; } = new();
}

public class ComunidadStatusDto
{
    public Guid Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string Status { get; set; } = "PENDIENTE"; // RECIBIDO o PENDIENTE
    public DateTime? UltimaFechaReporte { get; set; }
    public string UltimoReporteTexto { get; set; } = "SIN REPORTES";
}
