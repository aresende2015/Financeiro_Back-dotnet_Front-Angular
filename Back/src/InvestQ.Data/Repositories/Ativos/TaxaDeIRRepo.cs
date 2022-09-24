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
    public class TaxaDeIRRepo : GeralRepo, ITaxaDeIRRepo
    {
        private readonly InvestQContext _context;
        public TaxaDeIRRepo(InvestQContext context) : base(context)
        {
            _context = context;
        }

        public async Task<TaxaDeIR[]> GetAllTaxaDeIRAsync()
        {
            IQueryable<TaxaDeIR> query = _context.TaxasDeIR;

            query = query.AsNoTracking()                        
                         .OrderBy(tx => tx.Id);

            return await query.ToArrayAsync();
        }

        public async Task<TaxaDeIR> GetTaxaDeIRByIdAsync(Guid id)
        {
            IQueryable<TaxaDeIR> query = _context.TaxasDeIR;

            query = query.AsNoTracking()
                         .Where(tx => tx.Id == id) ;                    

            return await query.FirstOrDefaultAsync();
        }

        public async Task<TaxaDeIR> GetTaxaDeIRByTipoDeAtivoAsync(TipoDeAtivo tipoDeAtivo, DateTime dataDeNegociacao)
        {
            IQueryable<TaxaDeIR> query = _context.TaxasDeIR;                                   

            query = query.AsNoTracking()
                         .Where(tx => tx.TipoDeAtivo == tipoDeAtivo
                                    && tx.DataInicio.Date <= dataDeNegociacao.Date
                                    && ((tx.DataFim != null && tx.DataFim >= dataDeNegociacao.Date) 
                                         || (tx.DataFim == null))
                                    );            

            return await query.FirstOrDefaultAsync();
        }

        public async Task<TaxaDeIR> GetTaxaDeIRByTipoDeAtivoComMaxDataFimAsync(TipoDeAtivo tipoDeAtivo)
        {
            IQueryable<TaxaDeIR> query = _context.TaxasDeIR;                                   

            query = query.AsNoTracking()
                         .Where(tx => tx.TipoDeAtivo == tipoDeAtivo && tx.DataFim != null)
                         .OrderByDescending(tx => tx.DataFim);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<TaxaDeIR> GetTaxaDeIRByTipoDeAtivoVigenteAsync(TipoDeAtivo tipoDeAtivo)
        {
            IQueryable<TaxaDeIR> query = _context.TaxasDeIR;                                   

            query = query.AsNoTracking()
                         .Where(tx => tx.TipoDeAtivo == tipoDeAtivo && tx.DataFim == null);

            return await query.FirstOrDefaultAsync();
        }
    }
}