using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace SimuladorContribuicaoAposentadoria.Data.Context
{
    public class DesignSimuladorContribuicaoAposentadoriaContext : IDesignTimeDbContextFactory<SimuladorContribuicaoAposentadoriaContext>
    {
        public SimuladorContribuicaoAposentadoriaContext CreateDbContext(string[] args)
        {
            var local = "Host=127.0.0.1;Database=SimuladorContribuicaoAposentadoria;Username=postgres;Password=1234;";
            DbContextOptionsBuilder<SimuladorContribuicaoAposentadoriaContext> builder = new DbContextOptionsBuilder<SimuladorContribuicaoAposentadoriaContext>();
            builder.UseNpgsql(local);
            return new SimuladorContribuicaoAposentadoriaContext(builder.Options);
        }
    }
}