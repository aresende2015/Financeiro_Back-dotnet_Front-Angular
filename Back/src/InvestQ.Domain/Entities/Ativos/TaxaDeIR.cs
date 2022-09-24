using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InvestQ.Domain.Enum;

namespace InvestQ.Domain.Entities.Ativos
{
    public class TaxaDeIR : Entity
    {
        public TaxaDeIR() 
        {
        }
        public TaxaDeIR(Guid id,
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
                throw new Exception($"A taxa de IR já estava inativa.");
        }
        public void Reativar()
        {
            if (!Inativo)
                Inativo = false;
            else
                throw new Exception($"A taxa de IR já estava ativa.");
        }
        public TipoDeAtivo TipoDeAtivo { get; set; }
        public Decimal Percentual { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime? DataFim { get; set; }
    }
}