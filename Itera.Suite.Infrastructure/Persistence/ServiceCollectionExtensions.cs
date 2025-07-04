using Itera.Suite.Application.Handlers.ProjetosDeViagem;
using Itera.Suite.Domain.Interfaces;
using Itera.Suite.Infrastructure.Data;
using Itera.Suite.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Itera.Suite.Infrastructure;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(connectionString));

        // Aqui você poderá registrar os repositórios futuramente, por exemplo:
        // services.AddScoped<IProjetoRepository, ProjetoRepository>();
        services.AddScoped<IProjetoDeViagemRepository, ProjetoDeViagemRepository>();
        services.AddScoped<CriarProjetoDeViagemCommandHandler>();

        return services;
    }
}
