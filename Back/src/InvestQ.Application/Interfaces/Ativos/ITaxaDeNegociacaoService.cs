using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InvestQ.Application.Dtos.Ativos;
using InvestQ.Application.Dtos.Enum;

namespace InvestQ.Application.Interfaces.Ativos
{
    public interface ITaxaDeNegociacaoService
    {
        Task<TaxaDeNegociacaoDto> AdicionarTaxaDeNegociacao(TaxaDeNegociacaoDto model);
        Task<bool> DeletarTaxaDeNegociacao(Guid taxaDeNegociacaoId);
        
        Task<bool> InativarTaxaDeNegociacao(TaxaDeNegociacaoDto model);
        Task<bool> ReativarTaxaDeNegociacao(TaxaDeNegociacaoDto model);

        Task<TaxaDeNegociacaoDto[]> GetAllTaxaDeNegociacaoAsync();
        Task<TaxaDeNegociacaoDto> GetTaxaDeNegociacaoByTipoDeAtivoAsync(TipoDeAtivoDto tipoDeAtivoDto, DateTime dataDeNegociacao);
        Task<TaxaDeNegociacaoDto> GetTaxaDeNegociacaoByIdAsync(Guid id);
    }
}