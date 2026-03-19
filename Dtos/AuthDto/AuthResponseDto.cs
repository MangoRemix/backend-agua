using backend_agua.Dtos.Usuario;

namespace backend_agua.Dtos.Auth;

public class AuthResponseDto
{
    public string Token { get; set; } = string.Empty;
    public UsuarioDto Usuario { get; set; } = null!;
}
