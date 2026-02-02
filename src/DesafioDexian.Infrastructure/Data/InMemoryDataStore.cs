using DesafioDexian.Domain.Entities;

namespace DesafioDexian.Infrastructure.Data;

public class InMemoryDataStore
{
    public List<Aluno> Alunos { get; } = new();
    public List<Escola> Escolas { get; } = new();
    public List<Usuario> Usuarios { get; } = new();

    private int _nextAlunoId = 1;
    private int _nextEscolaId = 1;
    private int _nextUsuarioId = 1;

    public InMemoryDataStore()
    {
        SeedData();
    }

    private void SeedData()
    {
        // Seed Escolas
        Escolas.Add(new Escola { ICodEscola = _nextEscolaId++, SDescricao = "Colégio Estadual Teotônio Brandão Vilela" });

        // Seed Alunos
        Alunos.Add(new Aluno
        {
            ICodAluno = _nextAlunoId++,
            SNome = "Leonardo Santos Silva",
            DNascimento = new DateTime(1995, 4, 19),
            SCPF = "123.456.789-00",
            SEndereco = "Rua das Flores, 123",
            SCelular = "(11) 99999-1111",
            ICodEscola = 1
        });

        // Seed Usuarios
        Usuarios.Add(new Usuario
        {
            ICodUsuario = _nextUsuarioId++,
            SNome = "admin",
            SSenha = "admin123"
        });

        Usuarios.Add(new Usuario
        {
            ICodUsuario = _nextUsuarioId++,
            SNome = "TESTE",
            SSenha = "123"
        });
    }

    public int GetNextAlunoId() => _nextAlunoId++;
    public int GetNextEscolaId() => _nextEscolaId++;
    public int GetNextUsuarioId() => _nextUsuarioId++;
}

