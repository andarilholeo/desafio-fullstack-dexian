using DesafioDexian.Application.DTOs;
using DesafioDexian.Domain.Common;

namespace DesafioDexian.Application.Interfaces;

public interface IAlunoService
{
    Task<Result<IEnumerable<AlunoDto>>> GetAllAsync();
    Task<Result<AlunoDto>> GetByIdAsync(int id);
    Task<Result<AlunoDto>> CreateAsync(CreateAlunoDto dto);
    Task<Result<AlunoDto>> UpdateAsync(int id, UpdateAlunoDto dto);
    Task<Result> DeleteAsync(int id);
    Task<Result<IEnumerable<AlunoDto>>> GetByEscolaIdAsync(int escolaId);
}

