using Shared.Models;

namespace SistemaLlavesWebAPI.Interfaces
{
    public interface IMetodoPagosService
    {
        public Task<List<MetodosPagos>> GetAsync();
        public Task<bool> AddAsync(MetodosPagos metodosPago);
        public Task<MetodosPagos> PutAsync(MetodosPagos metodosPago);
        public Task<bool> DeleteAsync(int id);
        public Task<MetodosPagos?> GetById(int id);
    }
}
