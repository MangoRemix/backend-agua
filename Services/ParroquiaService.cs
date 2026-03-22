using backend_agua.Dtos.Parroquia;
using backend_agua.Infraestructure.Database;
using backend_agua.Interfaces;
using backend_agua.Models;
using Microsoft.EntityFrameworkCore;

namespace backend_agua.Services;

public class ParroquiaService : IParroquiaService
{
    private readonly ApplicationDbContext _context;

    public ParroquiaService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ParroquiaDto>> GetAllAsync()
    {
        var parroquias = await _context.Parroquias
            .Include(p => p.Municipio)
            .ToListAsync();

        return parroquias.Select(p => new ParroquiaDto
        {
            Id = p.Id,
            Nombre = p.Nombre,
            MunicipioId = p.MunicipioId,
            MunicipioNombre = p.Municipio.Nombre,
            LiderNombre = p.LiderNombre,
            LiderCedula = p.LiderCedula,
            LiderTlf = p.LiderTlf
        });
    }

    public async Task<ParroquiaDto?> GetByIdAsync(Guid id)
    {
        var parroquia = await _context.Parroquias
            .Include(p => p.Municipio)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (parroquia == null) return null;

        return new ParroquiaDto
        {
            Id = parroquia.Id,
            Nombre = parroquia.Nombre,
            MunicipioId = parroquia.MunicipioId,
            MunicipioNombre = parroquia.Municipio.Nombre,
            LiderNombre = parroquia.LiderNombre,
            LiderCedula = parroquia.LiderCedula,
            LiderTlf = parroquia.LiderTlf
        };
    }

    public async Task<IEnumerable<ParroquiaDto>> GetByMunicipioIdAsync(Guid municipioId)
    {
        var parroquias = await _context.Parroquias
            .Include(p => p.Municipio)
            .Where(p => p.MunicipioId == municipioId)
            .ToListAsync();

        return parroquias.Select(p => new ParroquiaDto
        {
            Id = p.Id,
            Nombre = p.Nombre,
            MunicipioId = p.MunicipioId,
            MunicipioNombre = p.Municipio.Nombre,
            LiderNombre = p.LiderNombre,
            LiderCedula = p.LiderCedula,
            LiderTlf = p.LiderTlf
        });
    }

    public async Task<ParroquiaDto> CreateAsync(ParroquiaCreateDto createDto)
    {
        var parroquia = new Parroquia
        {
            Id = Guid.NewGuid(),
            Nombre = createDto.Nombre,
            MunicipioId = createDto.MunicipioId,
            LiderNombre = createDto.LiderNombre,
            LiderCedula = createDto.LiderCedula,
            LiderTlf = createDto.LiderTlf
        };

        _context.Parroquias.Add(parroquia);
        await _context.SaveChangesAsync();

        // Recargar para obtener el nombre del municipio
        var created = await GetByIdAsync(parroquia.Id);
        return created!;
    }

    public async Task<ParroquiaDto?> UpdateAsync(Guid id, ParroquiaCreateDto updateDto)
    {
        var parroquia = await _context.Parroquias.FindAsync(id);
        if (parroquia == null) return null;

        parroquia.Nombre = updateDto.Nombre;
        parroquia.MunicipioId = updateDto.MunicipioId;
        parroquia.LiderNombre = updateDto.LiderNombre;
        parroquia.LiderCedula = updateDto.LiderCedula;
        parroquia.LiderTlf = updateDto.LiderTlf;

        await _context.SaveChangesAsync();

        return await GetByIdAsync(parroquia.Id);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var parroquia = await _context.Parroquias.FindAsync(id);
        if (parroquia == null) return false;

        _context.Parroquias.Remove(parroquia);
        await _context.SaveChangesAsync();
        return true;
    }
}
