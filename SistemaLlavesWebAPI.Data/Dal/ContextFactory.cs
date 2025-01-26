using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace SistemaLlavesWebAPI.Dal
{
    public class ContextFactory : IDesignTimeDbContextFactory<Context>
    {
        public Context CreateDbContext(string[] args)
        {
            // Construye la configuración
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            // Configura las opciones del contexto
            var optionsBuilder = new DbContextOptionsBuilder<Context>();
            optionsBuilder.UseMySql(configuration.GetConnectionString("ConStr"), new MySqlServerVersion(new Version(8,0,20)));

            return new Context(optionsBuilder.Options);
        }
    }
}
