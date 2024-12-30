using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using SistemaLlavesWebAPI.Dal;
using System.Diagnostics.CodeAnalysis;

namespace SistemaLlavesWebAPI.Data.DI
{
    [ExcludeFromCodeCoverage]
    public static class DbContextRegistrar
    {
        public static IServiceCollection RegisterDbContextFactory(this IServiceCollection services)
        {
            services.AddDbContextFactory<Context>(o => o.UseSqlServer("Name=ConStr"));
            return services;
        }
    }
}
