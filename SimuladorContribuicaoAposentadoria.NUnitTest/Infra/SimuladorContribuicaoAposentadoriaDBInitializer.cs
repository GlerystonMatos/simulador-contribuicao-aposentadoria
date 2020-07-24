using SimuladorContribuicaoAposentadoria.Data.Context;

namespace SimuladorContribuicaoAposentadoriaNUnitTest.Infra
{
    public class SimuladorContribuicaoAposentadoriaDBInitializer
    {
        public SimuladorContribuicaoAposentadoriaDBInitializer()
        {
        }

        public void Seed(SimuladorContribuicaoAposentadoriaContext context)
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            context.SaveChanges();
        }
    }
}