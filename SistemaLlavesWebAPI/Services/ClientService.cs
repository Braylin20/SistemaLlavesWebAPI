using Microsoft.EntityFrameworkCore;
using Shared.Models;
using SistemaLlavesWebAPI.Dal;
using SistemaLlavesWebAPI.Interfaces;

namespace SistemaLlavesWebAPI.Services;

public class ClientService(Context context) : IClientService
{
    private readonly Context _context = context;

    public async Task<List<Clientes>> GetAsync()
    {
        return await _context.Clientes.ToListAsync();
    }

    public async Task<bool> AddAsync(Clientes clientes)
    {
        await _context.Clientes.AddAsync(clientes);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<Clientes> PutAsync(Clientes clientes)
    {
        var clienteExists = await _context.Clientes.FindAsync(clientes.ClienteId) ?? 
                            throw new KeyNotFoundException("Cliente no encontrado");
        
        _context.Entry(clienteExists).State = EntityState.Detached;
        
        var result = _context.Clientes.Update(clientes);
        await _context.SaveChangesAsync();
        return result.Entity;
    }

    public async Task<Clientes> DeleteAsync(int clienteId)
    {
        var cliente = await _context.Clientes.FindAsync(clienteId) ?? 
                      throw new KeyNotFoundException("Cliente no encontrado");
        
        _context.Clientes.Remove(cliente);
        await _context.SaveChangesAsync();
        return cliente;
    }
}