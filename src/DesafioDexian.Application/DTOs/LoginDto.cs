namespace DesafioDexian.Application.DTOs;

public record LoginRequestDto(
    string SNome,
    string SSenha
);

public record LoginResponseDto(
    string Token,
    string Nome,
    DateTime Expiration
);

