using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InvestQ.Domain.Enum;

namespace InvestQ.Domain.Entities.Ativos
{
    public class TaxaDeNegociacao : Entity
    {
        public TaxaDeNegociacao() 
        {
        }
        public TaxaDeNegociacao(Guid id,
                                TipoDeAtivo tipoDeAtivo,
                                Decimal percentual,
                                DateTime dataInicio,
                                DateTime? dataFim)
        {
            Id = id;
            TipoDeAtivo = tipoDeAtivo;
            Percentual = percentual;
            DataInicio = dataInicio;
            DataFim = dataFim;
        }
        public void Inativar()
        {
            if (Inativo)
                Inativo = true;
            else
                throw new Exception($"O Tipo de Investimento já estava inativo.");
        }
        public void Reativar()
        {
            if (!Inativo)
                Inativo = false;
            else
                throw new Exception($"O Tipo de Investimento já estava ativo.");
        }

        public TipoDeAtivo TipoDeAtivo { get; set; }
        public Decimal Percentual { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime? DataFim { get; set; }
        
    }
}