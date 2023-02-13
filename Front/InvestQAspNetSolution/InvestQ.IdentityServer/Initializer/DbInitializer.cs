using IdentityModel;
using InvestQ.IdentityServer.Configuration;
using InvestQ.IdentityServer.Model;
using InvestQ.IdentityServer.Model.Context;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace InvestQ.IdentityServer.Initializer
{
    public class DbInitializer : IDbInitializer
    {
        private readonly InvestQIdentityServerContext _context;
        private readonly UserManager<ApplicationUser> _user;
        private readonly RoleManager<IdentityRole> _role;

        public DbInitializer(InvestQIdentityServerContext context, 
                             UserManager<ApplicationUser> user, 
                             RoleManager<IdentityRole> role)
        {
            _context = context;
            _user = user;
            _role = role;
        }

        public void Initialize()
        {
            if (_role.FindByNameAsync(IdentityConfiguration.Admin).Result != null) return;

            _role.CreateAsync(new IdentityRole(IdentityConfiguration.Admin)).GetAwaiter().GetResult();
            _role.CreateAsync(new IdentityRole(IdentityConfiguration.Client)).GetAwaiter().GetResult();

            ApplicationUser admin = new ApplicationUser()
            {
                UserName = "AlexResende",
                Email = "alex.q.resende@gmail.com",
                EmailConfirmed = true,
                PhoneNumber = "+55 (21) 12345-6789",
                FirstName = "Alex",
                LastName = "Resende"
            };

            _user.CreateAsync(admin, "Alex123@").GetAwaiter().GetResult();

            _user.AddToRoleAsync(admin, IdentityConfiguration.Admin).GetAwaiter().GetResult();

            var adminClaims = _user.AddClaimsAsync(admin, new Claim[]
            {
                new Claim(JwtClaimTypes.Name, $"{admin.FirstName} {admin.LastName}"),
                new Claim(JwtClaimTypes.GivenName, admin.FirstName),
                new Claim(JwtClaimTypes.FamilyName, admin.LastName),
                new Claim(JwtClaimTypes.Role, IdentityConfiguration.Admin)
            }).Result;

            ApplicationUser client = new ApplicationUser()
            {
                UserName = "CaioResende",
                Email = "caio.q.resende@gmail.com",
                EmailConfirmed = true,
                PhoneNumber = "+55 (21) 12345-6789",
                FirstName = "Caio",
                LastName = "Resende"
            };

            _user.CreateAsync(client, "Alex123@").GetAwaiter().GetResult();

            _user.AddToRoleAsync(client, IdentityConfiguration.Client).GetAwaiter().GetResult();

            var clinetClaims = _user.AddClaimsAsync(client, new Claim[]
            {
                new Claim(JwtClaimTypes.Name, $"{client.FirstName} {admin.LastName}"),
                new Claim(JwtClaimTypes.GivenName, client.FirstName),
                new Claim(JwtClaimTypes.FamilyName, client.LastName),
                new Claim(JwtClaimTypes.Role, IdentityConfiguration.Client)
            }).Result;
        }
    }
}
