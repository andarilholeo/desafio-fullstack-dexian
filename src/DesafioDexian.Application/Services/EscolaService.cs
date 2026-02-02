using DesafioDexian.Application.DTOs;
using DesafioDexian.Application.Interfaces;
using DesafioDexian.Domain.Common;
using DesafioDexian.Domain.Entities;
using DesafioDexian.Domain.Interfaces;

namespace DesafioDexian.Application.Services;

public class EscolaService : IEscolaService
{
    private readonly IEscolaRepository _escolaRepository;

    public EscolaService(IEscolaRepository escolaRepository)
    {
        _escolaRepository = escolaRepository;
    }

    public async Task<Result<IEnumerable<EscolaDto>>> GetAllAsync()
    {
        var escolas = await _escolaRepository.GetAllAsync();
        return Result.Success(escolas.Select(MapToDto));
    }

    public async Task<Result<EscolaDto>> GetByIdAsync(int id)
    {
        var escola = await _escolaRepository.GetByIdAsync(id);

        if (escola is null)
        {
            return Result.Failure<EscolaDto>("Escola não encontrada", ResultErrorCode.NotFound);
        }

        return Result.Success(MapToDto(escola));
    }

    public async Task<Result<EscolaDto>> CreateAsync(CreateEscolaDto dto)
    {
        var escola = new Escola
        {
            SDescricao = dto.SDescricao
        };

        var created = await _escolaRepository.CreateAsync(escola);
        return Result.Success(MapToDto(created));
    }

    public async Task<Result<EscolaDto>> UpdateAsync(int id, UpdateEscolaDto dto)
    {
        var existing = await _escolaRepository.GetByIdAsync(id);

        if (existing is null)
        {
            return Result.Failure<EscolaDto>("Escola não encontrada", ResultErrorCode.NotFound);
        }

        var escola = new Escola
        {
            ICodEscola = id,
            SDescricao = dto.SDescricao
        };

        var updated = await _escolaRepository.UpdateAsync(escola);
        return Result.Success(MapToDto(updated!));
    }

    public async Task<Result> DeleteAsync(int id)
    {
        var existing = await _escolaRepository.GetByIdAsync(id);

        if (existing is null)
        {
            return Result.Failure("Escola não encontrada", ResultErrorCode.NotFound);
        }

        await _escolaRepository.DeleteAsync(id);
        return Result.Success();
    }

    private static EscolaDto MapToDto(Escola escola)
    {
        return new EscolaDto(
            escola.ICodEscola,
            escola.SDescricao
        );
    }
}

