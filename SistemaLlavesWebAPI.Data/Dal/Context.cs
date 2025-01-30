using Microsoft.EntityFrameworkCore;
using Shared.Models;

namespace SistemaLlavesWebAPI.Dal;

public class Context : DbContext
{
    public Context(DbContextOptions<Context> options) : base(options) { }
    
    public DbSet<Categorias> Categorias { get; set; }
    public DbSet<Clientes> Clientes { get; set; }
    public DbSet<Compras> Compras { get; set; }
    public DbSet<ComprasDetalle> ComprasDetalle { get; set; }
    public DbSet<Garantias> Garantias { get; set; }
    public DbSet<MetodosPagos> MetodosPagos { get; set; }
    public DbSet<Productos> Productos { get; set; }
    public DbSet<Proveedores> Proveedores { get; set; }
    public DbSet<Ventas> Ventas { get; set; }
    public DbSet<VentasDetalle> VentasDetalle { get; set; }
    public DbSet<Cuadres> Cuadres{ get; set; }
}