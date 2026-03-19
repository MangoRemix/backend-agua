using backend_agua.Dtos.Comuna;
using backend_agua.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace backend_agua.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class ComunasController : ControllerBase
{
    private readonly IComunaService _comunaService;

    public ComunasController(IComunaService comunaService)
    {
        _comunaService = comunaService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ComunaDto>>> GetComunas()
    {
        return Ok(await _comunaService.GetAllAsync());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ComunaDto>> GetComuna(Guid id)
    {
        var comuna = await _comunaService.GetByIdAsync(id);
        if (comuna == null) return NotFound();
        return Ok(comuna);
    }

    [HttpGet("parroquia/{parroquiaId}")]
    public async Task<ActionResult<IEnumerable<ComunaDto>>> GetComunasByParroquia(Guid parroquiaId)
    {
        return Ok(await _comunaService.GetByParroquiaIdAsync(parroquiaId));
    }

    [HttpPost]
    public async Task<ActionResult<ComunaDto>> PostComuna(ComunaCreateDto createDto)
    {
        var comuna = await _comunaService.CreateAsync(createDto);
        return CreatedAtAction(nameof(GetComuna), new { id = comuna.Id }, comuna);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutComuna(Guid id, ComunaCreateDto updateDto)
    {
        var updated = await _comunaService.UpdateAsync(id, updateDto);
        if (updated == null) return NotFound();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteComuna(Guid id)
    {
        var deleted = await _comunaService.DeleteAsync(id);
        if (!deleted) return NotFound();
        return NoContent();
    }
}
