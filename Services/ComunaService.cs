using backend_agua.Dtos.Comuna;
using backend_agua.Infraestructure.Database;
using backend_agua.Interfaces;
using backend_agua.Models;
using Microsoft.EntityFrameworkCore;

namespace backend_agua.Services;

public class ComunaService : IComunaService
{
    private readonly ApplicationDbContext _context;

    public ComunaService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ComunaDto>> GetAllAsync()
    {
        var comunas = await _context.Comunas
            .Include(c => c.Parroquia)
            .ToListAsync();

        return comunas.Select(c => new ComunaDto
        {
            Id = c.Id,
            Nombre = c.Nombre,
            LiderNombre = c.LiderNombre,
            LiderCedula = c.LiderCedula,
            ParroquiaId = c.ParroquiaId,
            ParroquiaNombre = c.Parroquia.Nombre
        });
    }

    public async Task<ComunaDto?> GetByIdAsync(Guid id)
    {
        var comuna = await _context.Comunas
            .Include(c => c.Parroquia)
            .FirstOrDefaultAsync(c => c.Id == id);

        if (comuna == null) return null;

        return new ComunaDto
        {
            Id = comuna.Id,
            Nombre = comuna.Nombre,
            LiderNombre = comuna.LiderNombre,
            LiderCedula = comuna.LiderCedula,
            ParroquiaId = comuna.ParroquiaId,
            ParroquiaNombre = comuna.Parroquia.Nombre
        };
    }

    public async Task<IEnumerable<ComunaDto>> GetByParroquiaIdAsync(Guid parroquiaId)
    {
        var comunas = await _context.Comunas
            .Include(c => c.Parroquia)
            .Where(c => c.ParroquiaId == parroquiaId)
            .ToListAsync();

        return comunas.Select(c => new ComunaDto
        {
            Id = c.Id,
            Nombre = c.Nombre,
            ParroquiaId = c.ParroquiaId,
            ParroquiaNombre = c.Parroquia.Nombre
        });
    }

    public async Task<ComunaDto> CreateAsync(ComunaCreateDto createDto)
    {
        var comuna = new Comuna
        {
            Id = Guid.NewGuid(),
            Nombre = createDto.Nombre,
            LiderNombre = createDto.LiderNombre,
            LiderCedula = createDto.LiderCedula,
            ParroquiaId = createDto.ParroquiaId
        };

        _context.Comunas.Add(comuna);
        await _context.SaveChangesAsync();

        return (await GetByIdAsync(comuna.Id))!;
    }

    public async Task<ComunaDto?> UpdateAsync(Guid id, ComunaCreateDto updateDto)
    {
        var comuna = await _context.Comunas.FindAsync(id);
        if (comuna == null) return null;

        comuna.Nombre = updateDto.Nombre;
        comuna.LiderNombre = updateDto.LiderNombre;
        comuna.LiderCedula = updateDto.LiderCedula;
        comuna.ParroquiaId = updateDto.ParroquiaId;

        await _context.SaveChangesAsync();

        return await GetByIdAsync(comuna.Id);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var comuna = await _context.Comunas.FindAsync(id);
        if (comuna == null) return false;

        _context.Comunas.Remove(comuna);
        await _context.SaveChangesAsync();
        return true;
    }
}
