using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json.Serialization;
using InvestQ.Application.Interfaces;
using InvestQ.Application.Interfaces.Acoes;
using InvestQ.Application.Interfaces.Ativos;
using InvestQ.Application.Interfaces.Clientes;
using InvestQ.Application.Interfaces.FundosImobiliarios;
using InvestQ.Application.Interfaces.Identity;
using InvestQ.Application.Interfaces.TesourosDiretos;
using InvestQ.Application.Services;
using InvestQ.Application.Services.Acoes;
using InvestQ.Application.Services.Ativos;
using InvestQ.Application.Services.Clientes;
using InvestQ.Application.Services.FundosImobiliarios;
using InvestQ.Application.Services.Identity;
using InvestQ.Application.Services.TesourosDiretos;
using InvestQ.Data.Context;
using InvestQ.Data.Interfaces;
using InvestQ.Data.Interfaces.Acoes;
using InvestQ.Data.Interfaces.Ativos;
using InvestQ.Data.Interfaces.Clientes;
using InvestQ.Data.Interfaces.FundosImobiliarios;
using InvestQ.Data.Interfaces.Identity;
using InvestQ.Data.Interfaces.TesourosDiretos;
using InvestQ.Data.Repositories;
using InvestQ.Data.Repositories.Acoes;
using InvestQ.Data.Repositories.Ativos;
using InvestQ.Data.Repositories.Clientes;
using InvestQ.Data.Repositories.FundosImobiliarios;
using InvestQ.Data.Repositories.Identity;
using InvestQ.Data.Repositories.TesourosDiretos;
using InvestQ.Domain.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace InvestQ.API
{
    public class _Startup
    {
        public _Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IConfiguration _configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // services.AddDbContext<InvestQContext>(
            //     context => context.UseSqlite(_configuration.GetConnectionString("Default"))
            // );
            services.AddDbContext<InvestQContext>(
                context => context.UseSqlServer(_configuration.GetConnectionString("Default"))
            );

            services.AddIdentityCore<User>(opt => 
            {
                opt.Password.RequireDigit = true;
                opt.Password.RequireNonAlphanumeric = true;
                opt.Password.RequireLowercase = true;
                opt.Password.RequireUppercase = true;
                opt.Password.RequiredLength = 8;
            }).AddRoles<Role>()
                .AddRoleManager<RoleManager<Role>>()
                .AddSignInManager<SignInManager<User>>()
                .AddRoleValidator<RoleValidator<Role>>()
                .AddEntityFrameworkStores<InvestQContext>()
                .AddDefaultTokenProviders();

            // Feito para o projeto Angular acessar
            // services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            //         .AddJwtBearer(opt => 
            //         {
            //             opt.TokenValidationParameters = new TokenValidationParameters
            //             {
            //                 ValidateIssuerSigningKey = true,
            //                 IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["TokenKey"])),
            //                 ValidateIssuer = false,
            //                 ValidateAudience = false
            //             };
            //         });

            //Feito para o projeto Angular acessar
            services.AddControllers()
                .AddJsonOptions(opt => opt.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter())
                )
                .AddNewtonsoftJson(
                    opt => opt.SerializerSettings.ReferenceLoopHandling =
                        Newtonsoft.Json.ReferenceLoopHandling.Ignore
                );

            //services.AddControllers();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(opt => 
                {
                    opt.Authority = "https://localhost:4435/";
                    opt.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateAudience = false
                    };
                });

            services.AddAuthorization(opt => 
            {
                opt.AddPolicy("ApiScope", policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.RequireClaim("scope", "investq");
                });
            });

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddCors();

            services.AddScoped<IClienteRepo, ClienteRepo>();
            services.AddScoped<ILancamentoRepo, LancamentoRepo>();
            services.AddScoped<IPortifolioRepo, PortifolioRepo>();
            services.AddScoped<ICorretoraRepo, CorretoraRepo>();
            services.AddScoped<ICarteiraRepo, CarteiraRepo>();
            services.AddScoped<ISetorRepo, SetorRepo>();
            services.AddScoped<ISubsetorRepo, SubsetorRepo>();
            services.AddScoped<ISegmentoRepo, SegmentoRepo>();
            services.AddScoped<ISegmentoAnbimaRepo, SegmentoAnbimaRepo>();
            services.AddScoped<IFundoImobiliarioRepo, FundoImobiliarioRepo>();
            services.AddScoped<IAdministradorDeFundoImobiliarioRepo, AdministradorDeFundoImobiliarioRepo>();
            services.AddScoped<ITipoDeInvestimentoRepo, TipoDeInvestimentoRepo>();
            services.AddScoped<ITesouroDiretoRepo, TesouroDiretoRepo>();
            services.AddScoped<IProventoRepo, ProventoRepo>();
            services.AddScoped<IAcaoRepo, AcaoRepo>();
            services.AddScoped<IAtivoRepo, AtivoRepo>();
            services.AddScoped<ITaxaDeNegociacaoRepo, TaxaDeNegociacaoRepo>();
            services.AddScoped<ITaxaDeIRRepo, TaxaDeIRRepo>();
            services.AddScoped<IUserRepo, UserRepo>();

            services.AddScoped<IGeralRepo, GeralRepo>();

            services.AddScoped<IClienteService, ClienteService>();
            services.AddScoped<ILancamentoService, LancamentoService>();
            services.AddScoped<IPortifolioService, PortifolioService>();
            services.AddScoped<ICorretoraService, CorretoraService>();
            services.AddScoped<ICarteiraService, CarteiraService>();
            services.AddScoped<ISetorService, SetorService>();
            services.AddScoped<ISubsetorService, SubsetorService>();
            services.AddScoped<ISegmentoService, SegmentoService>();
            services.AddScoped<ISegmentoAnbimaService, SegmentoAnbimaService>();
            services.AddScoped<IFundoImobiliarioService, FundoImobiliarioService>();
            services.AddScoped<IAdministradorDeFundoImobiliarioService, AdministradorDeFundoImobiliarioService>();
            services.AddScoped<ITipoDeInvestimentoService, TipoDeInvestimentoService>();
            services.AddScoped<ITesouroDiretoService, TesouroDiretoService>();
            services.AddScoped<IProventoService, ProventoService>();            
            services.AddScoped<IAcaoService, AcaoService>();
            services.AddScoped<IAtivoService, AtivoService>();
            services.AddScoped<ITaxaDeNegociacaoService, TaxaDeNegociacaoService>();
            services.AddScoped<ITaxaDeIRService, TaxaDeIRService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ITokenService, TokenService>();

            services.AddSwaggerGen(opt =>
            {
                opt.SwaggerDoc("v1", new OpenApiInfo { Title = "InvestQ.API", Version = "v1" });
                
                opt.EnableAnnotations();

                opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = @"JWT Authorization header usando o Bearer.
                                    Entre com 'Bearer ' [espaço] então coloque seu token.
                                    Exemplo: 'Bearer 1234abcdef",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                opt.AddSecurityRequirement( new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header
                        },
                        new List<string>()
                    }
                });

                // Feito para o projeto Angular acessar
                // opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme 
                // {
                //     Description = @"JWT Authorization header usando o Bearer.
                //                 Entre com 'Bearer ' [espaço] então coloque seu token.
                //                 Exemplo: 'Bearer 1234abcdef",
                //     Name = "Authorization",
                //     In = ParameterLocation.Header,
                //     Type = SecuritySchemeType.ApiKey,
                //     Scheme = "Bearer"
                // });

                // opt.AddSecurityRequirement( new OpenApiSecurityRequirement()
                // {
                //     {
                //         new OpenApiSecurityScheme
                //         {
                //             Reference = new OpenApiReference
                //             {
                //                 Type = ReferenceType.SecurityScheme,
                //                 Id = "Bearer"
                //             },
                //             Scheme = "oauth2",
                //             Name = "Bearer",
                //             In = ParameterLocation.Header
                //         },
                //         new List<string>()
                //     }
                // });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "InvestQ.API v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();            

            app.UseCors(acesso => acesso.AllowAnyHeader()
                                        .AllowAnyMethod()
                                        .AllowAnyOrigin()
            );

            app.UseStaticFiles(new StaticFileOptions() {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "Resources")),
                RequestPath = new PathString("/Resources")
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
