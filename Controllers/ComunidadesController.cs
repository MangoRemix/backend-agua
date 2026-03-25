using backend_agua.Dtos.Common;
using backend_agua.Dtos.Comunidad;
using backend_agua.Dtos.ComunidadDto;
using backend_agua.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace backend_agua.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class ComunidadesController : ControllerBase
{
    private readonly IComunidadService _comunidadService;

    public ComunidadesController(IComunidadService comunidadService)
    {
        _comunidadService = comunidadService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ComunidadDto>>> GetComunidades()
    {
        return Ok(await _comunidadService.GetAllAsync());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ComunidadDto>> GetComunidad(Guid id)
    {
        var comunidad = await _comunidadService.GetByIdAsync(id);
        if (comunidad == null) return NotFound();
        return Ok(comunidad);
    }

    [HttpGet("comuna/{comunaId}")]
    public async Task<ActionResult<PagedResult<ComunidadDto>>> GetComunidadesByComuna(Guid comunaId, [FromQuery] ComunidadFilterDto filter)
    {
        filter.ComunaId = comunaId;
        return Ok(await _comunidadService.GetPagedAsync(filter));
    }

    [HttpPost]
    public async Task<ActionResult<ComunidadDto>> PostComunidad(ComunidadCreateDto createDto)
    {
        var comunidad = await _comunidadService.CreateAsync(createDto);
        return CreatedAtAction(nameof(GetComunidad), new { id = comunidad.Id }, comunidad);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutComunidad(Guid id, ComunidadCreateDto updateDto)
    {
        var updated = await _comunidadService.UpdateAsync(id, updateDto);
        if (updated == null) return NotFound();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteComunidad(Guid id)
    {
        var deleted = await _comunidadService.DeleteAsync(id);
        if (!deleted) return NotFound();
        return NoContent();
    }
}
