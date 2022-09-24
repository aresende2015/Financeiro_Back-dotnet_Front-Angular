using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InvestQ.Domain.Entities.Ativos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InvestQ.Data.Mappings
{
    public class TaxaDeIRMap : IEntityTypeConfiguration<TaxaDeIR>
    {
        public void Configure(EntityTypeBuilder<TaxaDeIR> builder)
        {
            builder.ToTable("TaxasDeIR");

            builder.Property(tx => tx.Percentual)
                   .HasColumnType("decimal(8,4)")
                   .IsRequired();
        }
    }
}