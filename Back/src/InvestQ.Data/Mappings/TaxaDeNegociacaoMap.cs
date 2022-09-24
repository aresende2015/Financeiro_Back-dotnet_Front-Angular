using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InvestQ.Domain.Entities.Ativos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InvestQ.Data.Mappings
{
    public class TaxaDeNegociacaoMap : IEntityTypeConfiguration<TaxaDeNegociacao>
    {
        public void Configure(EntityTypeBuilder<TaxaDeNegociacao> builder)
        {
            builder.ToTable("TaxasDeNegociacoes");

            builder.Property(tx => tx.Percentual)
                   .HasColumnType("decimal(8,4)")
                   .IsRequired();
        }
    }
}