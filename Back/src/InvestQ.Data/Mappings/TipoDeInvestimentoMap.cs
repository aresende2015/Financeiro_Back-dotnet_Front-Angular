using InvestQ.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InvestQ.Data.Mappings
{
    public class TipoDeInvestimentoMap : IEntityTypeConfiguration<TipoDeInvestimento>
    {
        public void Configure(EntityTypeBuilder<TipoDeInvestimento> builder)
        {
            builder.ToTable("TiposDeInvestimentos");

            builder.HasMany(ti => ti.FundosImobiliarios)
                    .WithOne(fi => fi.TipoDeInvestimento)
                    .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(ti => ti.TesourosDiretos)
                    .WithOne(td => td.TipoDeInvestimento)
                    .OnDelete(DeleteBehavior.Cascade); 

            builder.HasMany(ti => ti.Acoes)
                    .WithOne(a => a.TipoDeInvestimento)
                    .OnDelete(DeleteBehavior.Cascade);

            builder
                .Property(ti => ti.Descricao)
                .IsRequired()
                .HasMaxLength(50);
        }
    }
}