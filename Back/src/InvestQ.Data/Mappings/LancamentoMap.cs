using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InvestQ.Domain.Entities.Clientes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InvestQ.Data.Mappings
{
    public class LancamentoMap : IEntityTypeConfiguration<Lancamento>
    {
        public void Configure(EntityTypeBuilder<Lancamento> builder)
        {
            builder.ToTable("Lancamentos");

            builder.Property(l=> l.ValorDaOperacao)
                   .HasColumnType("decimal(20,2)")
                   .IsRequired();

            builder.Property(l=> l.CustoB3DaOperacao)
                .HasColumnType("decimal(8,2)")
                .IsRequired();

            builder.Property(l=> l.CustoIRDaOperacao)
                .HasColumnType("decimal(8,2)")
                .IsRequired();

            builder.Property(l=> l.Quantidade)
                   .HasColumnType("decimal(20,2)")
                   .IsRequired();

            builder.Property(l=> l.QuantidadeDayTrade)
                   .HasColumnType("decimal(20,2)")
                   .IsRequired();
        }
    }
}