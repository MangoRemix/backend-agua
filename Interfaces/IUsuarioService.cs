using backend_agua.Dtos.Usuario;

namespace backend_agua.Interfaces;

public interface IUsuarioService
{
    Task<IEnumerable<UsuarioDto>> GetAllAsync();
    Task<UsuarioDto?> GetByIdAsync(Guid id);
    Task<UsuarioDto> CreateAsync(UsuarioCreateDto createDto);
    Task<UsuarioDto?> UpdateAsync(Guid id, UsuarioUpdateDto updateDto);
    Task<bool> DeleteAsync(Guid id);
}
