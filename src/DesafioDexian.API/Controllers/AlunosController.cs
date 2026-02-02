using DesafioDexian.Application.DTOs;
using DesafioDexian.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DesafioDexian.API.Controllers;

[Route("api/[controller]")]
[Authorize]
public class AlunosController : ApiControllerBase
{
    private readonly IAlunoService _alunoService;

    public AlunosController(IAlunoService alunoService)
    {
        _alunoService = alunoService;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<AlunoDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        var result = await _alunoService.GetAllAsync();
        return HandleResult(result);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(AlunoDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _alunoService.GetByIdAsync(id);
        return HandleResult(result);
    }

    [HttpGet("escola/{escolaId}")]
    [ProducesResponseType(typeof(IEnumerable<AlunoDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetByEscolaId(int escolaId)
    {
        var result = await _alunoService.GetByEscolaIdAsync(escolaId);
        return HandleResult(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(AlunoDto), StatusCodes.Status201Created)]
    public async Task<IActionResult> Create([FromBody] CreateAlunoDto dto)
    {
        var result = await _alunoService.CreateAsync(dto);
        return HandleCreatedResult(result, nameof(GetById), new { id = result.Value?.ICodAluno });
    }

    [HttpPut("{id}")]
    [ProducesResponseType(typeof(AlunoDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateAlunoDto dto)
    {
        var result = await _alunoService.UpdateAsync(id, dto);
        return HandleResult(result);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _alunoService.DeleteAsync(id);
        return HandleResult(result);
    }
}

