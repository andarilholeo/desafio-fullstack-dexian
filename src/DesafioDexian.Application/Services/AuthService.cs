using DesafioDexian.Application.DTOs;
using DesafioDexian.Application.Interfaces;
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

    public async Task<LoginResponseDto?> LoginAsync(LoginRequestDto request)
    {
        var usuario = await _usuarioRepository.GetByNomeAsync(request.SNome);

        if (usuario is null || usuario.SSenha != request.SSenha)
        {
            return null;
        }

        var token = _tokenService.GenerateToken(usuario);
        var expiration = DateTime.UtcNow.AddHours(2);

        return new LoginResponseDto(token, usuario.SNome, expiration);
    }
}

