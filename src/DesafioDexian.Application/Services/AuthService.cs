using DesafioDexian.Application.DTOs;
using DesafioDexian.Application.Interfaces;
using DesafioDexian.Domain.Common;
using DesafioDexian.Domain.Interfaces;

namespace DesafioDexian.Application.Services;

public class AuthService : IAuthService
{
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly ITokenService _tokenService;

    public AuthService(IUsuarioRepository usuarioRepository, ITokenService tokenService)
    {
        _usuarioRepository = usuarioRepository;
        _tokenService = tokenService;
    }

    public async Task<Result<LoginResponseDto>> LoginAsync(LoginRequestDto request)
    {
        var usuario = await _usuarioRepository.GetByNomeAsync(request.SNome);

        if (usuario is null || usuario.SSenha != request.SSenha)
        {
            return Result.Failure<LoginResponseDto>("Usuário ou senha inválidos", ResultErrorCode.Unauthorized);
        }

        var token = _tokenService.GenerateToken(usuario);
        var expiration = DateTime.UtcNow.AddHours(2);

        return Result.Success(new LoginResponseDto(token, usuario.SNome, expiration));
    }
}

