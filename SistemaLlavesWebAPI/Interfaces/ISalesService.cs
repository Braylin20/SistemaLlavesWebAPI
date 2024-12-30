using Shared.Models;

namespace SistemaLlavesWebAPI.Interfaces
{
    public interface ISalesService
    {
        Task<IEnumerable<Ventas>> GetAsync();
        Task<Ventas> AddAsync(Ventas ventas);
        Task<Ventas> PutAsync(Ventas ventas);
        Task<bool> DeleteAsync(int ventaId);
        Task<Ventas?> GetVentaById(int id);
    }
}