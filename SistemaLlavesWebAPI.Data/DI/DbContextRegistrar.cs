using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using SistemaLlavesWebAPI.Dal;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Configuration;

namespace SistemaLlavesWebAPI.Data.DI
{
    [ExcludeFromCodeCoverage]
    public static class DbContextRegistrar
    {
        public static IServiceCollection RegisterDbContextFactory(this IServiceCollection services, IConfiguration configuration)
        {
            // Usa la configuración para obtener la cadena de conexión
            var connectionString = configuration.GetConnectionString("ConStr")
            // Configura el DbContextFactory con la cadena de conexión
            services.AddDbContextFactory<Context>(options =>
                options.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 20))));

            return services;
        }
    }
}
