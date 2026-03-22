using backend_agua.Dtos.Reporte;

namespace backend_agua.Interfaces;

public interface IReporteService
{
    Task<ReporteStatusDto> GetReporteStatusAsync();
    Task<ReporteDto> IniciarReporteAsync(Guid comunidadId, Guid usuarioId);
    Task<ReporteDto?> UpdateSuministroAsync(Guid reporteId, ReporteSuministroUpdateDto updateDto);
    Task<ReporteDto?> UpdateIncidenciasAsync(Guid reporteId, ReporteIncidenciasUpdateDto updateDto);
    Task<ReporteDto?> UpdateSaludAsync(Guid reporteId, ReporteSaludUpdateDto updateDto);
    Task<ReporteDto?> UpdateParticipacionAsync(Guid reporteId, ReporteParticipacionUpdateDto updateDto);
    Task<ReporteDto?> GetByIdAsync(Guid id);
    Task<IEnumerable<ReporteDto>> GetByComunidadAsync(Guid comunidadId);
}
