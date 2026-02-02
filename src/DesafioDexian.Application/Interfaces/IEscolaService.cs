using DesafioDexian.Application.DTOs;
using DesafioDexian.Domain.Common;

namespace DesafioDexian.Application.Interfaces;

public interface IEscolaService
{
    Task<Result<IEnumerable<EscolaDto>>> GetAllAsync();
    Task<Result<EscolaDto>> GetByIdAsync(int id);
    Task<Result<EscolaDto>> CreateAsync(CreateEscolaDto dto);
    Task<Result<EscolaDto>> UpdateAsync(int id, UpdateEscolaDto dto);
    Task<Result> DeleteAsync(int id);
}

