using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SimuladorContribuicaoAposentadoria.Domain.Entities;

namespace SimuladorContribuicaoAposentadoria.Data.Configuration
{
    public class UsuarioConfig : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.HasKey(u => u.Id);
            builder.Property(u => u.Nome).IsRequired();
            builder.HasIndex(u => u.Nome).IsUnique();
            builder.Property(u => u.Senha).IsRequired();
        }
    }
}