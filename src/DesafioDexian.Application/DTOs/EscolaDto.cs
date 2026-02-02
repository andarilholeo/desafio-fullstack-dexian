namespace DesafioDexian.Application.DTOs;

public record EscolaDto(
    int ICodEscola,
    string SDescricao
);

public record CreateEscolaDto(
    string SDescricao
);

public record UpdateEscolaDto(
    string SDescricao
);

