using DesafioDexian.Application.DTOs;
using DesafioDexian.Domain.Common;

namespace DesafioDexian.Application.Interfaces;

public interface IAuthService
{
    Task<Result<LoginResponseDto>> LoginAsync(LoginRequestDto request);
}

