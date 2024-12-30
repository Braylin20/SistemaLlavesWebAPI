using Microsoft.EntityFrameworkCore;
using Shared.Models;
using SistemaLlavesWebAPI.Dal;
using SistemaLlavesWebAPI.Abstractions;
using System.Diagnostics.CodeAnalysis;

namespace SistemaLlavesWebAPI.Services
{
    [ExcludeFromCodeCoverage]
    public class CategoryServices(Context context): ICategoryService
    {
        private readonly Context _context = context;

        public async Task<List<Categorias>> GetAsync()
        {
            return await _context.Categorias.ToListAsync();
        }

        public async Task<Categorias> GetCategoryById(int id)
        {
            var categoria = await _context.Categorias.FindAsync(id);
            if (categoria == null)
            {
                throw new KeyNotFoundException("Categoría no encontrada.");
            }
            return categoria;
        }

        public async Task<Categorias?> AddAsync(Categorias categorias)
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
            var categoria = await _context.Categorias.FindAsync(categoriaId);
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