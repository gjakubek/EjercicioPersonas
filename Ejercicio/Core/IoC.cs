using Ejercicio.Repositories;
using Ejercicio.Services;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Ejercicio.Core
{
    public static class IoC
    {
        public static IServiceCollection AddDependencyGenToken(this IServiceCollection services)
        {
            services.AddTransient<IGeneradorToken, GeneradorToken>();
            return services;
        }
        public static IServiceCollection AddDependencyPersonasRepository(this IServiceCollection services)
        {
            services.AddTransient<IPersonasRepository, PersonasRepository>();
            return services;
        }
    }
}
