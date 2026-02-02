using DesafioDexian.Domain.Entities;
using DesafioDexian.Domain.Interfaces;
using DesafioDexian.Infrastructure.Data;

namespace DesafioDexian.Infrastructure.Repositories;

public class UsuarioRepository : IUsuarioRepository
{
    private readonly InMemoryDataStore _dataStore;

    public UsuarioRepository(InMemoryDataStore dataStore)
    {
        _dataStore = dataStore;
    }

    public Task<Usuario?> GetByNomeAsync(string nome)
    {
        var usuario = _dataStore.Usuarios.FirstOrDefault(u => u.SNome == nome);
        return Task.FromResult(usuario);
    }

    public Task<Usuario?> GetByIdAsync(int id)
    {
        var usuario = _dataStore.Usuarios.FirstOrDefault(u => u.ICodUsuario == id);
        return Task.FromResult(usuario);
    }

    public Task<Usuario> CreateAsync(Usuario usuario)
    {
        usuario.ICodUsuario = _dataStore.GetNextUsuarioId();
        _dataStore.Usuarios.Add(usuario);
        return Task.FromResult(usuario);
    }
}

