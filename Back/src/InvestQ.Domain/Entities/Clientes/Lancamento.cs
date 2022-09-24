using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InvestQ.Domain.Entities.Ativos;
using InvestQ.Domain.Enum;

namespace InvestQ.Domain.Entities.Clientes
{
    public class Lancamento : Entity
    {
        public Lancamento() 
        {
        }
        public Lancamento(Decimal valorDaOperacao,
                          Decimal custoB3DaOperacao,
                          Decimal custoIRDaOperacao,
                          DateTime dataDaOperacao, 
                          Decimal quantidade,
                          Decimal quantidadeDayTrade,
                          bool contabilizado,
                          TipoDeMovimentacao tipoDeMovimentacao,
                          TipoDeOperacao tipoDeOperacao,
                          Guid ativoId,
                          Guid carteiraId) 
        {
            ValorDaOperacao = valorDaOperacao;
            CustoB3DaOperacao = custoB3DaOperacao;
            CustoIRDaOperacao = custoIRDaOperacao;
            DataDaOperacao = dataDaOperacao;
            Quantidade = quantidade;
            QuantidadeDayTrade = quantidadeDayTrade;
            Contabilizado = contabilizado;
            TipoDeMovimentacao = tipoDeMovimentacao;
            TipoDeOperacao = tipoDeOperacao;
            AtivoId = ativoId;
            CarteiraId = carteiraId;
        }
        public decimal ValorDaOperacao { get; set; }
        public decimal CustoB3DaOperacao { get; set; }
        public decimal CustoIRDaOperacao { get; set; }
        public DateTime DataDaOperacao { get; set; }
        public Decimal Quantidade { get; set; }
        public Decimal QuantidadeDayTrade { get; set; }
        public bool Contabilizado { get; set; }
        public TipoDeMovimentacao TipoDeMovimentacao { get; set; }
        public TipoDeOperacao TipoDeOperacao { get; set; }
        public Guid AtivoId { get; set; }
        public Ativo Ativo { get; set; }
        public Guid CarteiraId { get; set; }
        public Carteira Carteira { get; set; }

    }
}