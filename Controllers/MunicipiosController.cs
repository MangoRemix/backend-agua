using backend_agua.Dtos.Municipio;
using backend_agua.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace backend_agua.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class MunicipiosController : ControllerBase
{
    private readonly IMunicipioService _municipioService;

    public MunicipiosController(IMunicipioService municipioService)
    {
        _municipioService = municipioService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<MunicipioDto>>> GetMunicipios()
    {
        return Ok(await _municipioService.GetAllAsync());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<MunicipioDto>> GetMunicipio(Guid id)
    {
        var municipio = await _municipioService.GetByIdAsync(id);
        if (municipio == null) return NotFound();
        return Ok(municipio);
    }

    [HttpPost]
    public async Task<ActionResult<MunicipioDto>> PostMunicipio(MunicipioCreateDto createDto)
    {
        var municipio = await _municipioService.CreateAsync(createDto);
        return CreatedAtAction(nameof(GetMunicipio), new { id = municipio.Id }, municipio);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutMunicipio(Guid id, MunicipioCreateDto updateDto)
    {
        var updated = await _municipioService.UpdateAsync(id, updateDto);
        if (updated == null) return NotFound();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteMunicipio(Guid id)
    {
        var deleted = await _municipioService.DeleteAsync(id);
        if (!deleted) return NotFound();
        return NoContent();
    }
}
