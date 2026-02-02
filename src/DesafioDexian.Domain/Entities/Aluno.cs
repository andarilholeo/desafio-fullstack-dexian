namespace DesafioDexian.Domain.Entities;

public class Aluno
{
    public int ICodAluno { get; set; }
    public string SNome { get; set; } = string.Empty;
    public DateTime DNascimento { get; set; }
    public string SCPF { get; set; } = string.Empty;
    public string SEndereco { get; set; } = string.Empty;
    public string SCelular { get; set; } = string.Empty;
    public int ICodEscola { get; set; }
}

