using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InvestQ.Data.Context;
using InvestQ.Data.Interfaces.FundosImobiliarios;
using InvestQ.Domain.Entities.FundosImobiliarios;
using Microsoft.EntityFrameworkCore;

namespace InvestQ.Data.Repositories.FundosImobiliarios
{
    public class AdministradorDeFundoImobiliarioRepo : GeralRepo, IAdministradorDeFundoImobiliarioRepo
    {
        private readonly InvestQContext _context;
        public AdministradorDeFundoImobiliarioRepo(InvestQContext context) : base(context)
        {
            _context = context;
        }
        public async Task<AdministradorDeFundoImobiliario[]> GetAllAdministradoresDeFundosImobiliariosAsync(bool includeFundoImobiliario)
        {
            IQueryable<AdministradorDeFundoImobiliario> query = _context.AdministradoresDeFundosImobiliarios;

            if (includeFundoImobiliario)
                query = query.Include(afi => afi.FundosImobiliarios);

            query = query.AsNoTracking()
                         .OrderBy(afi => afi.Id);

            return await query.ToArrayAsync();
        }
        public async Task<AdministradorDeFundoImobiliario> GetAdministradorDeFundoImobiliarioByIdAsync(Guid id, bool includeFundoImobiliario)
        {
            IQueryable<AdministradorDeFundoImobiliario> query = _context.AdministradoresDeFundosImobiliarios;

            if (includeFundoImobiliario)
                query = query.Include(afi => afi.FundosImobiliarios);

            query = query.AsNoTracking()
                         .Where(afi => afi.Id == id)
                         .OrderBy(afi => afi.Id);
                         

            return await query.FirstOrDefaultAsync();
        }


    }
}