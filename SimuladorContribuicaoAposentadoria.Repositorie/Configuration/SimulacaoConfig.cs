using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SimuladorContribuicaoAposentadoria.Domain.Entities;

namespace SimuladorContribuicaoAposentadoria.Data.Configuration
{
    public class SimulacaoConfig : IEntityTypeConfiguration<Simulacao>
    {
        public void Configure(EntityTypeBuilder<Simulacao> builder)
        {
            builder.HasKey(s => s.Id);
            builder.Property(s => s.Salario).IsRequired();
            builder.Property(s => s.Nascimento).IsRequired();
            builder.Property(s => s.IdadeComQueComecouContribuicao).IsRequired();
            builder.Property(s => s.Sexo).IsRequired();
            builder.Property(s => s.Tipo).IsRequired();
            builder.Property(s => s.IdUsuario).IsRequired();
            builder.HasOne(s => s.Usuario).WithMany(u => u.Simulacoes).HasForeignKey(s => s.IdUsuario).OnDelete(DeleteBehavior.Cascade);
        }
    }
}