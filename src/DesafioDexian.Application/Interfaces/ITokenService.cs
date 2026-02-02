using DesafioDexian.Domain.Entities;

namespace DesafioDexian.Application.Interfaces;

public interface ITokenService
{
    string GenerateToken(Usuario usuario);
}

