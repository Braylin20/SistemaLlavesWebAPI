using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Shared.Models;
using SistemaLlavesWebAPI.Dal;
using SistemaLlavesWebAPI.Interfaces;
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
    public async Task<Productos?> PutAsync(Productos producto)
    {
        var existingProducto = await _context.Productos.FindAsync(producto.ProductoId);

        if (existingProducto is null) return existingProducto;

        _context.Entry(existingProducto).State = EntityState.Detached;

        var result = _context.Productos.Update(producto);
        await _context.SaveChangesAsync();
        return result.Entity;

    }
    public async Task<Productos?> DeleteAsync(int id)
    {
        var producto = await _context.Productos.FindAsync(id);
        if(producto != null)
        {
            _context.Remove(producto);
            await _context.SaveChangesAsync();
        }
        
        return producto;
    }

    public async Task<Productos?> GetById(int id)
    {
        return  await _context.Productos.FindAsync(id);
    }
}
