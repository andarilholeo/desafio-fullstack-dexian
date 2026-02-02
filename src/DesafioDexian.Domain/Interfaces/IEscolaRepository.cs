using DesafioDexian.Domain.Entities;

namespace DesafioDexian.Domain.Interfaces;

public interface IEscolaRepository
{
    Task<IEnumerable<Escola>> GetAllAsync();
    Task<Escola?> GetByIdAsync(int id);
    Task<Escola> CreateAsync(Escola escola);
    Task<Escola?> UpdateAsync(Escola escola);
    Task<bool> DeleteAsync(int id);
}

