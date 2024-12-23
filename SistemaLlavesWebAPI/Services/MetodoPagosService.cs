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
        public async Task<MetodosPagos> PutAsync(MetodosPagos metodosPago)
        {
            var result = _context.MetodosPagos.Update(metodosPago);
            await _context.SaveChangesAsync();
            return result.Entity;
        }
        public async Task<MetodosPagos> DeleteAsync(int id)
        {
            var metodoPago = await _context.MetodosPagos.FindAsync(id) ??
           throw new KeyNotFoundException("The product was not found");

            _context.Remove(metodoPago);
            await _context.SaveChangesAsync();

            return metodoPago;
        }
    }
}
