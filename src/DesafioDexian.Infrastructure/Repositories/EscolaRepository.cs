using DesafioDexian.Domain.Entities;
using DesafioDexian.Domain.Interfaces;
using DesafioDexian.Infrastructure.Data;

namespace DesafioDexian.Infrastructure.Repositories;

public class EscolaRepository : IEscolaRepository
{
    private readonly InMemoryDataStore _dataStore;

    public EscolaRepository(InMemoryDataStore dataStore)
    {
        _dataStore = dataStore;
    }

    public Task<IEnumerable<Escola>> GetAllAsync()
    {
        return Task.FromResult<IEnumerable<Escola>>(_dataStore.Escolas.ToList());
    }

    public Task<Escola?> GetByIdAsync(int id)
    {
        var escola = _dataStore.Escolas.FirstOrDefault(e => e.ICodEscola == id);
        return Task.FromResult(escola);
    }

    public Task<Escola> CreateAsync(Escola escola)
    {
        escola.ICodEscola = _dataStore.GetNextEscolaId();
        _dataStore.Escolas.Add(escola);
        return Task.FromResult(escola);
    }

    public Task<Escola?> UpdateAsync(Escola escola)
    {
        var existing = _dataStore.Escolas.FirstOrDefault(e => e.ICodEscola == escola.ICodEscola);
        if (existing is null)
        {
            return Task.FromResult<Escola?>(null);
        }

        existing.SDescricao = escola.SDescricao;

        return Task.FromResult<Escola?>(existing);
    }

    public Task<bool> DeleteAsync(int id)
    {
        var escola = _dataStore.Escolas.FirstOrDefault(e => e.ICodEscola == id);
        if (escola is null)
        {
            return Task.FromResult(false);
        }

        _dataStore.Escolas.Remove(escola);
        return Task.FromResult(true);
    }
}

