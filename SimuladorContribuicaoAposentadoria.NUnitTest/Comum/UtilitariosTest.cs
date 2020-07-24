using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SimuladorContribuicaoAposentadoria.Data.Context;
using SimuladorContribuicaoAposentadoria.Service.AutoMapper;
using SimuladorContribuicaoAposentadoriaNUnitTest.Infra;

namespace SimuladorContribuicaoAposentadoriaNUnitTest.Comum
{
    public class UtilitariosTest
    {
        private static SimuladorContribuicaoAposentadoriaContext simuladorContribuicaoAposentadoriaContext;

        public static SimuladorContribuicaoAposentadoriaContext GetContext()
        {
            if (simuladorContribuicaoAposentadoriaContext == null)
            {
                DbContextOptions<SimuladorContribuicaoAposentadoriaContext> dbContextOptions = new DbContextOptionsBuilder<SimuladorContribuicaoAposentadoriaContext>()
                    .UseNpgsql("Host=127.0.0.1;Database=SimuladorContribuicaoAposentadoria;Username=postgres;Password=1234;")
                    .Options;

                simuladorContribuicaoAposentadoriaContext = new SimuladorContribuicaoAposentadoriaContext(dbContextOptions);
                SimuladorContribuicaoAposentadoriaDBInitializer simuladorContribuicaoAposentadoriaDBInitializer = new SimuladorContribuicaoAposentadoriaDBInitializer();
                simuladorContribuicaoAposentadoriaDBInitializer.Seed(simuladorContribuicaoAposentadoriaContext);
            }

            return simuladorContribuicaoAposentadoriaContext;
        }

        public static IMapper GetMapper()
        {
            MapperConfiguration mapperConfiguration = new MapperConfiguration(cfg => cfg.AddProfile(new AutoMapping()));
            return mapperConfiguration.CreateMapper();
        }
    }
}