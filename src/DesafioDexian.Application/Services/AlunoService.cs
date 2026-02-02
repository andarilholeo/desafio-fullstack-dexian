using DesafioDexian.Application.DTOs;
using DesafioDexian.Application.Interfaces;
using DesafioDexian.Domain.Entities;
using DesafioDexian.Domain.Interfaces;

namespace DesafioDexian.Application.Services;

public class AlunoService : IAlunoService
{
    private readonly IAlunoRepository _alunoRepository;

    public AlunoService(IAlunoRepository alunoRepository)
    {
        _alunoRepository = alunoRepository;
    }

    public async Task<IEnumerable<AlunoDto>> GetAllAsync()
    {
        var alunos = await _alunoRepository.GetAllAsync();
        return alunos.Select(MapToDto);
    }

    public async Task<AlunoDto?> GetByIdAsync(int id)
    {
        var aluno = await _alunoRepository.GetByIdAsync(id);
        return aluno is null ? null : MapToDto(aluno);
    }

    public async Task<AlunoDto> CreateAsync(CreateAlunoDto dto)
    {
        var aluno = new Aluno
        {
            SNome = dto.SNome,
            DNascimento = dto.DNascimento,
            SCPF = dto.SCPF,
            SEndereco = dto.SEndereco,
            SCelular = dto.SCelular,
            ICodEscola = dto.ICodEscola
        };

        var created = await _alunoRepository.CreateAsync(aluno);
        return MapToDto(created);
    }

    public async Task<AlunoDto?> UpdateAsync(int id, UpdateAlunoDto dto)
    {
        var aluno = new Aluno
        {
            ICodAluno = id,
            SNome = dto.SNome,
            DNascimento = dto.DNascimento,
            SCPF = dto.SCPF,
            SEndereco = dto.SEndereco,
            SCelular = dto.SCelular,
            ICodEscola = dto.ICodEscola
        };

        var updated = await _alunoRepository.UpdateAsync(aluno);
        return updated is null ? null : MapToDto(updated);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        return await _alunoRepository.DeleteAsync(id);
    }

    public async Task<IEnumerable<AlunoDto>> GetByEscolaIdAsync(int escolaId)
    {
        var alunos = await _alunoRepository.GetByEscolaIdAsync(escolaId);
        return alunos.Select(MapToDto);
    }

    private static AlunoDto MapToDto(Aluno aluno)
    {
        return new AlunoDto(
            aluno.ICodAluno,
            aluno.SNome,
            aluno.DNascimento,
            aluno.SCPF,
            aluno.SEndereco,
            aluno.SCelular,
            aluno.ICodEscola
        );
    }
}

