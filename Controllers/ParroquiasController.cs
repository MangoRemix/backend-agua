using backend_agua.Dtos.Parroquia;
using backend_agua.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace backend_agua.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class ParroquiasController : ControllerBase
{
    private readonly IParroquiaService _parroquiaService;

    public ParroquiasController(IParroquiaService parroquiaService)
    {
        _parroquiaService = parroquiaService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ParroquiaDto>>> GetParroquias()
    {
        return Ok(await _parroquiaService.GetAllAsync());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ParroquiaDto>> GetParroquia(Guid id)
    {
        var parroquia = await _parroquiaService.GetByIdAsync(id);
        if (parroquia == null) return NotFound();
        return Ok(parroquia);
    }

    [HttpGet("municipio/{municipioId}")]
    public async Task<ActionResult<IEnumerable<ParroquiaDto>>> GetParroquiasByMunicipio(Guid municipioId)
    {
        return Ok(await _parroquiaService.GetByMunicipioIdAsync(municipioId));
    }

    [HttpPost]
    public async Task<ActionResult<ParroquiaDto>> PostParroquia(ParroquiaCreateDto createDto)
    {
        var parroquia = await _parroquiaService.CreateAsync(createDto);
        return CreatedAtAction(nameof(GetParroquia), new { id = parroquia.Id }, parroquia);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutParroquia(Guid id, ParroquiaCreateDto updateDto)
    {
        var updated = await _parroquiaService.UpdateAsync(id, updateDto);
        if (updated == null) return NotFound();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteParroquia(Guid id)
    {
        var deleted = await _parroquiaService.DeleteAsync(id);
        if (!deleted) return NotFound();
        return NoContent();
    }
}
