using Microsoft.Extensions.DependencyInjection;
using SimuladorContribuicaoAposentadoria.Domain.Interfaces.Services;
using SimuladorContribuicaoAposentadoria.Service.Services;

namespace SimuladorContribuicaoAposentadoria.CrossCutting
{
    public static class InjectorService
    {
        public static void RegisterService(this IServiceCollection services)
        {
            services.AddScoped<IUsuarioService, UsuarioService>();
            services.AddScoped<ISimulacaoService, SimulacaoService>();
            services.AddScoped<IResultadoService, ResultadoService>();
        }
    }
}