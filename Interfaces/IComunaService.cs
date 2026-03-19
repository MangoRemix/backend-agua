using backend_agua.Dtos.Comuna;

namespace backend_agua.Interfaces;

public interface IComunaService
{
    Task<IEnumerable<ComunaDto>> GetAllAsync();
    Task<ComunaDto?> GetByIdAsync(Guid id);
    Task<IEnumerable<ComunaDto>> GetByParroquiaIdAsync(Guid parroquiaId);
    Task<ComunaDto> CreateAsync(ComunaCreateDto createDto);
    Task<ComunaDto?> UpdateAsync(Guid id, ComunaCreateDto updateDto);
    Task<bool> DeleteAsync(Guid id);
}
