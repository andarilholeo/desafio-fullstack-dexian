using DesafioDexian.Application.DTOs;

namespace DesafioDexian.Application.Interfaces;

public interface IAlunoService
{
    Task<IEnumerable<AlunoDto>> GetAllAsync();
    Task<AlunoDto?> GetByIdAsync(int id);
    Task<AlunoDto> CreateAsync(CreateAlunoDto dto);
    Task<AlunoDto?> UpdateAsync(int id, UpdateAlunoDto dto);
    Task<bool> DeleteAsync(int id);
    Task<IEnumerable<AlunoDto>> GetByEscolaIdAsync(int escolaId);
}

