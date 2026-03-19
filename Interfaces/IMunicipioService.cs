using backend_agua.Dtos.Municipio;

namespace backend_agua.Interfaces;

public interface IMunicipioService
{
    Task<IEnumerable<MunicipioDto>> GetAllAsync();
    Task<MunicipioDto?> GetByIdAsync(Guid id);
    Task<MunicipioDto> CreateAsync(MunicipioCreateDto createDto);
    Task<MunicipioDto?> UpdateAsync(Guid id, MunicipioCreateDto updateDto);
    Task<bool> DeleteAsync(Guid id);
}
