using DesafioDexian.Application.DTOs;
using DesafioDexian.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DesafioDexian.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class AlunosController : ControllerBase
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
        var alunos = await _alunoService.GetAllAsync();
        return Ok(alunos);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(AlunoDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(int id)
    {
        var aluno = await _alunoService.GetByIdAsync(id);

        if (aluno is null)
        {
            return NotFound(new { message = "Aluno não encontrado" });
        }

        return Ok(aluno);
    }

    [HttpGet("escola/{escolaId}")]
    [ProducesResponseType(typeof(IEnumerable<AlunoDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetByEscolaId(int escolaId)
    {
        var alunos = await _alunoService.GetByEscolaIdAsync(escolaId);
        return Ok(alunos);
    }

    [HttpPost]
    [ProducesResponseType(typeof(AlunoDto), StatusCodes.Status201Created)]
    public async Task<IActionResult> Create([FromBody] CreateAlunoDto dto)
    {
        var aluno = await _alunoService.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = aluno.ICodAluno }, aluno);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(typeof(AlunoDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateAlunoDto dto)
    {
        var aluno = await _alunoService.UpdateAsync(id, dto);

        if (aluno is null)
        {
            return NotFound(new { message = "Aluno não encontrado" });
        }

        return Ok(aluno);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _alunoService.DeleteAsync(id);

        if (!deleted)
        {
            return NotFound(new { message = "Aluno não encontrado" });
        }

        return NoContent();
    }
}

