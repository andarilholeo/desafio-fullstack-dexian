using DesafioDexian.Application.DTOs;
using DesafioDexian.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DesafioDexian.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class EscolasController : ControllerBase
{
    private readonly IEscolaService _escolaService;

    public EscolasController(IEscolaService escolaService)
    {
        _escolaService = escolaService;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<EscolaDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        var escolas = await _escolaService.GetAllAsync();
        return Ok(escolas);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(EscolaDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(int id)
    {
        var escola = await _escolaService.GetByIdAsync(id);

        if (escola is null)
        {
            return NotFound(new { message = "Escola não encontrada" });
        }

        return Ok(escola);
    }

    [HttpPost]
    [ProducesResponseType(typeof(EscolaDto), StatusCodes.Status201Created)]
    public async Task<IActionResult> Create([FromBody] CreateEscolaDto dto)
    {
        var escola = await _escolaService.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = escola.ICodEscola }, escola);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(typeof(EscolaDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateEscolaDto dto)
    {
        var escola = await _escolaService.UpdateAsync(id, dto);

        if (escola is null)
        {
            return NotFound(new { message = "Escola não encontrada" });
        }

        return Ok(escola);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _escolaService.DeleteAsync(id);

        if (!deleted)
        {
            return NotFound(new { message = "Escola não encontrada" });
        }

        return NoContent();
    }
}

