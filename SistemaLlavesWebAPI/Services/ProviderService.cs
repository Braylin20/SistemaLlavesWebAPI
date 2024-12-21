using Microsoft.EntityFrameworkCore;
using Shared.Models;
using SistemaLlavesWebAPI.Dal;
using SistemaLlavesWebAPI.Interfaces;

namespace SistemaLlavesWebAPI.Services;

public class ProviderService(Context context) : IProviderService
{
    private readonly Context _context = context;
    
    public async Task<List<Proveedores>> GetAsync()
    {
        return await _context.Proveedores.ToListAsync();
    }
    
    public async Task<bool> AddAsync(Proveedores proveedores)
    {
        await _context.Proveedores.AddAsync(proveedores);
        return await _context.SaveChangesAsync() > 0;
    }
    
    public async Task<Proveedores> PutAsync(Proveedores proveedores)
    {
        var proveedorExists = await _context.Proveedores.FindAsync(proveedores.ProovedorId) ?? 
                              throw new KeyNotFoundException("Proveedor no encontrado");
        
        _context.Entry(proveedorExists).State = EntityState.Detached;
        
        var result = _context.Proveedores.Update(proveedores);
        await _context.SaveChangesAsync();
        return result.Entity;
    }
    
    public async Task<Proveedores> DeleteAsync(int proveedorId)
    {
        var proveedor = await _context.Proveedores.FindAsync(proveedorId) ?? 
                        throw new KeyNotFoundException("Proveedor no encontrado");
        
        _context.Proveedores.Remove(proveedor);
        await _context.SaveChangesAsync();
        return proveedor;
    }
}