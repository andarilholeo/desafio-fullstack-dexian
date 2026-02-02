using DesafioDexian.Application.Interfaces;
using DesafioDexian.Application.Services;
using DesafioDexian.Domain.Interfaces;
using DesafioDexian.Infrastructure.Data;
using DesafioDexian.Infrastructure.Repositories;
using DesafioDexian.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace DesafioDexian.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        // Data Store (Singleton para manter dados em memória)
        services.AddSingleton<InMemoryDataStore>();

        // Repositories
        services.AddScoped<IAlunoRepository, AlunoRepository>();
        services.AddScoped<IEscolaRepository, EscolaRepository>();
        services.AddScoped<IUsuarioRepository, UsuarioRepository>();

        // Services
        services.AddScoped<IAlunoService, AlunoService>();
        services.AddScoped<IEscolaService, EscolaService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<ITokenService, TokenService>();

        return services;
    }
}

