using Duende.IdentityServer.Services;
using InvestQ.IdentityServer.Configuration;
using InvestQ.IdentityServer.Initializer;
using InvestQ.IdentityServer.Model;
using InvestQ.IdentityServer.Model.Context;
using InvestQ.IdentityServer.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace InvestQ.IdentityServer;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    public IConfiguration _configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddDbContext<InvestQIdentityServerContext>(
                context => context.UseSqlServer(_configuration.GetConnectionString("Default"))
            );

        services.AddIdentity<ApplicationUser, IdentityRole>()
            .AddEntityFrameworkStores<InvestQIdentityServerContext>()
            .AddDefaultTokenProviders();

        var builder = services.AddIdentityServer(options =>
            {
                options.Events.RaiseErrorEvents = true;
                options.Events.RaiseInformationEvents = true;
                options.Events.RaiseFailureEvents = true;
                options.Events.RaiseSuccessEvents = true;
                options.EmitStaticAudienceClaim = true;
            }).AddInMemoryIdentityResources(IdentityConfiguration.IdentityResources)
              .AddInMemoryApiScopes(IdentityConfiguration.ApiScopes)
              .AddInMemoryClients(IdentityConfiguration.Clients)
              .AddAspNetIdentity<ApplicationUser>();

        services.AddScoped<IDbInitializer, DbInitializer>();
        services.AddScoped<IProfileService, ProfileService>();

        builder.AddDeveloperSigningCredential();

        services.AddControllersWithViews();
    }
    public void Configure(IApplicationBuilder app,
            IWebHostEnvironment env,
            IDbInitializer initializer
        )
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseExceptionHandler("/Home/Error");
        }
        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();
        app.UseIdentityServer();
        app.UseAuthorization();

        initializer.Initialize();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
        });
    }
}

