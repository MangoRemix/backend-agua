using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using backend_agua.Dtos.Auth;
using backend_agua.Dtos.Usuario;
using backend_agua.Infraestructure.Database;
using backend_agua.Interfaces;
using backend_agua.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace backend_agua.Services;

public class AuthService : IAuthService
{
    private readonly ApplicationDbContext _context;
    private readonly IConfiguration _configuration;

    public AuthService(ApplicationDbContext context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
    }

    public async Task<AuthResponseDto?> LoginAsync(LoginDto loginDto)
    {
        var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Cedula == loginDto.Cedula);

        if (usuario == null)
            return null; // Usuario no encontrado

        // Verificar la contraseña usando BCrypt
        bool isPasswordValid = BCrypt.Net.BCrypt.Verify(loginDto.Password, usuario.Password);

        if (!isPasswordValid)
            return null; // Contraseña incorrecta

        var usuarioDto = new UsuarioDto
        {
            Id = usuario.Id,
            Nombre = usuario.Nombre,
            Apellido = usuario.Apellido,
            Cedula = usuario.Cedula,
            Email = usuario.Email,
            Telefono = usuario.Telefono,
            Rol = usuario.Rol,
            Status = usuario.Status
        };

        var token = GenerateJwtToken(usuarioDto);

        return new AuthResponseDto
        {
            Token = token,
            Usuario = usuarioDto
        };
    }

    public string GenerateJwtToken(UsuarioDto usuario)
    {
        var jwtSettings = _configuration.GetSection("Jwt");
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]!));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
            new Claim(ClaimTypes.NameIdentifier, usuario.Cedula),
            new Claim(ClaimTypes.GivenName, usuario.Nombre),
            new Claim(ClaimTypes.Surname, usuario.Apellido),
            new Claim(ClaimTypes.Role, usuario.Rol.ToString())
        };

        if (!string.IsNullOrEmpty(usuario.Email))
        {
            claims.Add(new Claim(ClaimTypes.Email, usuario.Email));
        }

        var token = new JwtSecurityToken(
            issuer: jwtSettings["Issuer"],
            audience: jwtSettings["Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddDays(Convert.ToDouble(jwtSettings["ExpireDays"])),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
