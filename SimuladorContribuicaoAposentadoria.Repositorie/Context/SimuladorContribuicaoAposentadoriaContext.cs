using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SimuladorContribuicaoAposentadoria.Data.Configuration;
using SimuladorContribuicaoAposentadoria.Domain.Entities;

namespace SimuladorContribuicaoAposentadoria.Data.Context
{
    public class SimuladorContribuicaoAposentadoriaContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public SimuladorContribuicaoAposentadoriaContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public SimuladorContribuicaoAposentadoriaContext(DbContextOptions<SimuladorContribuicaoAposentadoriaContext> options) : base(options)
        {
        }

        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<Simulacao> Simulacao { get; set; }
        public DbSet<Resultado> Resultado { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new UsuarioConfig());
            modelBuilder.ApplyConfiguration(new SimulacaoConfig());
            modelBuilder.ApplyConfiguration(new ResultadoConfig());

            modelBuilder.Entity<Usuario>().HasData(
                new Usuario("admin", "123"));
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (_configuration != null)
            {
                optionsBuilder.UseNpgsql(_configuration.GetConnectionString("DefaultConnection"));
            }
        }
    }
}