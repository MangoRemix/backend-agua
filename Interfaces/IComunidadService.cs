using backend_agua.Dtos.Comunidad;

namespace backend_agua.Interfaces;

public interface IComunidadService
{
    Task<IEnumerable<ComunidadDto>> GetAllAsync();
    Task<ComunidadDto?> GetByIdAsync(Guid id);
    Task<IEnumerable<ComunidadDto>> GetByComunaIdAsync(Guid comunaId);
    Task<ComunidadDto> CreateAsync(ComunidadCreateDto createDto);
    Task<ComunidadDto?> UpdateAsync(Guid id, ComunidadCreateDto updateDto);
    Task<bool> DeleteAsync(Guid id);
}
