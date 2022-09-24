using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InvestQ.Application.Dtos.Enum;

namespace InvestQ.Application.Dtos.Ativos
{
    public class TaxaDeNegociacaoDto
    {
        public Guid Id { get; set; }
        public TipoDeAtivoDto TipoDeAtivo { get; set; }
        public Decimal Percentual { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime? DataFim { get; set; }
    }
}