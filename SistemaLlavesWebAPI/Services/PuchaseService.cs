using Microsoft.EntityFrameworkCore;
using Shared.Models;
using SistemaLlavesWebAPI.Dal;
using SistemaLlavesWebAPI.Interfaces;

namespace SistemaLlavesWebAPI.Services
{
    public class PuchaseService(Context context) : IPuchaseService
    {
        private readonly Context _context = context;

        public async Task<List<Compras>> GetAllAsync()
        {
            return await _context.Compras.ToListAsync();
        }

        public async Task<Compras?>GetById(int id)
        {
            return await _context.Compras.FindAsync(id);
        }

        public async Task<bool> AddAsync(Compras compra)
        {
            await _context.Compras.AddAsync(compra);

            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<Compras?> DeleteAsync(int id)
        {
            var puchase = await _context.Compras.FindAsync(id);

            if (puchase is null) return null;

            _context.Compras.Remove(puchase);
            await _context.SaveChangesAsync();

            return puchase;
        }

        public async Task<Compras> PutAsync(Compras compra)
        {
            var existingCompra = await _context.Compras.FindAsync(compra.CompraId) ??
                throw new KeyNotFoundException($"Compra with ID {compra.CompraId} was not found");

            _context.Entry(existingCompra).State = EntityState.Detached;

            var result = _context.Compras.Update(compra);
            await _context.SaveChangesAsync();
            return result.Entity;
        }
    }
}
