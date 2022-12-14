using System;
using System.Linq;
using System.Threading.Tasks;
using InvestQ.Data.Context;
using InvestQ.Data.Interfaces.Acoes;
using InvestQ.Domain.Entities.Acoes;
using Microsoft.EntityFrameworkCore;

namespace InvestQ.Data.Repositories.Acoes
{
    public class AcaoRepo : GeralRepo, IAcaoRepo
    {
        private readonly InvestQContext _context;
        public AcaoRepo(InvestQContext context) : base(context)
        {
            _context = context;
        }
        public async Task<Acao> GetAcaoByDescricaoAsync(string descricao)
        {
            IQueryable<Acao> query = _context.Acoes;

            query = query.Include(a => a.Segmento)
                         .Include(a => a.TipoDeInvestimento);

            query = query.AsNoTracking()
                         .Where(a => a.Descricao == descricao);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<Acao> GetAcaoByIdAsync(Guid id)
        {
            IQueryable<Acao> query = _context.Acoes;

            query = query.Include(a => a.Segmento)
                         .Include(a => a.TipoDeInvestimento);

            query = query.AsNoTracking()
                         .Where(a => a.Id == id);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<Acao[]> GetAcoesBySegmentoIdAsync(Guid segmentoId)
        {
            IQueryable<Acao> query = _context.Acoes;

            query = query.Include(fi => fi.Segmento)
                         .Include(fi => fi.TipoDeInvestimento);

            query = query.AsNoTracking()
                         .Where(fi => fi.SegmentoId == segmentoId);

            return await query.ToArrayAsync();
        }

        public async Task<Acao[]> GetAcoesByTipoDeInvestimentoIdAsync(Guid tipoDeInvestimentoId)
        {
            IQueryable<Acao> query = _context.Acoes;

            query = query.Include(a => a.Segmento)
                         .Include(a => a.TipoDeInvestimento);

            query = query.AsNoTracking()
                         .Where(a => a.TipoDeInvestimentoId == tipoDeInvestimentoId);

            return await query.ToArrayAsync();
        }

        public async Task<Acao[]> GetAllAcoesAsync()
        {
            IQueryable<Acao> query = _context.Acoes;

            query = query.Include(a => a.Segmento)
                         .Include(a => a.TipoDeInvestimento);

            query = query.AsNoTracking()
                         .OrderBy(a => a.Id);

            return await query.ToArrayAsync();
        }
    }
}