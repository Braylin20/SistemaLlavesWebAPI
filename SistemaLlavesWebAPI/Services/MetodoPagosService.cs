using Microsoft.EntityFrameworkCore;
using Shared.Models;
using SistemaLlavesWebAPI.Dal;
using SistemaLlavesWebAPI.Interfaces;
using System.Diagnostics.CodeAnalysis;

namespace SistemaLlavesWebAPI.Services
{
    [ExcludeFromCodeCoverage]
    public class MetodoPagosService(Context _context) : IMetodoPagosService
    {
        public async Task<List<MetodosPagos>> GetAsync()
        {
            return await _context.MetodosPagos.ToListAsync();
        }
        public async Task<bool> AddAsync(MetodosPagos metodosPago)
        {
            _context.MetodosPagos.Add(metodosPago);
            return await _context.SaveChangesAsync() > 0;
        }
        public async Task<bool> PutAsync(MetodosPagos metodosPago)
        {
            var existingEntity = await _context.MetodosPagos.FindAsync(metodosPago.MetodoPagoId);
            if (existingEntity is null) return false;

            _context.Entry(existingEntity).State = EntityState.Detached;

            _context.Update(metodosPago);
            return await _context.SaveChangesAsync() > 0;
        }
        public async Task<bool> DeleteAsync(int id)
        {
            var metodoPago = await _context.MetodosPagos.FindAsync(id);
            if (metodoPago is null)
                return false;

            _context.Remove(metodoPago);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<MetodosPagos?> GetById(int id)
        {
            return await _context.MetodosPagos.FindAsync(id);
        }
    }
}
