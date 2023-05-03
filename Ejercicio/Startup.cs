using Ejercicio.Core;
using Ejercicio.Models;
using JWTValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Hosting.Internal;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ejercicio
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment webHostEnvi)
        {
            Configuration = configuration;
            StaticConfig = configuration;
            HostingEnvironment = webHostEnvi;
        }

        public IConfiguration Configuration { get; }
        public static IConfiguration StaticConfig { get; private set; }
        private IWebHostEnvironment HostingEnvironment { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Ejercicio API",
                    Description = "Una ASP.NET Core Web API para ejercicios solicitados",
                    TermsOfService = new Uri("https://example.com/terms"),
                    Contact = new OpenApiContact
                    {
                        Name = "Example Contact",
                        Url = new Uri("https://example.com/contact")
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Example License",
                        Url = new Uri("https://example.com/license")
                    }
                });
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = @"JWT Authorization header usando un esquema Bearer. \r\n\r\n 
                      Ingresar 'Bearer' [space] y luego el token en el text input de abajao.
                      \r\n\r\nEjemplo: 'Bearer 12345abcdef'",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });
                options.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme {
                            Reference = new OpenApiReference { 
                                Type = ReferenceType.SecurityScheme, 
                                Id = "Bearer"
                            }, 
                            Scheme = "oauth2",
                            Name = "Bearer", 
                            In = ParameterLocation.Header,
                        },
                        new List<string>()
                    }
                });

            });
            services.AddDbContext<EjercicioDBContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"),
                ef => ef.MigrationsAssembly(typeof(EjercicioDBContext).Assembly.FullName)));
            services.AddScoped<IEjercicioDBContext>(provider => provider.GetService<EjercicioDBContext>());
            services.AddControllers();
            services.AddJwtAuthentication(HostingEnvironment, new ConfigurationBuilder().AddJsonFile("appsettings.json")
                                                                                                 .AddJsonFile($"appsettings.{HostingEnvironment.EnvironmentName}.json", optional: false));
            IoC.AddDependencyGenToken(services);
            IoC.AddDependencyPersonasRepository(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint("/swagger/v1/swagger.json", "Ejercicios");
                    options.RoutePrefix = string.Empty;
                });
            };
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseAuthentication();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
