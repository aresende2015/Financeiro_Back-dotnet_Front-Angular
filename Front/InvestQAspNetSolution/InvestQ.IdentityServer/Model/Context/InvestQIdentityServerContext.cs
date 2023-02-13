using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace InvestQ.IdentityServer.Model.Context
{
    public class InvestQIdentityServerContext : IdentityDbContext<ApplicationUser>
    {
        public InvestQIdentityServerContext(DbContextOptions<InvestQIdentityServerContext> options)
            : base(options) { }
    }
}
