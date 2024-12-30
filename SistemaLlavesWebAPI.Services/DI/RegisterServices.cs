using Microsoft.Extensions.DependencyInjection;
using Shared.Models;
using SistemaLlavesWebAPI.Abstractions;
using SistemaLlavesWebAPI.Data.DI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaLlavesWebAPI.Services.DI
{
    public static class RegisterServices
    {
        public static IServiceCollection Register_Services(this IServiceCollection services)
        {
            services.RegisterDbContextFactory();
            services.AddScoped<ICategoryService, CategoryServices>();
            services.AddScoped<IPuchaseService, PuchaseService>();
            services.AddScoped<IProductService, ProductServices>();
            services.AddScoped<IMetodoPagosService, MetodoPagosService>();
            services.AddScoped<IProviderService, ProviderService>();
            services.AddScoped<IClientService, ClientService>();
            services.AddScoped<ISalesService, SalesServices>();
            services.AddScoped<IWarrantyService, WarrantyService>();
         
            return services;

        }
    }
}
