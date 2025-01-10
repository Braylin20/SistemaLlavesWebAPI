using Shared.Models;

namespace SistemaLlavesWebAPI.Abstractions;

public interface IClientService
{
    public Task<List<Clientes>> GetAsync();
    public Task<bool> AddAsync(Clientes clientes);
    public Task<Clientes> PutAsync(Clientes clientes);
    public Task<Clientes> DeleteAsync(int clienteId);
    public Task<Clientes> GetByIdAsync(int clienteId);
}