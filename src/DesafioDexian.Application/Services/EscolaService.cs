using DesafioDexian.Application.DTOs;
using DesafioDexian.Application.Interfaces;
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

    public async Task<IEnumerable<EscolaDto>> GetAllAsync()
    {
        var escolas = await _escolaRepository.GetAllAsync();
        return escolas.Select(MapToDto);
    }

    public async Task<EscolaDto?> GetByIdAsync(int id)
    {
        var escola = await _escolaRepository.GetByIdAsync(id);
        return escola is null ? null : MapToDto(escola);
    }

    public async Task<EscolaDto> CreateAsync(CreateEscolaDto dto)
    {
        var escola = new Escola
        {
            SDescricao = dto.SDescricao
        };

        var created = await _escolaRepository.CreateAsync(escola);
        return MapToDto(created);
    }

    public async Task<EscolaDto?> UpdateAsync(int id, UpdateEscolaDto dto)
    {
        var escola = new Escola
        {
            ICodEscola = id,
            SDescricao = dto.SDescricao
        };

        var updated = await _escolaRepository.UpdateAsync(escola);
        return updated is null ? null : MapToDto(updated);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        return await _escolaRepository.DeleteAsync(id);
    }

    private static EscolaDto MapToDto(Escola escola)
    {
        return new EscolaDto(
            escola.ICodEscola,
            escola.SDescricao
        );
    }
}

