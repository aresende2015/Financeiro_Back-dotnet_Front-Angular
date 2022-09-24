using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InvestQ.Data.Context;
using InvestQ.Data.Interfaces.Ativos;
using InvestQ.Domain.Entities.Ativos;
using InvestQ.Domain.Enum;
using Microsoft.EntityFrameworkCore;

namespace InvestQ.Data.Repositories.Ativos
{
    public class TaxaDeNegociacaoRepo : GeralRepo, ITaxaDeNegociacaoRepo
    {
        private readonly InvestQContext _context;
        public TaxaDeNegociacaoRepo(InvestQContext context) : base(context)
        {
            _context = context;
        }
        public async Task<TaxaDeNegociacao[]> GetAllTaxaDeNegociacaoAsync()
        {
            IQueryable<TaxaDeNegociacao> query = _context.TaxasDeNegociacoes;

            query = query.AsNoTracking()
                         .OrderBy(tx => tx.Id);

            return await query.ToArrayAsync();
        }

        public async Task<TaxaDeNegociacao> GetTaxaDeNegociacaoByIdAsync(Guid id)
        {
            IQueryable<TaxaDeNegociacao> query = _context.TaxasDeNegociacoes;

            query = query.AsNoTracking()
                         .Where(tx => tx.Id == id);                         

            return await query.FirstOrDefaultAsync();
        }

        public async Task<TaxaDeNegociacao> GetTaxaDeNegociacaoByTipoDeAtivoAsync(TipoDeAtivo tipoDeAtivo, DateTime dataDeNegociacao)
        {
            IQueryable<TaxaDeNegociacao> query = _context.TaxasDeNegociacoes;                                   

            query = query.AsNoTracking()
                         .Where(tx => tx.TipoDeAtivo == tipoDeAtivo
                                    && tx.DataInicio.Date <= dataDeNegociacao.Date
                                    && ((tx.DataFim != null && tx.DataFim >= dataDeNegociacao.Date) 
                                         || (tx.DataFim == null))
                                    );            

            return await query.FirstOrDefaultAsync();
        }

        public async Task<TaxaDeNegociacao> GetTaxaDeNegociacaoByTipoDeAtivoVigenteAsync(TipoDeAtivo tipoDeAtivo)
        {
            IQueryable<TaxaDeNegociacao> query = _context.TaxasDeNegociacoes;                                   

            query = query.AsNoTracking()
                         .Where(tx => tx.TipoDeAtivo == tipoDeAtivo && tx.DataFim == null);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<TaxaDeNegociacao> GetTaxaDeNegociacaoByTipoDeAtivoComMaxDataFimAsync(TipoDeAtivo tipoDeAtivo)
        {
            IQueryable<TaxaDeNegociacao> query = _context.TaxasDeNegociacoes;                                   

            query = query.AsNoTracking()
                         .Where(tx => tx.TipoDeAtivo == tipoDeAtivo && tx.DataFim != null)
                         .OrderByDescending(tx => tx.DataFim);

            return await query.FirstOrDefaultAsync();
        }
    }
}