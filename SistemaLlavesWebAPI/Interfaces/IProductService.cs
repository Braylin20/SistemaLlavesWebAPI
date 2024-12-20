using Shared.Models;

namespace SistemaLlavesWebAPI.Interfaces;

public interface IProductService
{
    public Task<List<Productos>> GetAsync();
    public Task <Productos> AddAsync();
    public Task<Productos> PutAsync();
    public Task<Productos> DeleteAsync();
   
}
