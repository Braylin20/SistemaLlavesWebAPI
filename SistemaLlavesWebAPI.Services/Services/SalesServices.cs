using Microsoft.EntityFrameworkCore;
using Shared.Models;
using SistemaLlavesWebAPI.Dal;
using SistemaLlavesWebAPI.Abstractions;
using System.Diagnostics.CodeAnalysis;

namespace SistemaLlavesWebAPI.Services
{
    [ExcludeFromCodeCoverage]
    public class SalesServices(Context context) : ISalesService
    {
        private readonly Context _context = context;

        public async Task<IEnumerable<Ventas>> GetAsync()
        {
            return await _context.Ventas.ToListAsync();
        }
        public async Task<Ventas?> GetVentaById(int id)
        {
            var venta = await _context.Ventas.FindAsync(id);
            if (venta == null)
            {
                throw new KeyNotFoundException("Categoría no encontrada.");
            }
            return venta;
        }
        public async Task<Ventas> AddAsync(Ventas ventas)
        {
            await _context.Ventas.AddAsync(ventas);
            await _context.SaveChangesAsync();
            return ventas;
        }

        public async Task<bool> DeleteAsync(int ventaId)
        {
            var venta = await _context.Ventas.FindAsync(ventaId);
            if (venta == null)
            {
                return false;
            }

            _context.Ventas.Remove(venta);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<Ventas> PutAsync(Ventas venta)
        {
            var result = _context.Ventas.Update(venta);
            await _context.SaveChangesAsync();
            return result.Entity;
        }
    }
}