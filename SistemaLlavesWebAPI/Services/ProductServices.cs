using Microsoft.AspNetCore.Http.HttpResults;
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
        public Task<Productos> PutAsync(int id, Productos producto)
        {
            _context.Productos.Update(producto);
        }
        public async Task<Productos?> DeleteAsync(int id)
        {
            var producto = _context.Productos.Find(id);

            if (producto is not null)
            {
                _context.Remove(producto);
                await _context.SaveChangesAsync();
            }

            return producto;
        }


    }
}
