using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shared.Models;
using SistemaLlavesWebAPI.Abstractions;
using SistemaLlavesWebAPI.Abstractions.Interfaces;
using SistemaLlavesWebAPI.Data.DI;
using SistemaLlavesWebAPI.Services.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaLlavesWebAPI.Services.DI
{
    [ExcludeFromCodeCoverage]
    public static class RegisterServices
    {
        public static IServiceCollection Register_Services(this IServiceCollection services, IConfiguration configuration)
        {
            services.RegisterDbContextFactory(configuration);
            services.AddScoped<ICategoryService, CategoryServices>();
            services.AddScoped<IPuchaseService, PuchaseService>();
            services.AddScoped<IProductService, ProductServices>();
            services.AddScoped<IMetodoPagosService, MetodoPagosService>();
            services.AddScoped<IProviderService, ProviderService>();
            services.AddScoped<IClientService, ClientService>();
            services.AddScoped<ISalesService, SalesServices>();
            services.AddScoped<IWarrantyService, WarrantyService>();
            services.AddScoped<ICuadresService, CuadresServices>();
         
            return services;

        }
    }
}
