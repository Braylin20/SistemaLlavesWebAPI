using Shared.Models;

namespace SistemaLlavesWebAPI.Interfaces;

public interface IWarrantyService
{
    public Task<List<Garantias>> GetAsync();
    public Task<bool> AddAsync(Garantias garantia);
    public Task<Garantias> PutAsync(Garantias garantia);
    public Task<Garantias?> DeleteAsync(int id);
    public Task<Garantias?> GetById(int id);
}
