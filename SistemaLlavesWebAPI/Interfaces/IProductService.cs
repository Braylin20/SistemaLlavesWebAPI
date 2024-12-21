using Shared.Models;

namespace SistemaLlavesWebAPI.Interfaces;

public interface IProductService
{
    public Task<List<Productos>> GetAsync();
    public Task <bool> AddAsync(Productos producto);
    public Task<Productos?> PutAsync(Productos producto);
    public Task<Productos> DeleteAsync(int id);
    public Task<Productos?> GetById(int id);
   
}
