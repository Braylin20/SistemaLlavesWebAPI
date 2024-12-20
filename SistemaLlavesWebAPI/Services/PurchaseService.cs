using Microsoft.EntityFrameworkCore;
using Shared.Models;
using SistemaLlavesWebAPI.Dal;
using SistemaLlavesWebAPI.Interfaces;

namespace SistemaLlavesWebAPI.Services
{
    public class PurchaseService(Context context) : IPuchaseService
    {
        private readonly Context _context = context;

        public async Task<List<Compras>> GetAsync()
        {
            return await _context.Compras.ToListAsync();
        }

        public async Task<bool> AddAsync(Compras compra)
        {
            await _context.Compras.AddAsync(compra);

            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<Compras> DeleteAsync(int id)
        {
            var puchase = await _context.Compras.FindAsync(id) ?? throw new KeyNotFoundException($"Puchase with ID {id} was no found");

            _context.Compras.Remove(puchase);
            await _context.SaveChangesAsync();

            return puchase;
        }

        public async Task<Compras> PutAsync(Compras compra)
        {
            var result = _context.Compras.Update(compra);
            await _context.SaveChangesAsync();
            return result.Entity;
        }
    }
}
