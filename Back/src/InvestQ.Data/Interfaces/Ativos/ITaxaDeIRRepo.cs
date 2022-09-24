using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InvestQ.Domain.Entities.Ativos;
using InvestQ.Domain.Enum;

namespace InvestQ.Data.Interfaces.Ativos
{
    public interface ITaxaDeIRRepo  : IGeralRepo
    {
        Task<TaxaDeIR[]> GetAllTaxaDeIRAsync();
        Task<TaxaDeIR> GetTaxaDeIRByTipoDeAtivoAsync(TipoDeAtivo tipoDeAtivo, DateTime dataDeNegociacao);
        Task<TaxaDeIR> GetTaxaDeIRByTipoDeAtivoVigenteAsync(TipoDeAtivo tipoDeAtivo);
        Task<TaxaDeIR> GetTaxaDeIRByTipoDeAtivoComMaxDataFimAsync(TipoDeAtivo tipoDeAtivo); 
        Task<TaxaDeIR> GetTaxaDeIRByIdAsync(Guid id);
    }
}