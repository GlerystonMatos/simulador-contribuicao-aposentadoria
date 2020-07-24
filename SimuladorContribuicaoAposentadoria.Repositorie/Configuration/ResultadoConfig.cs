using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SimuladorContribuicaoAposentadoria.Domain.Entities;

namespace SimuladorContribuicaoAposentadoria.Data.Configuration
{
    public class ResultadoConfig : IEntityTypeConfiguration<Resultado>
    {
        public void Configure(EntityTypeBuilder<Resultado> builder)
        {
            builder.HasKey(r => r.Id);
            builder.Property(r => r.QuantidadeContribuicoesParaPagarIniciandoMesAtual).IsRequired();
            builder.Property(r => r.DataUltimaContribuicao).IsRequired();
            builder.Property(r => r.ValorPagoMes).IsRequired();
            builder.Property(r => r.PercentualAplicadoParaCalculo).IsRequired();
            builder.Property(r => r.IdSimulacao).IsRequired();
            builder.HasOne(r => r.Simulacao).WithOne(s => s.Resultado).HasForeignKey<Resultado>(r => r.IdSimulacao).OnDelete(DeleteBehavior.Cascade);
        }
    }
}