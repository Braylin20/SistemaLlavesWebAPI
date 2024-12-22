using Shared.Models;

namespace SistemaLlavesWebAPI.Interfaces;

public interface IPuchaseService
{
    public Task<List<Compras>> GetAllAsync();
    public Task<bool> AddAsync(Compras compra);
    public Task<Compras> PutAsync(Compras compra);
    public Task<Compras?> DeleteAsync(int id);
    public Task<Compras?> GetById(int id);
}