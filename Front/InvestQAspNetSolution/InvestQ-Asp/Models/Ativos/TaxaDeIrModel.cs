using InvestQ_Asp.Models.Enum;

namespace InvestQ_Asp.Models.Ativos
{
    public class TaxaDeIrModel
    {
        public Guid Id { get; set; }
        public TipoDeAtivo TipoDeAtivo { get; set; }
        public Decimal Percentual { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime? DataFim { get; set; }

    }
}
