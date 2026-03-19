using backend_agua.Dtos.Reporte;
using backend_agua.Infraestructure.Database;
using backend_agua.Interfaces;
using backend_agua.Models;
using Microsoft.EntityFrameworkCore;

namespace backend_agua.Services;

public class ReporteService : IReporteService
{
    private readonly ApplicationDbContext _context;

    public ReporteService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ReporteDto> IniciarReporteAsync(Guid comunidadId, Guid usuarioId)
    {
        var reporte = new Reporte
        {
            Id = Guid.NewGuid(),
            ComunidadId = comunidadId,
            UsuarioId = usuarioId,
            FechaCreacion = DateTime.UtcNow,
            Estatus = EstatusReporte.Borrador
        };

        _context.Reportes.Add(reporte);
        await _context.SaveChangesAsync();

        return (await GetByIdAsync(reporte.Id))!;
    }

    public async Task<ReporteDto?> UpdateSuministroAsync(Guid reporteId, ReporteSuministroUpdateDto updateDto)
    {
        var reporte = await _context.Reportes.FindAsync(reporteId);
        if (reporte == null) return null;

        reporte.LlegaPorTuberia = updateDto.LlegaPorTuberia;
        reporte.HorasSuministro = updateDto.LlegaPorTuberia ? updateDto.HorasSuministro : null;
        reporte.Caudal = updateDto.LlegaPorTuberia ? updateDto.Caudal : null;

        reporte.RecibeCisterna = updateDto.RecibeCisterna;
        reporte.LitrosCisterna = updateDto.RecibeCisterna ? updateDto.LitrosCisterna : null;
        reporte.TipoCisterna = updateDto.RecibeCisterna ? updateDto.TipoCisterna : null;

        reporte.TieneTanque = updateDto.TieneTanque;
        reporte.TipoTanque = updateDto.TieneTanque ? updateDto.TipoTanque : null;

        reporte.FamiliasBeneficiadas = updateDto.FamiliasBeneficiadas;
        reporte.ApoyoAdicionalLitros = updateDto.ApoyoAdicionalLitros;

        await _context.SaveChangesAsync();

        return await GetByIdAsync(reporte.Id);
    }

    public async Task<ReporteDto?> UpdateIncidenciasAsync(Guid reporteId, ReporteIncidenciasUpdateDto updateDto)
    {
        var reporte = await _context.Reportes.FindAsync(reporteId);
        if (reporte == null) return null;

        // Venta Ilegal
        reporte.TieneVentaIlegal = updateDto.TieneVentaIlegal;
        reporte.ChoferNombreApellido = updateDto.TieneVentaIlegal ? updateDto.ChoferNombreApellido : null;
        reporte.ChoferCedula = updateDto.TieneVentaIlegal ? updateDto.ChoferCedula : null;
        reporte.VehiculoMarcaModelo = updateDto.TieneVentaIlegal ? updateDto.VehiculoMarcaModelo : null;
        reporte.VehiculoPlaca = updateDto.TieneVentaIlegal ? updateDto.VehiculoPlaca : null;
        reporte.VehiculoColor = updateDto.TieneVentaIlegal ? updateDto.VehiculoColor : null;

        // Trancas
        reporte.TieneTrancas = updateDto.TieneTrancas;
        reporte.TrancaPropiciaNombre = updateDto.TieneTrancas ? updateDto.TrancaPropiciaNombre : null;
        reporte.TrancaLugar = updateDto.TieneTrancas ? updateDto.TrancaLugar : null;
        reporte.TrancaDuracion = updateDto.TieneTrancas ? updateDto.TrancaDuracion : null;

        // Conflictos
        reporte.TieneConflictos = updateDto.TieneConflictos;
        reporte.ConflictosExplicacion = updateDto.TieneConflictos ? updateDto.ConflictosExplicacion : null;

        // Fugas
        reporte.TieneFugas = updateDto.TieneFugas;
        reporte.FugaLugar = updateDto.TieneFugas ? updateDto.FugaLugar : null;
        reporte.FugaTipo = updateDto.TieneFugas ? updateDto.FugaTipo : null;

        await _context.SaveChangesAsync();
        return await GetByIdAsync(reporte.Id);
    }

    public async Task<ReporteDto?> UpdateSaludAsync(Guid reporteId, ReporteSaludUpdateDto updateDto)
    {
        var reporte = await _context.Reportes
            .Include(r => r.PersonasAfectadas)
            .FirstOrDefaultAsync(r => r.Id == reporteId);

        if (reporte == null) return null;

        reporte.TieneDiarrea = updateDto.TieneDiarrea;
        reporte.CantidadCasosDiarrea = updateDto.TieneDiarrea ? updateDto.CantidadCasosDiarrea : 0;
        reporte.TieneVomitos = updateDto.TieneVomitos;
        reporte.TieneDolorAbdominal = updateDto.TieneDolorAbdominal;

        // Limpiar personas afectadas anteriores y agregar las nuevas
        _context.PersonasAfectadas.RemoveRange(reporte.PersonasAfectadas);
        
        foreach (var personaDto in updateDto.PersonasAfectadas)
        {
            // Validar si la persona corresponde a una condición activa
            bool esValida = false;
            if (personaDto.Condicion == CondicionSalud.Diarrea && updateDto.TieneDiarrea) esValida = true;
            if (personaDto.Condicion == CondicionSalud.Vomitos && updateDto.TieneVomitos) esValida = true;
            if (personaDto.Condicion == CondicionSalud.DolorAbdominal && updateDto.TieneDolorAbdominal) esValida = true;

            if (esValida)
            {
                reporte.PersonasAfectadas.Add(new PersonaAfectada
                {
                    Id = Guid.NewGuid(),
                    ReporteId = reporte.Id,
                    Nombre = personaDto.Nombre,
                    Apellido = personaDto.Apellido,
                    Edad = personaDto.Edad,
                    Cedula = personaDto.Cedula,
                    Condicion = personaDto.Condicion
                });
            }
        }

        await _context.SaveChangesAsync();
        return await GetByIdAsync(reporte.Id);
    }

    public async Task<ReporteDto?> UpdateParticipacionAsync(Guid reporteId, ReporteParticipacionUpdateDto updateDto)
    {
        var reporte = await _context.Reportes.FindAsync(reporteId);
        if (reporte == null) return null;

        // Partido
        reporte.TienePartido = updateDto.TienePartido;
        reporte.Partido = updateDto.TienePartido ? updateDto.Partido : null;

        // Alcaldia
        reporte.TieneAlcaldia = updateDto.TieneAlcaldia;
        reporte.DetalleAlcaldia = updateDto.TieneAlcaldia ? updateDto.DetalleAlcaldia : null;

        // Gobernacion
        reporte.TieneGobernacion = updateDto.TieneGobernacion;
        reporte.DetalleGobernacion = updateDto.TieneGobernacion ? updateDto.DetalleGobernacion : null;

        // Institucion Nacional
        reporte.TieneInstitucionNacional = updateDto.TieneInstitucionNacional;
        reporte.DetalleInstitucionNacional = updateDto.TieneInstitucionNacional ? updateDto.DetalleInstitucionNacional : null;

        // El paso 4 es el último paso del stepper, por lo que marcamos el reporte como completado
        reporte.Estatus = EstatusReporte.Completado;

        await _context.SaveChangesAsync();
        return await GetByIdAsync(reporte.Id);
    }

    public async Task<ReporteDto?> GetByIdAsync(Guid id)
    {
        var reporte = await _context.Reportes
            .Include(r => r.Comunidad)
                .ThenInclude(c => c.Comuna)
            .Include(r => r.Usuario)
            .Include(r => r.PersonasAfectadas)
            .FirstOrDefaultAsync(r => r.Id == id);

        if (reporte == null) return null;

        return MapToDto(reporte);
    }

    public async Task<IEnumerable<ReporteDto>> GetByComunidadAsync(Guid comunidadId)
    {
        var reportes = await _context.Reportes
            .Include(r => r.Comunidad)
                .ThenInclude(c => c.Comuna)
            .Include(r => r.Usuario)
            .Where(r => r.ComunidadId == comunidadId)
            .OrderByDescending(r => r.FechaCreacion)
            .ToListAsync();

        return reportes.Select(MapToDto);
    }

    private static ReporteDto MapToDto(Reporte reporte)
    {
        return new ReporteDto
        {
            Id = reporte.Id,
            ComunidadId = reporte.ComunidadId,
            UsuarioId = reporte.UsuarioId,
            UsuarioNombre = $"{reporte.Usuario?.Nombre} {reporte.Usuario?.Apellido}",
            
            ComunaNombre = reporte.Comunidad?.Comuna?.Nombre,
            ComunaLiderNombre = reporte.Comunidad?.Comuna?.LiderNombre,
            ComunaLiderCedula = reporte.Comunidad?.Comuna?.LiderCedula,
            ComunidadNombre = reporte.Comunidad?.Nombre,
            ComunidadLiderNombre = reporte.Comunidad?.LiderNombre,
            ComunidadLiderCedula = reporte.Comunidad?.LiderCedula,

            FechaCreacion = reporte.FechaCreacion,
            Estatus = reporte.Estatus.ToString(),
            
            Suministro = new ReporteSuministroDto
            {
                LlegaPorTuberia = reporte.LlegaPorTuberia,
                HorasSuministro = reporte.HorasSuministro,
                Caudal = reporte.Caudal?.ToString(),
                RecibeCisterna = reporte.RecibeCisterna,
                LitrosCisterna = reporte.LitrosCisterna,
                TipoCisterna = reporte.TipoCisterna?.ToString(),
                TieneTanque = reporte.TieneTanque,
                TipoTanque = reporte.TipoTanque?.ToString(),
                FamiliasBeneficiadas = reporte.FamiliasBeneficiadas,
                ApoyoAdicionalLitros = reporte.ApoyoAdicionalLitros
            },

            Incidencias = new ReporteIncidenciasDto
            {
                TieneVentaIlegal = reporte.TieneVentaIlegal,
                ChoferNombreApellido = reporte.ChoferNombreApellido,
                ChoferCedula = reporte.ChoferCedula,
                VehiculoMarcaModelo = reporte.VehiculoMarcaModelo,
                VehiculoPlaca = reporte.VehiculoPlaca,
                VehiculoColor = reporte.VehiculoColor,
                TieneTrancas = reporte.TieneTrancas,
                TrancaPropiciaNombre = reporte.TrancaPropiciaNombre,
                TrancaLugar = reporte.TrancaLugar,
                TrancaDuracion = reporte.TrancaDuracion,
                TieneConflictos = reporte.TieneConflictos,
                ConflictosExplicacion = reporte.ConflictosExplicacion,
                TieneFugas = reporte.TieneFugas,
                FugaLugar = reporte.FugaLugar,
                FugaTipo = reporte.FugaTipo?.ToString()
            },

            Salud = new ReporteSaludDto
            {
                TieneDiarrea = reporte.TieneDiarrea,
                CantidadCasosDiarrea = reporte.CantidadCasosDiarrea,
                TieneVomitos = reporte.TieneVomitos,
                TieneDolorAbdominal = reporte.TieneDolorAbdominal,
                PersonasAfectadas = reporte.PersonasAfectadas.Select(p => new PersonaAfectadaDto
                {
                    Nombre = p.Nombre,
                    Apellido = p.Apellido,
                    Edad = p.Edad,
                    Cedula = p.Cedula,
                    Condicion = p.Condicion
                }).ToList()
            },

            Participacion = new ReporteParticipacionDto
            {
                TienePartido = reporte.TienePartido,
                PartidoNombre = reporte.Partido?.ToString(),
                TieneAlcaldia = reporte.TieneAlcaldia,
                DetalleAlcaldia = reporte.DetalleAlcaldia,
                TieneGobernacion = reporte.TieneGobernacion,
                DetalleGobernacion = reporte.DetalleGobernacion,
                TieneInstitucionNacional = reporte.TieneInstitucionNacional,
                DetalleInstitucionNacional = reporte.DetalleInstitucionNacional
            }
        };
    }
}
