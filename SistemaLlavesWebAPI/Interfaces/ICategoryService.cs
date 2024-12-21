using Shared.Models;

namespace SistemaLlavesWebAPI.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<Categorias>> GetAsync();
        Task<Categorias> AddAsync(Categorias categorias);
        Task<Categorias> PutAsync(Categorias categorias);
        Task<bool> DeleteAsync(int categoriaId);
    }
}
