using DesafioDexian.Application.DTOs;

namespace DesafioDexian.Application.Interfaces;

public interface IEscolaService
{
    Task<IEnumerable<EscolaDto>> GetAllAsync();
    Task<EscolaDto?> GetByIdAsync(int id);
    Task<EscolaDto> CreateAsync(CreateEscolaDto dto);
    Task<EscolaDto?> UpdateAsync(int id, UpdateEscolaDto dto);
    Task<bool> DeleteAsync(int id);
}

