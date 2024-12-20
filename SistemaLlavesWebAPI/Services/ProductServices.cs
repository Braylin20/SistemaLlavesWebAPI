using Microsoft.EntityFrameworkCore;
using Shared.Models;
using SistemaLlavesWebAPI.Dal;
using SistemaLlavesWebAPI.Interfaces;

namespace SistemaLlavesWebAPI.Services
{
    public class ProductServices : IProductService
    {
        private readonly Context _context;

        public ProductServices(Context context)
        {
            this._context = context;
        }
        public async Task<List<Productos>> GetAsync()
        {
            return await _context.Productos.ToListAsync();
        }
        public async Task<bool> AddAsync(Productos producto)
        {
            _context.Productos.Add(producto);
            return await _context.SaveChangesAsync() > 0;
        }
        public Task<Productos> PutAsync()
        {
            throw new NotImplementedException();
        }
        public Task<Productos> DeleteAsync()
        {
            throw new NotImplementedException();
        }


    }
}
