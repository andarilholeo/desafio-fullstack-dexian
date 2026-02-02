using DesafioDexian.Domain.Entities;
using DesafioDexian.Domain.Interfaces;
using DesafioDexian.Infrastructure.Data;

namespace DesafioDexian.Infrastructure.Repositories;

public class AlunoRepository : IAlunoRepository
{
    private readonly InMemoryDataStore _dataStore;

    public AlunoRepository(InMemoryDataStore dataStore)
    {
        _dataStore = dataStore;
    }

    public Task<IEnumerable<Aluno>> GetAllAsync()
    {
        return Task.FromResult<IEnumerable<Aluno>>(_dataStore.Alunos.ToList());
    }

    public Task<Aluno?> GetByIdAsync(int id)
    {
        var aluno = _dataStore.Alunos.FirstOrDefault(a => a.ICodAluno == id);
        return Task.FromResult(aluno);
    }

    public Task<Aluno> CreateAsync(Aluno aluno)
    {
        aluno.ICodAluno = _dataStore.GetNextAlunoId();
        _dataStore.Alunos.Add(aluno);
        return Task.FromResult(aluno);
    }

    public Task<Aluno?> UpdateAsync(Aluno aluno)
    {
        var existing = _dataStore.Alunos.FirstOrDefault(a => a.ICodAluno == aluno.ICodAluno);
        if (existing is null)
        {
            return Task.FromResult<Aluno?>(null);
        }

        existing.SNome = aluno.SNome;
        existing.DNascimento = aluno.DNascimento;
        existing.SCPF = aluno.SCPF;
        existing.SEndereco = aluno.SEndereco;
        existing.SCelular = aluno.SCelular;
        existing.ICodEscola = aluno.ICodEscola;

        return Task.FromResult<Aluno?>(existing);
    }

    public Task<bool> DeleteAsync(int id)
    {
        var aluno = _dataStore.Alunos.FirstOrDefault(a => a.ICodAluno == id);
        if (aluno is null)
        {
            return Task.FromResult(false);
        }

        _dataStore.Alunos.Remove(aluno);
        return Task.FromResult(true);
    }

    public Task<IEnumerable<Aluno>> GetByEscolaIdAsync(int escolaId)
    {
        var alunos = _dataStore.Alunos.Where(a => a.ICodEscola == escolaId).ToList();
        return Task.FromResult<IEnumerable<Aluno>>(alunos);
    }
}

