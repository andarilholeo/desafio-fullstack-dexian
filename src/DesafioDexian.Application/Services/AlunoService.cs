using DesafioDexian.Application.DTOs;
using DesafioDexian.Application.Interfaces;
using DesafioDexian.Domain.Common;
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

    public async Task<Result<IEnumerable<AlunoDto>>> GetAllAsync()
    {
        var alunos = await _alunoRepository.GetAllAsync();
        return Result.Success(alunos.Select(MapToDto));
    }

    public async Task<Result<AlunoDto>> GetByIdAsync(int id)
    {
        var aluno = await _alunoRepository.GetByIdAsync(id);

        if (aluno is null)
        {
            return Result.Failure<AlunoDto>("Aluno não encontrado", ResultErrorCode.NotFound);
        }

        return Result.Success(MapToDto(aluno));
    }

    public async Task<Result<AlunoDto>> CreateAsync(CreateAlunoDto dto)
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
        return Result.Success(MapToDto(created));
    }

    public async Task<Result<AlunoDto>> UpdateAsync(int id, UpdateAlunoDto dto)
    {
        var existing = await _alunoRepository.GetByIdAsync(id);

        if (existing is null)
        {
            return Result.Failure<AlunoDto>("Aluno não encontrado", ResultErrorCode.NotFound);
        }

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
        return Result.Success(MapToDto(updated!));
    }

    public async Task<Result> DeleteAsync(int id)
    {
        var existing = await _alunoRepository.GetByIdAsync(id);

        if (existing is null)
        {
            return Result.Failure("Aluno não encontrado", ResultErrorCode.NotFound);
        }

        await _alunoRepository.DeleteAsync(id);
        return Result.Success();
    }

    public async Task<Result<IEnumerable<AlunoDto>>> GetByEscolaIdAsync(int escolaId)
    {
        var alunos = await _alunoRepository.GetByEscolaIdAsync(escolaId);
        return Result.Success(alunos.Select(MapToDto));
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

