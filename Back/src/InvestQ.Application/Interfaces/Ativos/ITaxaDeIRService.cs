using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InvestQ.Application.Dtos.Ativos;
using InvestQ.Application.Dtos.Enum;

namespace InvestQ.Application.Interfaces.Ativos
{
    public interface ITaxaDeIRService
    {
        Task<TaxaDeIRDto> AdicionarTaxaDeIR(TaxaDeIRDto model);
        Task<bool> DeletarTaxaDeIR(Guid taxaDeIRId);

        Task<bool> InativarTaxaDeIR(TaxaDeIRDto model);
        Task<bool> ReativarTaxaDeIR(TaxaDeIRDto model);

        Task<TaxaDeIRDto[]> GetAllTaxaDeIRAsync();
        Task<TaxaDeIRDto> GetTaxaDeIRByTipoDeAtivoAsync(TipoDeAtivoDto tipoDeAtivoDto, DateTime dataDeNegociacao);
        Task<TaxaDeIRDto> GetTaxaDeIRByIdAsync(Guid id);
    }
}