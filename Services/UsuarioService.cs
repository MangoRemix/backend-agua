using backend_agua.Dtos.Usuario;
using backend_agua.Infraestructure.Database;
using backend_agua.Interfaces;
using backend_agua.Models;
using Microsoft.EntityFrameworkCore;

namespace backend_agua.Services;

public class UsuarioService : IUsuarioService
{
    private readonly ApplicationDbContext _context;

    public UsuarioService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<UsuarioDto>> GetAllAsync()
    {
        var usuarios = await _context.Usuarios.ToListAsync();
        return usuarios.Select(MapToDto);
    }

    public async Task<UsuarioDto?> GetByIdAsync(Guid id)
    {
        var usuario = await _context.Usuarios.FindAsync(id);
        if (usuario == null) return null;

        return MapToDto(usuario);
    }

    public async Task<UsuarioDto> CreateAsync(UsuarioCreateDto createDto)
    {
        var usuario = new Usuario
        {
            Id = Guid.NewGuid(),
            Nombre = createDto.Nombre,
            Apellido = createDto.Apellido,
            Cedula = createDto.Cedula,
            Email = createDto.Email,
            Telefono = createDto.Telefono,
            Password = BCrypt.Net.BCrypt.HashPassword(createDto.Password),
            Rol = createDto.Rol,
            Status = createDto.Status
        };

        _context.Usuarios.Add(usuario);
        await _context.SaveChangesAsync();

        return MapToDto(usuario);
    }

    public async Task<UsuarioDto?> UpdateAsync(Guid id, UsuarioUpdateDto updateDto)
    {
        var usuario = await _context.Usuarios.FindAsync(id);
        if (usuario == null) return null;

        usuario.Nombre = updateDto.Nombre;
        usuario.Apellido = updateDto.Apellido;
        usuario.Cedula = updateDto.Cedula;
        usuario.Email = updateDto.Email;
        usuario.Telefono = updateDto.Telefono;
        usuario.Rol = updateDto.Rol;
        usuario.Status = updateDto.Status;

        if (!string.IsNullOrEmpty(updateDto.Password))
        {
            usuario.Password = BCrypt.Net.BCrypt.HashPassword(updateDto.Password);
        }

        await _context.SaveChangesAsync();

        return MapToDto(usuario);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var usuario = await _context.Usuarios.FindAsync(id);
        if (usuario == null) return false;

        _context.Usuarios.Remove(usuario);
        await _context.SaveChangesAsync();
        return true;
    }

    private static UsuarioDto MapToDto(Usuario usuario)
    {
        return new UsuarioDto
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
    }
}
