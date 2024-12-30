using Shared.Models;

namespace SistemaLlavesWebAPI.Abstractions
{
    public interface IMetodoPagosService
    {
        public Task<List<MetodosPagos>> GetAsync();
        public Task<bool> AddAsync(MetodosPagos metodosPago);
        public Task<bool> PutAsync(MetodosPagos metodosPago);
        public Task<MetodosPagos?> DeleteAsync(int id);
        public Task<MetodosPagos?> GetById(int id);
    }
}
