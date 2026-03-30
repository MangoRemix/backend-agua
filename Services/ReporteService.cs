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
            SubmissionRemainingSeconds = remainingSeconds,
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

        var tz = GetCaracasTimeZone();
        var todayVzla = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, tz).Date;

        // Buscar reporte existente de hoy para esta comunidad
        // NOTA: Traemos los hijos para que el dashboard y el stepper tengan la data si ya se empezó
        var reporteExistente = await _context.Reportes
            .Include(r => r.Suministro)
                .ThenInclude(s => s.Cisternas)
            .Include(r => r.Suministro)
                .ThenInclude(s => s.Tanques)
            .Include(r => r.Incidencia)
            .Include(r => r.Salud)
                .ThenInclude(s => s.PersonasAfectadas)
            .Include(r => r.Participacion)
            .Where(r => r.ComunidadId == comunidadId)
            .ToListAsync();

        var deHoy = reporteExistente.FirstOrDefault(r => 
            TimeZoneInfo.ConvertTimeFromUtc(r.FechaCreacion, tz).Date == todayVzla);

        if (deHoy != null)
        {
            return MapToDto(deHoy);
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
                .ThenInclude(s => s.Cisternas)
            .Include(r => r.Suministro)
                .ThenInclude(s => s.Tanques)
            .FirstOrDefaultAsync(r => r.Id == reporteId);

        if (reporte == null) return null;

        reporte.Suministro.LlegaPorTuberia = updateDto.LlegaPorTuberia;
        reporte.Suministro.HorasSuministro = updateDto.LlegaPorTuberia ? updateDto.HorasSuministro : null;
        reporte.Suministro.Caudal = updateDto.LlegaPorTuberia ? updateDto.Caudal : null;

        reporte.Suministro.RecibeCisterna = updateDto.RecibeCisterna;
        
        // Manejo de Cisternas: Limpiar anteriores y agregar nuevas usando EF (evita conflictos de tracking)
        _context.Cisternas.RemoveRange(reporte.Suministro.Cisternas);
        reporte.Suministro.Cisternas.Clear();

        if (updateDto.RecibeCisterna && updateDto.Cisternas != null)
        {
            foreach (var cisternaDto in updateDto.Cisternas)
            {
                reporte.Suministro.Cisternas.Add(new Cisterna
                {
                    ReporteSuministroId = reporte.Suministro.Id,
                    Litros = cisternaDto.Litros,
                    Tipo = cisternaDto.Tipo
                });
            }
        }

        reporte.Suministro.TieneTanque = updateDto.TieneTanque;
        
        // Manejo de Tanques: Limpiar anteriores y agregar nuevos usando EF
        _context.Tanques.RemoveRange(reporte.Suministro.Tanques);
        reporte.Suministro.Tanques.Clear();

        if (updateDto.TieneTanque && updateDto.Tanques != null)
        {
            foreach (var tanqueDto in updateDto.Tanques)
            {
                reporte.Suministro.Tanques.Add(new Tanque
                {
                    ReporteSuministroId = reporte.Suministro.Id,
                    Tipo = tanqueDto.Tipo
                });
            }
        }

        reporte.Suministro.FamiliasBeneficiadas = updateDto.FamiliasBeneficiadas;
        reporte.Suministro.ApoyoAdicionalLitros = updateDto.ApoyoAdicionalLitros;

        await _context.SaveChangesAsync();

        return await GetByIdAsync(reporte.Id);
    }

    public async Task<ReporteDto?> UpdateIncidenciasAsync(Guid reporteId, ReporteIncidenciasUpdateDto updateDto)
    {
        var reporte = await _context.Reportes
            .Include(r => r.Incidencia)
                .ThenInclude(i => i.Trancas)
            .Include(r => r.Incidencia)
                .ThenInclude(i => i.Conflictos)
            .Include(r => r.Incidencia)
                .ThenInclude(i => i.Fugas)
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
        _context.Trancas.RemoveRange(reporte.Incidencia.Trancas);
        reporte.Incidencia.Trancas.Clear();
        reporte.Incidencia.TieneTrancas = updateDto.TieneTrancas || (updateDto.Trancas?.Any() ?? false);
        if (updateDto.Trancas != null)
        {
            foreach (var trancaDto in updateDto.Trancas)
            {
                reporte.Incidencia.Trancas.Add(new Tranca
                {
                    ReporteIncidenciaId = reporte.Incidencia.Id,
                    PropiciaNombre = trancaDto.PropiciaNombre,
                    Lugar = trancaDto.Lugar,
                    Duracion = trancaDto.Duracion
                });
            }
        }

        // Conflictos
        _context.Conflictos.RemoveRange(reporte.Incidencia.Conflictos);
        reporte.Incidencia.Conflictos.Clear();
        reporte.Incidencia.TieneConflictos = updateDto.TieneConflictos || (updateDto.Conflictos?.Any() ?? false);
        if (updateDto.Conflictos != null)
        {
            foreach (var conflictoDto in updateDto.Conflictos)
            {
                reporte.Incidencia.Conflictos.Add(new Conflicto
                {
                    ReporteIncidenciaId = reporte.Incidencia.Id,
                    Explicacion = conflictoDto.Explicacion
                });
            }
        }

        // Fugas
        _context.Fugas.RemoveRange(reporte.Incidencia.Fugas);
        reporte.Incidencia.Fugas.Clear();
        reporte.Incidencia.TieneFugas = updateDto.TieneFugas || (updateDto.Fugas?.Any() ?? false);
        if (updateDto.Fugas != null)
        {
            foreach (var fugaDto in updateDto.Fugas)
            {
                reporte.Incidencia.Fugas.Add(new Fuga
                {
                    ReporteIncidenciaId = reporte.Incidencia.Id,
                    Lugar = fugaDto.Lugar,
                    Tipo = fugaDto.Tipo
                });
            }
        }

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

        // Asegurar que la sección de salud exista
        if (reporte.Salud == null)
        {
            reporte.Salud = new ReporteSalud 
            { 
                Id = Guid.NewGuid(), 
                ReporteId = reporte.Id,
                PersonasAfectadas = new List<PersonaAfectada>()
            };
            _context.ReporteSalud.Add(reporte.Salud);
        }

        reporte.Salud.TieneDiarrea = updateDto.TieneDiarrea;
        reporte.Salud.CantidadCasosDiarrea = updateDto.TieneDiarrea ? updateDto.CantidadCasosDiarrea : 0;
        reporte.Salud.TieneVomitos = updateDto.TieneVomitos;
        reporte.Salud.CantidadCasosVomitos = updateDto.TieneVomitos ? updateDto.CantidadCasosVomitos : 0;
        reporte.Salud.TieneDolorAbdominal = updateDto.TieneDolorAbdominal;
        reporte.Salud.CantidadCasosDolorAbdominal = updateDto.TieneDolorAbdominal ? updateDto.CantidadCasosDolorAbdominal : 0;

        // Manejo de PersonasAfectadas: Limpiar anteriores y agregar nuevas usando EF
        _context.PersonasAfectadas.RemoveRange(reporte.Salud.PersonasAfectadas);
        reporte.Salud.PersonasAfectadas.Clear();
        
        if (updateDto.PersonasAfectadas != null)
        {
            foreach (var personaDto in updateDto.PersonasAfectadas)
            {
                // Agregamos la persona y activamos automáticamente las banderas de salud según sus condiciones
                var nuevaPersona = new PersonaAfectada
                {
                    ReporteSaludId = reporte.Salud.Id,
                    Nombre = personaDto.Nombre,
                    Apellido = personaDto.Apellido,
                    Edad = personaDto.Edad,
                    Cedula = personaDto.Cedula,
                    Condiciones = personaDto.Condiciones
                };

                foreach (var condicion in personaDto.Condiciones)
                {
                    if (condicion == CondicionSalud.Diarrea) reporte.Salud.TieneDiarrea = true;
                    if (condicion == CondicionSalud.Vomitos) reporte.Salud.TieneVomitos = true;
                    if (condicion == CondicionSalud.DolorAbdominal) reporte.Salud.TieneDolorAbdominal = true;
                }

                reporte.Salud.PersonasAfectadas.Add(nuevaPersona);
            }
        }

        await _context.SaveChangesAsync();
        return await GetByIdAsync(reporte.Id);
    }

    public async Task<ReporteDto?> UpdateParticipacionAsync(Guid reporteId, ReporteParticipacionUpdateDto updateDto)
    {
        var reporte = await _context.Reportes
            .Include(r => r.Suministro)
                .ThenInclude(s => s.Cisternas)
            .Include(r => r.Incidencia)
            .Include(r => r.Salud)
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

        // Validar si el reporte está realmente completo para cambiar su estatus
        if (EsReporteRealmenteCompleto(reporte))
        {
            reporte.Estatus = EstatusReporte.Completado;
        }
        else
        {
            reporte.Estatus = EstatusReporte.Incompleto;
        }

        await _context.SaveChangesAsync();
        return await GetByIdAsync(reporte.Id);
    }

    private bool EsReporteRealmenteCompleto(Reporte reporte)
    {
        if (reporte == null) return false;

        // 1. Suministro: Debe tener al menos una forma básica de obtención de agua marcada
        bool suministroLleno = reporte.Suministro != null && 
                             (reporte.Suministro.LlegaPorTuberia || 
                              (reporte.Suministro.RecibeCisterna && reporte.Suministro.Cisternas.Any()) || 
                              (reporte.Suministro.TieneTanque && reporte.Suministro.Tanques.Any()));

        // 2. Incidencias: Al menos una respuesta positiva o que el objeto exista y haya pasado por el flujo
        bool incidenciasOk = reporte.Incidencia != null;

        // 3. Salud: Se considera lleno si se respondieron las preguntas básicas
        bool saludOk = reporte.Salud != null;

        // 4. Participación: Validamos que al menos una de las opciones de participación sea verdadera
        bool participacionLlena = reporte.Participacion != null && 
                                (reporte.Participacion.TienePartido || 
                                 reporte.Participacion.TieneAlcaldia || 
                                 reporte.Participacion.TieneGobernacion || 
                                 reporte.Participacion.TieneInstitucionNacional);

        return suministroLleno && participacionLlena && incidenciasOk && saludOk;
    }

    public async Task<ReporteDto?> UpdateIsLeidoAsync(Guid reporteId, bool isLeido)
    {
        var reporte = await _context.Reportes.FirstOrDefaultAsync(r => r.Id == reporteId);
        if (reporte == null) return null;

        reporte.IsLeido = isLeido;
        await _context.SaveChangesAsync();

        return await GetByIdAsync(reporte.Id);
    }

    public async Task<IEnumerable<ReporteDto>> GetByComunidadAsync(Guid comunidadId)
    {
        var reportes = await _context.Reportes
            .Include(r => r.Comunidad)
                .ThenInclude(c => c.Comuna)
                    .ThenInclude(c => c.Parroquia)
            .Include(r => r.Usuario)
            .Include(r => r.Suministro)
                .ThenInclude(s => s.Cisternas)
            .Include(r => r.Suministro)
                .ThenInclude(s => s.Tanques)
            .Include(r => r.Incidencia)
            .Include(r => r.Salud)
                .ThenInclude(s => s.PersonasAfectadas)
            .Include(r => r.Participacion)
            .Where(r => r.ComunidadId == comunidadId)
            .OrderByDescending(r => r.FechaCreacion)
            .ToListAsync();

        return reportes.Select(r => MapToDto(r));
    }

    public async Task<backend_agua.Dtos.Common.PagedResult<ReporteDto>> GetPagedAsync(ReporteFilterDto filter)
    {
        var query = _context.Reportes
            .Include(r => r.Comunidad)
                .ThenInclude(c => c.Comuna)
                    .ThenInclude(c => c.Parroquia)
            .Include(r => r.Usuario)
            .Include(r => r.Suministro)
                .ThenInclude(s => s.Cisternas)
            .Include(r => r.Suministro)
                .ThenInclude(s => s.Tanques)
            .Include(r => r.Incidencia)
            .Include(r => r.Salud)
                .ThenInclude(s => s.PersonasAfectadas)
            .Include(r => r.Participacion)
            .AsQueryable();

        // Aplicar filtros
        if (filter.ParroquiaId.HasValue)
            query = query.Where(r => r.Comunidad.Comuna.ParroquiaId == filter.ParroquiaId);

        if (filter.ComunaId.HasValue)
            query = query.Where(r => r.Comunidad.ComunaId == filter.ComunaId);

        if (filter.ComunidadId.HasValue)
            query = query.Where(r => r.ComunidadId == filter.ComunidadId);

        if (filter.IsLeido.HasValue)
            query = query.Where(r => r.IsLeido == filter.IsLeido);

        if (filter.FechaDesde.HasValue)
            query = query.Where(r => r.FechaCreacion >= filter.FechaDesde.Value.ToUniversalTime());

        if (filter.FechaHasta.HasValue)
            query = query.Where(r => r.FechaCreacion <= filter.FechaHasta.Value.ToUniversalTime());

        // Ordenar por fecha descendente por defecto
        query = query.OrderByDescending(r => r.FechaCreacion);

        var totalItems = await query.CountAsync();
        
        var items = await query
            .AsSplitQuery() // <-- ESTO OBLIGA A CARGAR BIEN LAS RELACIONES CON PAGINACIÓN
            .Skip((filter.PageNumber - 1) * filter.PageSize)
            .Take(filter.PageSize)
            .ToListAsync();

        var dtos = items.Select(r => MapToDto(r)).ToList();

        return new backend_agua.Dtos.Common.PagedResult<ReporteDto>
        {
            Items = dtos,
            TotalItems = totalItems,
            PageNumber = filter.PageNumber,
            PageSize = filter.PageSize
        };
    }

    public async Task<ComunaDashboardDto> GetComunaDashboardAsync(Guid comunaId, int pageNumber, int pageSize, string statusFilter)
    {
        var tz = GetCaracasTimeZone();
        var todayVzla = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, tz).Date;

        var comuna = await _context.Comunas
            .Include(c => c.Comunidades)
            .FirstOrDefaultAsync(c => c.Id == comunaId);

        if (comuna == null) return new ComunaDashboardDto();

        var comunidadesIds = comuna.Comunidades.Select(c => c.Id).ToList();
        
        // Obtener el último reporte de cada comunidad de esta comuna
        var ultimosReportes = await _context.Reportes
            .Where(r => comunidadesIds.Contains(r.ComunidadId))
            .OrderByDescending(r => r.FechaCreacion)
            .ToListAsync();

        var comunidadStatusList = new List<ComunidadStatusDto>();

        foreach (var comunidad in comuna.Comunidades)
        {
            var ultimoReporte = ultimosReportes.FirstOrDefault(r => r.ComunidadId == comunidad.Id);
            string status = "PENDIENTE";
            string texto = "SIN REPORTES";

            if (ultimoReporte != null)
            {
                var fechaVzla = TimeZoneInfo.ConvertTimeFromUtc(ultimoReporte.FechaCreacion, tz);
                if (fechaVzla.Date == todayVzla)
                {
                    status = "RECIBIDO";
                    texto = $"HOY, {fechaVzla:hh:mm tt}";
                }
                else
                {
                    var dias = (todayVzla - fechaVzla.Date).Days;
                    if (dias == 1) texto = $"AYER, {fechaVzla:hh:mm tt}";
                    else texto = $"HACE {dias} DÍAS";
                }
            }

            comunidadStatusList.Add(new ComunidadStatusDto
            {
                Id = comunidad.Id,
                Nombre = comunidad.Nombre,
                Status = status,
                UltimaFechaReporte = ultimoReporte?.FechaCreacion,
                UltimoReporteTexto = texto
            });
        }

        // Aplicar filtro
        var filteredList = comunidadStatusList;
        if (statusFilter?.ToUpper() == "RECIBIDOS")
            filteredList = filteredList.Where(c => c.Status == "RECIBIDO").ToList();
        else if (statusFilter?.ToUpper() == "PENDIENTES")
            filteredList = filteredList.Where(c => c.Status == "PENDIENTE").ToList();

        var totalItems = filteredList.Count;
        var pagedItems = filteredList
            .OrderBy(c => c.Nombre)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        return new ComunaDashboardDto
        {
            ComunaNombre = comuna.Nombre,
            TotalComunidades = comuna.Comunidades.Count,
            EnviadosHoy = comunidadStatusList.Count(c => c.Status == "RECIBIDO"),
            ListadoComunidades = new backend_agua.Dtos.Common.PagedResult<ComunidadStatusDto>
            {
                Items = pagedItems,
                TotalItems = totalItems,
                PageNumber = pageNumber,
                PageSize = pageSize
            }
        };
    }

    public async Task<ReporteDto?> GetByIdAsync(Guid id)
    {
        var reporte = await _context.Reportes
            .Include(r => r.Comunidad)
                .ThenInclude(c => c.Comuna)
                    .ThenInclude(c => c.Parroquia)
            .Include(r => r.Usuario)
            .Include(r => r.Suministro)
                .ThenInclude(s => s.Cisternas)
            .Include(r => r.Suministro)
                .ThenInclude(s => s.Tanques)
            .Include(r => r.Incidencia)
                .ThenInclude(i => i.Trancas)
            .Include(r => r.Incidencia)
                .ThenInclude(i => i.Conflictos)
            .Include(r => r.Incidencia)
                .ThenInclude(i => i.Fugas)
            .Include(r => r.Salud)
                .ThenInclude(s => s.PersonasAfectadas)
            .Include(r => r.Participacion)
            .FirstOrDefaultAsync(r => r.Id == id);

        if (reporte == null) return null;

        return MapToDto(reporte);
    }


    private ReporteDto MapToDto(Reporte reporte)
    {
        var nowUtc = DateTime.UtcNow;
        var venezuelaTime = TimeZoneInfo.ConvertTimeFromUtc(nowUtc, GetCaracasTimeZone());
        var openingToday = reporte.FechaCreacion.Date.AddHours(_settings.OpenHour).AddMinutes(_settings.OpenMinute);
        var closingTime = openingToday.AddMinutes(_settings.DurationMinutes);
        
        double remainingSeconds = (closingTime - venezuelaTime).TotalSeconds;
        if (remainingSeconds < 0) remainingSeconds = 0;

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
            SubmissionRemainingSeconds = remainingSeconds,
            
            Suministro = reporte.Suministro == null ? new ReporteSuministroDto() : new ReporteSuministroDto
            {
                LlegaPorTuberia = reporte.Suministro.LlegaPorTuberia,
                HorasSuministro = reporte.Suministro.HorasSuministro,
                Caudal = reporte.Suministro.Caudal?.ToString(),
                RecibeCisterna = reporte.Suministro.RecibeCisterna,
                Cisternas = reporte.Suministro.Cisternas?.Select(c => new CisternaDto
                {
                    Id = c.Id,
                    Litros = c.Litros,
                    Tipo = c.Tipo.ToString()
                }).ToList() ?? new List<CisternaDto>(),
                TieneTanque = reporte.Suministro.TieneTanque,
                Tanques = reporte.Suministro.Tanques?.Select(t => new TanqueDto
                {
                    Id = t.Id,
                    Tipo = t.Tipo.ToString()
                }).ToList() ?? new List<TanqueDto>(),
                FamiliasBeneficiadas = reporte.Suministro.FamiliasBeneficiadas,
                ApoyoAdicionalLitros = reporte.Suministro.ApoyoAdicionalLitros
            },

            Incidencias = reporte.Incidencia == null ? new ReporteIncidenciasDto() : new ReporteIncidenciasDto
            {
                TieneVentaIlegal = reporte.Incidencia.TieneVentaIlegal,
                ChoferNombreApellido = reporte.Incidencia.ChoferNombreApellido,
                ChoferCedula = reporte.Incidencia.ChoferCedula,
                VehiculoMarcaModelo = reporte.Incidencia.VehiculoMarcaModelo,
                VehiculoPlaca = reporte.Incidencia.VehiculoPlaca,
                VehiculoColor = reporte.Incidencia.VehiculoColor,
                TieneTrancas = reporte.Incidencia.TieneTrancas,
                Trancas = reporte.Incidencia.Trancas?.Select(t => new OutputTrancaDto
                {
                    Id = t.Id,
                    PropiciaNombre = t.PropiciaNombre,
                    Lugar = t.Lugar,
                    Duracion = t.Duracion
                }).ToList() ?? new List<OutputTrancaDto>(),
                TieneConflictos = reporte.Incidencia.TieneConflictos,
                Conflictos = reporte.Incidencia.Conflictos?.Select(c => new OutputConflictoDto
                {
                    Id = c.Id,
                    Explicacion = c.Explicacion
                }).ToList() ?? new List<OutputConflictoDto>(),
                TieneFugas = reporte.Incidencia.TieneFugas,
                Fugas = reporte.Incidencia.Fugas?.Select(f => new OutputFugaDto
                {
                    Id = f.Id,
                    Lugar = f.Lugar,
                    Tipo = f.Tipo?.ToString()
                }).ToList() ?? new List<OutputFugaDto>()
            },

            Salud = reporte.Salud == null ? new ReporteSaludDto() : new ReporteSaludDto
            {
                TieneDiarrea = reporte.Salud.TieneDiarrea,
                CantidadCasosDiarrea = reporte.Salud.CantidadCasosDiarrea,
                TieneVomitos = reporte.Salud.TieneVomitos,
                CantidadCasosVomitos = reporte.Salud.CantidadCasosVomitos,
                TieneDolorAbdominal = reporte.Salud.TieneDolorAbdominal,
                CantidadCasosDolorAbdominal = reporte.Salud.CantidadCasosDolorAbdominal,
                PersonasAfectadas = reporte.Salud.PersonasAfectadas?.Select(p => new PersonaAfectadaDto
                {
                    Nombre = p.Nombre,
                    Apellido = p.Apellido,
                    Edad = p.Edad,
                    Cedula = p.Cedula,
                    Condiciones = p.Condiciones
                }).ToList() ?? new List<PersonaAfectadaDto>()
            },

            Participacion = reporte.Participacion == null ? new ReporteParticipacionDto() : new ReporteParticipacionDto
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
