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
    public async Task<bool> PutAsync(Productos producto)
    {
        _context.Update(producto);
        _context.Entry(producto).State = EntityState.Detached;
        return await _context.SaveChangesAsync() > 0 ;

    }
    public async Task<Productos> DeleteAsync(int id)
    {
        var producto = await _context.Productos.FindAsync(id) ??
            throw new KeyNotFoundException("The product was not found");

        _context.Remove(producto);
        await _context.SaveChangesAsync();

        return producto;
    }


}
