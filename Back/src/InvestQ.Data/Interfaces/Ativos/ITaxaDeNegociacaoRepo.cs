using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InvestQ.Domain.Entities.Ativos;
using InvestQ.Domain.Enum;

namespace InvestQ.Data.Interfaces.Ativos
{
    public interface ITaxaDeNegociacaoRepo  : IGeralRepo
    {
        Task<TaxaDeNegociacao[]> GetAllTaxaDeNegociacaoAsync();
        Task<TaxaDeNegociacao> GetTaxaDeNegociacaoByTipoDeAtivoAsync(TipoDeAtivo tipoDeAtivo, DateTime dataDeNegociacao);
        Task<TaxaDeNegociacao> GetTaxaDeNegociacaoByTipoDeAtivoVigenteAsync(TipoDeAtivo tipoDeAtivo);
        Task<TaxaDeNegociacao> GetTaxaDeNegociacaoByTipoDeAtivoComMaxDataFimAsync(TipoDeAtivo tipoDeAtivo);        
        Task<TaxaDeNegociacao> GetTaxaDeNegociacaoByIdAsync(Guid id);
    }
}