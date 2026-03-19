using backend_agua.Dtos.Municipio;
using backend_agua.Infraestructure.Database;
using backend_agua.Interfaces;
using backend_agua.Models;
using Microsoft.EntityFrameworkCore;

namespace backend_agua.Services;

public class MunicipioService : IMunicipioService
{
    private readonly ApplicationDbContext _context;

    public MunicipioService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<MunicipioDto>> GetAllAsync()
    {
        var municipios = await _context.Municipios.ToListAsync();
        return municipios.Select(m => new MunicipioDto
        {
            Id = m.Id,
            Nombre = m.Nombre
        });
    }

    public async Task<MunicipioDto?> GetByIdAsync(Guid id)
    {
        var municipio = await _context.Municipios.FindAsync(id);
        if (municipio == null) return null;

        return new MunicipioDto
        {
            Id = municipio.Id,
            Nombre = municipio.Nombre
        };
    }

    public async Task<MunicipioDto> CreateAsync(MunicipioCreateDto createDto)
    {
        var municipio = new Municipio
        {
            Id = Guid.NewGuid(),
            Nombre = createDto.Nombre
        };

        _context.Municipios.Add(municipio);
        await _context.SaveChangesAsync();

        return new MunicipioDto
        {
            Id = municipio.Id,
            Nombre = municipio.Nombre
        };
    }

    public async Task<MunicipioDto?> UpdateAsync(Guid id, MunicipioCreateDto updateDto)
    {
        var municipio = await _context.Municipios.FindAsync(id);
        if (municipio == null) return null;

        municipio.Nombre = updateDto.Nombre;

        await _context.SaveChangesAsync();

        return new MunicipioDto
        {
            Id = municipio.Id,
            Nombre = municipio.Nombre
        };
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var municipio = await _context.Municipios.FindAsync(id);
        if (municipio == null) return false;

        _context.Municipios.Remove(municipio);
        await _context.SaveChangesAsync();
        return true;
    }
}
