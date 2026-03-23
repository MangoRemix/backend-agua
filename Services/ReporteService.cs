using backend_agua.Dtos.Reporte;
using backend_agua.Infraestructure.Database;
using backend_agua.Interfaces;
using backend_agua.Models;
using backend_agua.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace backend_agua.Services;

public class ReporteService : IReporteService
{
    private readonly ApplicationDbContext _context;
    private readonly ReportSettings _settings;

    public ReporteService(ApplicationDbContext context, IOptions<ReportSettings> settings)
    {
        _context = context;
        _settings = settings.Value;
    }

    public async Task<ReporteStatusDto> GetReporteStatusAsync()
    {
        var nowUtc = DateTime.UtcNow;
        var venezuelaTime = TimeZoneInfo.ConvertTimeFromUtc(nowUtc, GetCaracasTimeZone());
        
        bool isManualDisabled = _settings.IsManualDisabled;
        bool isManualEnabled = _settings.IsManualEnabled;
        
        var openingToday = venezuelaTime.Date.AddHours(_settings.OpenHour).AddMinutes(_settings.OpenMinute);
        var closingToday = openingToday.AddMinutes(_settings.DurationMinutes);
        
        // El manual disabled tiene prioridad sobre el manual enabled por seguridad
        bool isOpen = !isManualDisabled && (isManualEnabled || (venezuelaTime >= openingToday && venezuelaTime < closingToday));
        
        DateTime nextOpening;
        DateTime? closingTime = null;
        double remainingSeconds;
        string message;
        
        if (isOpen)
        {
            nextOpening = openingToday.AddDays(1);
            
            if (isManualEnabled && !isManualDisabled)
            {
                closingTime = null;
                remainingSeconds = 0;
                message = "El sistema está abierto manualmente por configuración administrativa.";
            }
            else
            {
                closingTime = closingToday;
                remainingSeconds = (closingToday - venezuelaTime).TotalSeconds;
                message = $"El sistema de reportes está abierto. Cierra en {(int)remainingSeconds / 60}m {(int)remainingSeconds % 60}s.";
            }
        }
        else
        {
            if (isManualDisabled)
            {
                nextOpening = openingToday.AddDays(1);
                remainingSeconds = 0;
                message = "El sistema de reportes está deshabilitado manualmente.";
            }
            else if (venezuelaTime < openingToday)
            {
                nextOpening = openingToday;
                remainingSeconds = (nextOpening - venezuelaTime).TotalSeconds;
                message = $"El sistema de reportes está cerrado. Abre a las {_settings.OpenHour}:{_settings.OpenMinute:D2}.";
            }
            else
            {
                nextOpening = openingToday.AddDays(1);
                remainingSeconds = (nextOpening - venezuelaTime).TotalSeconds;
                message = $"El sistema de reportes está cerrado. Abre mañana a las {_settings.OpenHour}:{_settings.OpenMinute:D2}.";
            }
        }
        
        return new ReporteStatusDto
        {
            IsOpen = isOpen,
            ServerTime = venezuelaTime,
            NextOpeningTime = nextOpening,
            ClosingTime = closingTime,
            RemainingSeconds = remainingSeconds,
            Message = message
        };
    }

    public async Task<ReporteDto> IniciarReporteAsync(Guid comunidadId, Guid usuarioId)
    {
        var status = await GetReporteStatusAsync();
        if (!status.IsOpen)
        {
            throw new InvalidOperationException("No se pueden iniciar reportes fuera del horario permitido (8:00 PM - 9:00 PM Venezuela).");
        }

        var reporteId = Guid.NewGuid();
        var reporte = new Reporte
        {
            Id = reporteId,
            ComunidadId = comunidadId,
            UsuarioId = usuarioId,
            FechaCreacion = DateTime.UtcNow,
            Estatus = EstatusReporte.Incompleto,
            
            // Inicializar secciones
            Suministro = new ReporteSuministro { Id = Guid.NewGuid(), ReporteId = reporteId },
            Incidencia = new ReporteIncidencia { Id = Guid.NewGuid(), ReporteId = reporteId },
            Salud = new ReporteSalud { Id = Guid.NewGuid(), ReporteId = reporteId },
            Participacion = new ReporteParticipacion { Id = Guid.NewGuid(), ReporteId = reporteId }
        };

        _context.Reportes.Add(reporte);
        await _context.SaveChangesAsync();

        return (await GetByIdAsync(reporte.Id))!;
    }

    public async Task<ReporteDto?> UpdateSuministroAsync(Guid reporteId, ReporteSuministroUpdateDto updateDto)
    {
        var reporte = await _context.Reportes
            .Include(r => r.Suministro)
            .FirstOrDefaultAsync(r => r.Id == reporteId);

        if (reporte == null) return null;

        reporte.Suministro.LlegaPorTuberia = updateDto.LlegaPorTuberia;
        reporte.Suministro.HorasSuministro = updateDto.LlegaPorTuberia ? updateDto.HorasSuministro : null;
        reporte.Suministro.Caudal = updateDto.LlegaPorTuberia ? updateDto.Caudal : null;

        reporte.Suministro.RecibeCisterna = updateDto.RecibeCisterna;
        reporte.Suministro.LitrosCisterna = updateDto.RecibeCisterna ? updateDto.LitrosCisterna : null;
        reporte.Suministro.TipoCisterna = updateDto.RecibeCisterna ? updateDto.TipoCisterna : null;

        reporte.Suministro.TieneTanque = updateDto.TieneTanque;
        reporte.Suministro.TipoTanque = updateDto.TieneTanque ? updateDto.TipoTanque : null;

        reporte.Suministro.FamiliasBeneficiadas = updateDto.FamiliasBeneficiadas;
        reporte.Suministro.ApoyoAdicionalLitros = updateDto.ApoyoAdicionalLitros;

        await _context.SaveChangesAsync();

        return await GetByIdAsync(reporte.Id);
    }

    public async Task<ReporteDto?> UpdateIncidenciasAsync(Guid reporteId, ReporteIncidenciasUpdateDto updateDto)
    {
        var reporte = await _context.Reportes
            .Include(r => r.Incidencia)
            .FirstOrDefaultAsync(r => r.Id == reporteId);

        if (reporte == null) return null;

        // Venta Ilegal
        reporte.Incidencia.TieneVentaIlegal = updateDto.TieneVentaIlegal;
        reporte.Incidencia.ChoferNombreApellido = updateDto.TieneVentaIlegal ? updateDto.ChoferNombreApellido : null;
        reporte.Incidencia.ChoferCedula = updateDto.TieneVentaIlegal ? updateDto.ChoferCedula : null;
        reporte.Incidencia.VehiculoMarcaModelo = updateDto.TieneVentaIlegal ? updateDto.VehiculoMarcaModelo : null;
        reporte.Incidencia.VehiculoPlaca = updateDto.TieneVentaIlegal ? updateDto.VehiculoPlaca : null;
        reporte.Incidencia.VehiculoColor = updateDto.TieneVentaIlegal ? updateDto.VehiculoColor : null;

        // Trancas
        reporte.Incidencia.TieneTrancas = updateDto.TieneTrancas;
        reporte.Incidencia.TrancaPropiciaNombre = updateDto.TieneTrancas ? updateDto.TrancaPropiciaNombre : null;
        reporte.Incidencia.TrancaLugar = updateDto.TieneTrancas ? updateDto.TrancaLugar : null;
        reporte.Incidencia.TrancaDuracion = updateDto.TieneTrancas ? updateDto.TrancaDuracion : null;

        // Conflictos
        reporte.Incidencia.TieneConflictos = updateDto.TieneConflictos;
        reporte.Incidencia.ConflictosExplicacion = updateDto.TieneConflictos ? updateDto.ConflictosExplicacion : null;

        // Fugas
        reporte.Incidencia.TieneFugas = updateDto.TieneFugas;
        reporte.Incidencia.FugaLugar = updateDto.TieneFugas ? updateDto.FugaLugar : null;
        reporte.Incidencia.FugaTipo = updateDto.TieneFugas ? updateDto.FugaTipo : null;

        await _context.SaveChangesAsync();
        return await GetByIdAsync(reporte.Id);
    }

    public async Task<ReporteDto?> UpdateSaludAsync(Guid reporteId, ReporteSaludUpdateDto updateDto)
    {
        var reporte = await _context.Reportes
            .Include(r => r.Salud)
                .ThenInclude(s => s.PersonasAfectadas)
            .FirstOrDefaultAsync(r => r.Id == reporteId);

        if (reporte == null) return null;

        reporte.Salud.TieneDiarrea = updateDto.TieneDiarrea;
        reporte.Salud.CantidadCasosDiarrea = updateDto.TieneDiarrea ? updateDto.CantidadCasosDiarrea : 0;
        reporte.Salud.TieneVomitos = updateDto.TieneVomitos;
        reporte.Salud.TieneDolorAbdominal = updateDto.TieneDolorAbdominal;

        // Limpiar personas afectadas anteriores y agregar las nuevas
        _context.PersonasAfectadas.RemoveRange(reporte.Salud.PersonasAfectadas);
        
        foreach (var personaDto in updateDto.PersonasAfectadas)
        {
            // Validar si la persona corresponde a una condición activa
            bool esValida = false;
            if (personaDto.Condicion == CondicionSalud.Diarrea && updateDto.TieneDiarrea) esValida = true;
            if (personaDto.Condicion == CondicionSalud.Vomitos && updateDto.TieneVomitos) esValida = true;
            if (personaDto.Condicion == CondicionSalud.DolorAbdominal && updateDto.TieneDolorAbdominal) esValida = true;

            if (esValida)
            {
                reporte.Salud.PersonasAfectadas.Add(new PersonaAfectada
                {
                    Id = Guid.NewGuid(),
                    ReporteSaludId = reporte.Salud.Id,
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
        var reporte = await _context.Reportes
            .Include(r => r.Participacion)
            .FirstOrDefaultAsync(r => r.Id == reporteId);

        if (reporte == null) return null;

        // Partido
        reporte.Participacion.TienePartido = updateDto.TienePartido;
        reporte.Participacion.Partido = updateDto.TienePartido ? updateDto.Partido : null;

        // Alcaldia
        reporte.Participacion.TieneAlcaldia = updateDto.TieneAlcaldia;
        reporte.Participacion.DetalleAlcaldia = updateDto.TieneAlcaldia ? updateDto.DetalleAlcaldia : null;

        // Gobernacion
        reporte.Participacion.TieneGobernacion = updateDto.TieneGobernacion;
        reporte.Participacion.DetalleGobernacion = updateDto.TieneGobernacion ? updateDto.DetalleGobernacion : null;

        // Institucion Nacional
        reporte.Participacion.TieneInstitucionNacional = updateDto.TieneInstitucionNacional;
        reporte.Participacion.DetalleInstitucionNacional = updateDto.TieneInstitucionNacional ? updateDto.DetalleInstitucionNacional : null;

        // El paso 4 es el último paso del stepper, por lo que marcamos el reporte como completado
        reporte.Estatus = EstatusReporte.Completado;

        await _context.SaveChangesAsync();
        return await GetByIdAsync(reporte.Id);
    }

    public async Task<ReporteDto?> UpdateIsLeidoAsync(Guid reporteId, bool isLeido)
    {
        var reporte = await _context.Reportes.FirstOrDefaultAsync(r => r.Id == reporteId);
        if (reporte == null) return null;

        reporte.IsLeido = isLeido;
        await _context.SaveChangesAsync();

        return await GetByIdAsync(reporte.Id);
    }

    public async Task<ReporteDto?> GetByIdAsync(Guid id)
    {
        var reporte = await _context.Reportes
            .Include(r => r.Comunidad)
                .ThenInclude(c => c.Comuna)
                    .ThenInclude(c => c.Parroquia)
            .Include(r => r.Usuario)
            .Include(r => r.Suministro)
            .Include(r => r.Incidencia)
            .Include(r => r.Salud)
                .ThenInclude(s => s.PersonasAfectadas)
            .Include(r => r.Participacion)
            .FirstOrDefaultAsync(r => r.Id == id);

        if (reporte == null) return null;

        return MapToDto(reporte);
    }

    public async Task<IEnumerable<ReporteDto>> GetByComunidadAsync(Guid comunidadId)
    {
        var reportes = await _context.Reportes
            .Include(r => r.Comunidad)
                .ThenInclude(c => c.Comuna)
                    .ThenInclude(c => c.Parroquia)
            .Include(r => r.Usuario)
            .Include(r => r.Suministro)
            .Include(r => r.Incidencia)
            .Include(r => r.Salud)
                .ThenInclude(s => s.PersonasAfectadas)
            .Include(r => r.Participacion)
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
            
            ParroquiaNombre = reporte.Comunidad?.Comuna?.Parroquia?.Nombre,
            ParroquiaLiderNombre = reporte.Comunidad?.Comuna?.Parroquia?.LiderNombre,
            ParroquiaLiderCedula = reporte.Comunidad?.Comuna?.Parroquia?.LiderCedula,
            ParroquiaLiderTlf = reporte.Comunidad?.Comuna?.Parroquia?.LiderTlf,

            ComunaNombre = reporte.Comunidad?.Comuna?.Nombre,
            ComunaLiderNombre = reporte.Comunidad?.Comuna?.LiderNombre,
            ComunaLiderCedula = reporte.Comunidad?.Comuna?.LiderCedula,
            ComunaLiderTlf = reporte.Comunidad?.Comuna?.LiderTlf,

            ComunidadNombre = reporte.Comunidad?.Nombre,
            ComunidadLiderNombre = reporte.Comunidad?.LiderNombre,
            ComunidadLiderCedula = reporte.Comunidad?.LiderCedula,
            ComunidadLiderTlf = reporte.Comunidad?.LiderTlf,

            FechaCreacion = reporte.FechaCreacion,
            Estatus = reporte.Estatus.ToString(),
            IsLeido = reporte.IsLeido,
            
            Suministro = new ReporteSuministroDto
            {
                LlegaPorTuberia = reporte.Suministro.LlegaPorTuberia,
                HorasSuministro = reporte.Suministro.HorasSuministro,
                Caudal = reporte.Suministro.Caudal?.ToString(),
                RecibeCisterna = reporte.Suministro.RecibeCisterna,
                LitrosCisterna = reporte.Suministro.LitrosCisterna,
                TipoCisterna = reporte.Suministro.TipoCisterna?.ToString(),
                TieneTanque = reporte.Suministro.TieneTanque,
                TipoTanque = reporte.Suministro.TipoTanque?.ToString(),
                FamiliasBeneficiadas = reporte.Suministro.FamiliasBeneficiadas,
                ApoyoAdicionalLitros = reporte.Suministro.ApoyoAdicionalLitros
            },

            Incidencias = new ReporteIncidenciasDto
            {
                TieneVentaIlegal = reporte.Incidencia.TieneVentaIlegal,
                ChoferNombreApellido = reporte.Incidencia.ChoferNombreApellido,
                ChoferCedula = reporte.Incidencia.ChoferCedula,
                VehiculoMarcaModelo = reporte.Incidencia.VehiculoMarcaModelo,
                VehiculoPlaca = reporte.Incidencia.VehiculoPlaca,
                VehiculoColor = reporte.Incidencia.VehiculoColor,
                TieneTrancas = reporte.Incidencia.TieneTrancas,
                TrancaPropiciaNombre = reporte.Incidencia.TrancaPropiciaNombre,
                TrancaLugar = reporte.Incidencia.TrancaLugar,
                TrancaDuracion = reporte.Incidencia.TrancaDuracion,
                TieneConflictos = reporte.Incidencia.TieneConflictos,
                ConflictosExplicacion = reporte.Incidencia.ConflictosExplicacion,
                TieneFugas = reporte.Incidencia.TieneFugas,
                FugaLugar = reporte.Incidencia.FugaLugar,
                FugaTipo = reporte.Incidencia.FugaTipo?.ToString()
            },

            Salud = new ReporteSaludDto
            {
                TieneDiarrea = reporte.Salud.TieneDiarrea,
                CantidadCasosDiarrea = reporte.Salud.CantidadCasosDiarrea,
                TieneVomitos = reporte.Salud.TieneVomitos,
                TieneDolorAbdominal = reporte.Salud.TieneDolorAbdominal,
                PersonasAfectadas = reporte.Salud.PersonasAfectadas.Select(p => new PersonaAfectadaDto
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
                TienePartido = reporte.Participacion.TienePartido,
                PartidoNombre = reporte.Participacion.Partido?.ToString(),
                TieneAlcaldia = reporte.Participacion.TieneAlcaldia,
                DetalleAlcaldia = reporte.Participacion.DetalleAlcaldia,
                TieneGobernacion = reporte.Participacion.TieneGobernacion,
                DetalleGobernacion = reporte.Participacion.DetalleGobernacion,
                TieneInstitucionNacional = reporte.Participacion.TieneInstitucionNacional,
                DetalleInstitucionNacional = reporte.Participacion.DetalleInstitucionNacional
            }
        };
    }

    private static TimeZoneInfo GetCaracasTimeZone()
    {
        try
        {
            // Windows ID
            return TimeZoneInfo.FindSystemTimeZoneById("Venezuela Standard Time");
        }
        catch (TimeZoneNotFoundException)
        {
            // Linux/macOS/ICU ID
            return TimeZoneInfo.FindSystemTimeZoneById("America/Caracas");
        }
    }
}
