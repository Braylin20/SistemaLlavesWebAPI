using Microsoft.EntityFrameworkCore;
using Shared.Models;
using SistemaLlavesWebAPI.Dal;
using SistemaLlavesWebAPI.Abstractions;
using System.Diagnostics.CodeAnalysis;

namespace SistemaLlavesWebAPI.Services
{
    [ExcludeFromCodeCoverage]
    public class SalesServices : ISalesService
    {
        private readonly Context _context;

        public SalesServices(Context context)
        {
            this._context = context;
        }

        public async Task<IEnumerable<Ventas>> GetAsync()
        {
            return await _context.Ventas.ToListAsync();
        }

        public async Task<Ventas> AddAsync(Ventas ventas)
        {
            await _context.Ventas.AddAsync(ventas);
            await _context.SaveChangesAsync();
            return ventas;
        }

        public async Task<bool> DeleteAsync(int ventaId)
        {
            var venta = await _context.Categorias.FindAsync(ventaId);
            if (venta == null)
            {
                return false;
            }

            _context.Categorias.Remove(venta);
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