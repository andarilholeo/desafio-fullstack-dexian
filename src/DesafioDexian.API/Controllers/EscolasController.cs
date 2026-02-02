using DesafioDexian.Application.DTOs;
using DesafioDexian.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DesafioDexian.API.Controllers;

[Route("api/[controller]")]
[Authorize]
public class EscolasController : ApiControllerBase
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
        var result = await _escolaService.GetAllAsync();
        return HandleResult(result);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(EscolaDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _escolaService.GetByIdAsync(id);
        return HandleResult(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(EscolaDto), StatusCodes.Status201Created)]
    public async Task<IActionResult> Create([FromBody] CreateEscolaDto dto)
    {
        var result = await _escolaService.CreateAsync(dto);
        return HandleCreatedResult(result, nameof(GetById), new { id = result.Value?.ICodEscola });
    }

    [HttpPut("{id}")]
    [ProducesResponseType(typeof(EscolaDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateEscolaDto dto)
    {
        var result = await _escolaService.UpdateAsync(id, dto);
        return HandleResult(result);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _escolaService.DeleteAsync(id);
        return HandleResult(result);
    }
}

