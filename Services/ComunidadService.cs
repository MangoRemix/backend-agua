using backend_agua.Dtos.Comunidad;
using backend_agua.Infraestructure.Database;
using backend_agua.Interfaces;
using backend_agua.Models;
using Microsoft.EntityFrameworkCore;

namespace backend_agua.Services;

public class ComunidadService : IComunidadService
{
    private readonly ApplicationDbContext _context;

    public ComunidadService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ComunidadDto>> GetAllAsync()
    {
        var comunidades = await _context.Comunidades
            .Include(c => c.Comuna)
            .ToListAsync();

        return comunidades.Select(c => new ComunidadDto
        {
            Id = c.Id,
            Nombre = c.Nombre,
            LiderNombre = c.LiderNombre,
            LiderCedula = c.LiderCedula,
            LiderTlf = c.LiderTlf,
            ComunaId = c.ComunaId,
            ComunaNombre = c.Comuna.Nombre
        });
    }

    public async Task<ComunidadDto?> GetByIdAsync(Guid id)
    {
        var comunidad = await _context.Comunidades
            .Include(c => c.Comuna)
            .FirstOrDefaultAsync(c => c.Id == id);

        if (comunidad == null) return null;

        return new ComunidadDto
        {
            Id = comunidad.Id,
            Nombre = comunidad.Nombre,
            LiderNombre = comunidad.LiderNombre,
            LiderCedula = comunidad.LiderCedula,
            LiderTlf = comunidad.LiderTlf,
            ComunaId = comunidad.ComunaId,
            ComunaNombre = comunidad.Comuna.Nombre
        };
    }

    public async Task<IEnumerable<ComunidadDto>> GetByComunaIdAsync(Guid comunaId)
    {
        var comunidades = await _context.Comunidades
            .Include(c => c.Comuna)
            .Where(c => c.ComunaId == comunaId)
            .ToListAsync();

        return comunidades.Select(c => new ComunidadDto
        {
            Id = c.Id,
            Nombre = c.Nombre,
            LiderNombre = c.LiderNombre,
            LiderCedula = c.LiderCedula,
            LiderTlf = c.LiderTlf,
            ComunaId = c.ComunaId,
            ComunaNombre = c.Comuna.Nombre
        });
    }

    public async Task<ComunidadDto> CreateAsync(ComunidadCreateDto createDto)
    {
        var comunidad = new Comunidad
        {
            Id = Guid.NewGuid(),
            Nombre = createDto.Nombre,
            LiderNombre = createDto.LiderNombre,
            LiderCedula = createDto.LiderCedula,
            LiderTlf = createDto.LiderTlf,
            ComunaId = createDto.ComunaId
        };

        _context.Comunidades.Add(comunidad);
        await _context.SaveChangesAsync();

        return (await GetByIdAsync(comunidad.Id))!;
    }

    public async Task<ComunidadDto?> UpdateAsync(Guid id, ComunidadCreateDto updateDto)
    {
        var comunidad = await _context.Comunidades.FindAsync(id);
        if (comunidad == null) return null;

        comunidad.Nombre = updateDto.Nombre;
        comunidad.LiderNombre = updateDto.LiderNombre;
        comunidad.LiderCedula = updateDto.LiderCedula;
        comunidad.LiderTlf = updateDto.LiderTlf;
        comunidad.ComunaId = updateDto.ComunaId;

        await _context.SaveChangesAsync();

        return await GetByIdAsync(comunidad.Id);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var comunidad = await _context.Comunidades.FindAsync(id);
        if (comunidad == null) return false;

        _context.Comunidades.Remove(comunidad);
        await _context.SaveChangesAsync();
        return true;
    }
}
