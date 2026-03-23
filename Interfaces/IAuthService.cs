using backend_agua.Dtos.Auth;
using backend_agua.Dtos.Usuario;

namespace backend_agua.Interfaces;

public interface IAuthService
{
    Task<AuthResponseDto?> LoginAsync(LoginDto loginDto);
    Task LogoutAsync();
    string GenerateJwtToken(UsuarioDto usuario);
}
