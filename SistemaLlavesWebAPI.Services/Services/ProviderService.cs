using Microsoft.EntityFrameworkCore;
using Shared.Models;
using SistemaLlavesWebAPI.Dal;
using SistemaLlavesWebAPI.Abstractions;

namespace SistemaLlavesWebAPI.Services;

public class ProviderService(Context context) : IProviderService
{
    public async Task<List<Proveedores>> GetAsync()
    {
        return await context.Proveedores.ToListAsync();
    }
    
    public async Task<bool> AddAsync(Proveedores proveedores)
    {
        await context.Proveedores.AddAsync(proveedores);
        return await context.SaveChangesAsync() > 0;
    }
    
    public async Task<Proveedores> PutAsync(Proveedores proveedores)
    {
        var proveedorExists = await context.Proveedores.FindAsync(proveedores.ProovedorId) ?? 
                              throw new KeyNotFoundException("Proveedor no encontrado");
        
        context.Entry(proveedorExists).State = EntityState.Detached;
        
        var result = context.Proveedores.Update(proveedores);
        await context.SaveChangesAsync();
        return result.Entity;
    }
    
    public async Task<Proveedores> DeleteAsync(int proveedorId)
    {
        var proveedor = await context.Proveedores.FindAsync(proveedorId) ?? 
                        throw new KeyNotFoundException("Proveedor no encontrado");
        
        context.Proveedores.Remove(proveedor);
        await context.SaveChangesAsync();
        return proveedor;
    }
    
    public async Task<Proveedores> GetByIdAsync(int proveedorId)
    {
        var proveedor = await context.Proveedores.FindAsync(proveedorId);
        return proveedor ?? throw new KeyNotFoundException("Proveedor no encontrado");
    }
}