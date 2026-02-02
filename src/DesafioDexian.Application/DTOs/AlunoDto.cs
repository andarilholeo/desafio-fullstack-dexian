namespace DesafioDexian.Application.DTOs;

public record AlunoDto(
    int ICodAluno,
    string SNome,
    DateTime DNascimento,
    string SCPF,
    string SEndereco,
    string SCelular,
    int ICodEscola
);

public record CreateAlunoDto(
    string SNome,
    DateTime DNascimento,
    string SCPF,
    string SEndereco,
    string SCelular,
    int ICodEscola
);

public record UpdateAlunoDto(
    string SNome,
    DateTime DNascimento,
    string SCPF,
    string SEndereco,
    string SCelular,
    int ICodEscola
);

