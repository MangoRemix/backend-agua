using backend_agua.Dtos.Parroquia;

namespace backend_agua.Interfaces;

public interface IParroquiaService
{
    Task<IEnumerable<ParroquiaDto>> GetAllAsync();
    Task<ParroquiaDto?> GetByIdAsync(Guid id);
    Task<IEnumerable<ParroquiaDto>> GetByMunicipioIdAsync(Guid municipioId);
    Task<ParroquiaDto> CreateAsync(ParroquiaCreateDto createDto);
    Task<ParroquiaDto?> UpdateAsync(Guid id, ParroquiaCreateDto updateDto);
    Task<bool> DeleteAsync(Guid id);
}
