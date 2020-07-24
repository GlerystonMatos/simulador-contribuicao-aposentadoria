using Microsoft.Extensions.DependencyInjection;
using SimuladorContribuicaoAposentadoria.Data.Context;

namespace SimuladorContribuicaoAposentadoria.CrossCutting
{
    public static class InjectorDependencies
    {
        public static void RegisterDependencies(this IServiceCollection services)
        {
            services.RegisterRepository();
            services.RegisterService();
            services.AddScoped<SimuladorContribuicaoAposentadoriaContext>();
        }
    }
}