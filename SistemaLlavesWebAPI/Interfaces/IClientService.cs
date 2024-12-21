using Shared.Models;

namespace SistemaLlavesWebAPI.Interfaces;

public interface IClientService
{
    public Task<List<Clientes>> GetAsync();
    public Task<bool> AddAsync(Clientes clientes);
    public Task<Clientes> PutAsync(Clientes clientes);
    public Task<Clientes> DeleteAsync(int clienteId);
}