using Microsoft.EntityFrameworkCore;
using Shared.Models;
using SistemaLlavesWebAPI.Dal;
using SistemaLlavesWebAPI.Interfaces;
using System.Diagnostics.CodeAnalysis;

namespace SistemaLlavesWebAPI.Services
{
    [ExcludeFromCodeCoverage]
    public class CategoryServices: ICategoryService
    {
        private readonly Context _context;

        public CategoryServices(Context context)
        {
            this._context = context;
        }

        public async Task<IEnumerable<Categorias>> GetAsync()
        {
            return await _context.Categorias.ToListAsync();
        }

        public async Task<Categorias> AddAsync(Categorias categorias)
        {
            await _context.Categorias.AddAsync(categorias);
            await _context.SaveChangesAsync();
            return categorias;
        }

        public async Task<Categorias> PutAsync(Categorias categorias)
        {
            if (categorias.CategoriaId == 0)
            {
                throw new ArgumentException("El ID de la categoría no puede ser cero.");
            }
           
            var existingCategory = await _context.Categorias.FindAsync(categorias.CategoriaId);
            if (existingCategory == null)
            {
                throw new KeyNotFoundException($"La categoría con ID {categorias.CategoriaId} no existe.");
            }

            existingCategory.Nombre = categorias.Nombre;       
      
            _context.Categorias.Update(existingCategory);
            await _context.SaveChangesAsync();

            return existingCategory;
        }

        public async Task<bool> DeleteAsync(int categoriaId)
        {
            var categoria = await _context.Categorias.FirstOrDefaultAsync(c => c.CategoriaId == categoriaId);
            if (categoria == null)
            {
                return false;
            }

            _context.Categorias.Remove(categoria);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}