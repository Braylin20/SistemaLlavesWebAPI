
using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using SistemaLlavesWebAPI.Abstractions;
using SistemaLlavesWebAPI.Data.DI;
using SistemaLlavesWebAPI.Services;
using SistemaLlavesWebAPI.Services.DI;

namespace SistemaLlavesWebAPI
{
    [ExcludeFromCodeCoverage]
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var ConStr = 
                builder.Configuration.GetConnectionString("ConStr");

            //Inyeccion del contexto
            builder.Services.Register_Services();

            builder.Services.AddScoped<IClientService, ClientService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
