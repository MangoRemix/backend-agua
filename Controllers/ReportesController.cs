using backend_agua.Dtos.Reporte;
using backend_agua.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace backend_agua.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class ReportesController : ControllerBase
{
    private readonly IReporteService _reporteService;

    public ReportesController(IReporteService reporteService)
    {
        _reporteService = reporteService;
    }

    [HttpGet("status")]
    public async Task<ActionResult<ReporteStatusDto>> GetStatus()
    {
        return Ok(await _reporteService.GetReporteStatusAsync());
    }

    [HttpPost("iniciar/{comunidadId}")]
    public async Task<ActionResult<ReporteDto>> Iniciar(Guid comunidadId)
    {
        try
        {
            var usuarioIdStr = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(usuarioIdStr)) return Unauthorized();

            var usuarioId = Guid.Parse(usuarioIdStr);
            var reporte = await _reporteService.IniciarReporteAsync(comunidadId, usuarioId);
            
            return CreatedAtAction(nameof(GetById), new { id = reporte.Id }, reporte);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPatch("{id}/suministro")]
    public async Task<ActionResult<ReporteDto>> UpdateSuministro(Guid id, ReporteSuministroUpdateDto updateDto)
    {
        var reporte = await _reporteService.UpdateSuministroAsync(id, updateDto);
        if (reporte == null) return NotFound();
        
        return Ok(reporte);
    }

    [HttpPatch("{id}/incidencias")]
    public async Task<ActionResult<ReporteDto>> UpdateIncidencias(Guid id, ReporteIncidenciasUpdateDto updateDto)
    {
        var reporte = await _reporteService.UpdateIncidenciasAsync(id, updateDto);
        if (reporte == null) return NotFound();
        
        return Ok(reporte);
    }

    [HttpPatch("{id}/salud")]
    public async Task<ActionResult<ReporteDto>> UpdateSalud(Guid id, ReporteSaludUpdateDto updateDto)
    {
        var reporte = await _reporteService.UpdateSaludAsync(id, updateDto);
        if (reporte == null) return NotFound();
        
        return Ok(reporte);
    }

    [HttpPatch("{id}/participacion")]
    public async Task<ActionResult<ReporteDto>> UpdateParticipacion(Guid id, ReporteParticipacionUpdateDto updateDto)
    {
        var reporte = await _reporteService.UpdateParticipacionAsync(id, updateDto);
        if (reporte == null) return NotFound();
        
        return Ok(reporte);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ReporteDto>> GetById(Guid id)
    {
        var reporte = await _reporteService.GetByIdAsync(id);
        if (reporte == null) return NotFound();
        
        return Ok(reporte);
    }

    [HttpGet("comunidad/{comunidadId}")]
    public async Task<ActionResult<IEnumerable<ReporteDto>>> GetByComunidad(Guid comunidadId)
    {
        return Ok(await _reporteService.GetByComunidadAsync(comunidadId));
    }
}
