using Microsoft.EntityFrameworkCore;
using Shared.Models;
using SistemaLlavesWebAPI.Dal;
using SistemaLlavesWebAPI.Interfaces;

namespace SistemaLlavesWebAPI.Services;

public class ClientService(Context context) : IClientService
{
    public async Task<List<Clientes>> GetAsync()
    {
        return await context.Clientes.ToListAsync();
    }

    public async Task<bool> AddAsync(Clientes clientes)
    {
        await context.Clientes.AddAsync(clientes);
        return await context.SaveChangesAsync() > 0;
    }

    public async Task<Clientes> PutAsync(Clientes clientes)
    {
        var clienteExists = await context.Clientes.FindAsync(clientes.ClienteId) ?? 
                            throw new KeyNotFoundException("Cliente no encontrado");
        
        context.Entry(clienteExists).State = EntityState.Detached;
        
        var result = context.Clientes.Update(clientes);
        await context.SaveChangesAsync();
        return result.Entity;
    }

    public async Task<Clientes> DeleteAsync(int clienteId)
    {
        var cliente = await context.Clientes.FindAsync(clienteId) ?? 
                      throw new KeyNotFoundException("Cliente no encontrado");
        
        context.Clientes.Remove(cliente);
        await context.SaveChangesAsync();
        return cliente;
    }
}