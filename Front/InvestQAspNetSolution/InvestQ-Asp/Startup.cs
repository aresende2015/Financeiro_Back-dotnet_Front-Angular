using Microsoft.AspNetCore.Authentication;
using InvestQ_Asp.Services.Ativos.IServices;
using InvestQ_Asp.Services.Ativos;
using InvestQ_Asp.Services.Acoes;
using InvestQ_Asp.Services.Acoes.IServices;

namespace InvestQ_Asp;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    public IConfiguration _configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddHttpClient<ISetorService, SetorService>(
            c => c.BaseAddress = new Uri(_configuration["ServiceUrls:InvestQAPI"])
        );

        services.AddHttpClient<ITaxaDeIrService, TaxaDeIrService>(
            c => c.BaseAddress = new Uri(_configuration["ServiceUrls:InvestQAPI"])
        );

        services.AddControllersWithViews();

        services.AddAuthentication(opt =>
        {
            opt.DefaultScheme = "Cookies";
            opt.DefaultChallengeScheme = "oidc";
        })
            .AddCookie("Cookies", c => c.ExpireTimeSpan = TimeSpan.FromMinutes(10))
            .AddOpenIdConnect("oidc", options =>
            {
                options.Authority = _configuration["ServiceUrls:IdentityServer"];
                options.GetClaimsFromUserInfoEndpoint = true;
                options.ClientId = "investq";
                options.ClientSecret = "my_super_secret";
                options.ResponseType = "code";

                options.ClaimActions.MapJsonKey("role", "role", "role");
                options.ClaimActions.MapJsonKey("sub", "sub", "sub");

                options.TokenValidationParameters.NameClaimType = "name";
                options.TokenValidationParameters.RoleClaimType = "role";

                options.Scope.Add("investq");

                options.SaveTokens = true;
            });

        services.AddMemoryCache();

        services.AddSession();

        services.AddRazorPages();

        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

    }
    public void Configure(IApplicationBuilder app,
            IWebHostEnvironment env)
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
        app.UseAuthentication();
        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}"
            );
        });
    }
}