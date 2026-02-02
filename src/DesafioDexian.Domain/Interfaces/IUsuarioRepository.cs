using DesafioDexian.Domain.Entities;

namespace DesafioDexian.Domain.Interfaces;

public interface IUsuarioRepository
{
    Task<Usuario?> GetByNomeAsync(string nome);
    Task<Usuario?> GetByIdAsync(int id);
    Task<Usuario> CreateAsync(Usuario usuario);
}

