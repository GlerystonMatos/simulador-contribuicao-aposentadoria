using Microsoft.Extensions.DependencyInjection;
using SimuladorContribuicaoAposentadoria.Data.Repositories;
using SimuladorContribuicaoAposentadoria.Domain.Interfaces.Data;

namespace SimuladorContribuicaoAposentadoria.CrossCutting
{
    public static class InjectorRepository
    {
        public static void RegisterRepository(this IServiceCollection services)
        {
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            services.AddScoped<ISimulacaoRepository, SimulacaoRepository>();
            services.AddScoped<IResultadoRepository, ResultadoRepository>();
        }
    }
}