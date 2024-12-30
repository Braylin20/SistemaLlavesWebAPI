using Microsoft.EntityFrameworkCore;
using Shared.Models;
using SistemaLlavesWebAPI.Dal;
using SistemaLlavesWebAPI.Abstractions;
using System.Diagnostics.CodeAnalysis;


namespace SistemaLlavesWebAPI.Services;

[ExcludeFromCodeCoverage]
public class ProductServices(Context _context) : IProductService
{

    public async Task<List<Productos>> GetAsync()
    {
        return await _context.Productos.ToListAsync();
    }
    public async Task<bool> AddAsync(Productos producto)
    {
        _context.Productos.Add(producto);
        return await _context.SaveChangesAsync() > 0;
    }
    public async Task<bool> PutAsync(Productos producto)
    {
        var existingEntity = await _context.Productos.FindAsync(producto.ProductoId);
        if (existingEntity is null) return false;

        _context.Entry(existingEntity).State = EntityState.Detached; 

        _context.Update(producto);
        return await _context.SaveChangesAsync() > 0;
    }
    public async Task<Productos?> DeleteAsync(int id)
    {
        var producto = await _context.Productos.FindAsync(id);
        if (producto != null)
        {
            _context.Remove(producto);
            await _context.SaveChangesAsync();
        }

        return producto;
    }

    public async Task<Productos?> GetById(int id)
    {
        return await _context.Productos.FindAsync(id);
    }
}
