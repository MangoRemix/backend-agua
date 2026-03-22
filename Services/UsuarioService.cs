using backend_agua.Dtos.Usuario;
using backend_agua.Dtos.Common;
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
        var usuarios = await _context.Usuarios
            .Include(u => u.Comunidad)
            .Include(u => u.Comuna)
            .Include(u => u.Parroquia)
            .ToListAsync();
        return usuarios.Select(MapToDto);
    }

    public async Task<PagedResult<UsuarioDto>> GetPagedAsync(UsuarioFilterDto filter)
    {
        var query = _context.Usuarios
            .Include(u => u.Comunidad)
            .Include(u => u.Comuna)
            .Include(u => u.Parroquia)
            .AsQueryable();

        // Filtros
        if (filter.ParroquiaId.HasValue)
            query = query.Where(u => u.ParroquiaId == filter.ParroquiaId);
        
        if (filter.ComunaId.HasValue)
            query = query.Where(u => u.ComunaId == filter.ComunaId);

        if (filter.Status.HasValue)
            query = query.Where(u => u.Status == filter.Status);

        if (filter.Rol.HasValue)
            query = query.Where(u => u.Rol == filter.Rol);

        var totalItems = await query.CountAsync();
        
        var items = await query
            .Skip((filter.PageNumber - 1) * filter.PageSize)
            .Take(filter.PageSize)
            .ToListAsync();

        return new PagedResult<UsuarioDto>
        {
            Items = items.Select(MapToDto),
            TotalItems = totalItems,
            PageNumber = filter.PageNumber,
            PageSize = filter.PageSize
        };
    }

    public async Task<UsuarioDto?> GetByIdAsync(Guid id)
    {
        var usuario = await _context.Usuarios
            .Include(u => u.Comunidad)
            .Include(u => u.Comuna)
            .Include(u => u.Parroquia)
            .FirstOrDefaultAsync(u => u.Id == id);
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
            Status = createDto.Status,
            ComunidadId = createDto.ComunidadId,
            ComunaId = createDto.ComunaId,
            ParroquiaId = createDto.ParroquiaId
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
        usuario.ComunidadId = updateDto.ComunidadId;
        usuario.ComunaId = updateDto.ComunaId;
        usuario.ParroquiaId = updateDto.ParroquiaId;

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
            Status = usuario.Status,
            ComunidadId = usuario.ComunidadId,
            ComunidadNombre = usuario.Comunidad?.Nombre,
            ComunaId = usuario.ComunaId,
            ComunaNombre = usuario.Comuna?.Nombre,
            ParroquiaId = usuario.ParroquiaId,
            ParroquiaNombre = usuario.Parroquia?.Nombre
        };
    }
}
