using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InvestQ.Data.Context;
using InvestQ.Data.Interfaces.Ativos;
using InvestQ.Domain.Entities.Ativos;
using Microsoft.EntityFrameworkCore;

namespace InvestQ.Data.Repositories.Ativos
{
    public class ProventoRepo : GeralRepo, IProventoRepo
    {
        private readonly InvestQContext _context;
        public ProventoRepo(InvestQContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Provento[]> GetAllProventosAsync()
        {
            IQueryable<Provento> query = _context.Proventos;

            query = query.Include(p => p.Ativo);

            query = query.AsNoTracking()
                         .OrderBy(p => p.Id);

            return await query.ToArrayAsync();
        }

        public async Task<Provento[]> GetAllProventosByAtivoIdAsync(Guid ativoId)
        {
            IQueryable<Provento> query = _context.Proventos;

            query = query.Include(p => p.Ativo);

            query = query.AsNoTracking()
                         .Where(p => p.AtivoId == ativoId)
                         .OrderBy(p => p.Id);

            return await query.ToArrayAsync();
        }
        public async Task<Provento> GetProventoByIdAsync(Guid id)
        {
            IQueryable<Provento> query = _context.Proventos;

            query = query.Include(p => p.Ativo);

            query = query.AsNoTracking()
                         .Where(p => p.Id == id);                         

            return await query.FirstOrDefaultAsync();
        }
    }
}