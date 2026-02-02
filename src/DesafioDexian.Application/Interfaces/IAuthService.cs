using DesafioDexian.Application.DTOs;

namespace DesafioDexian.Application.Interfaces;

public interface IAuthService
{
    Task<LoginResponseDto?> LoginAsync(LoginRequestDto request);
}

