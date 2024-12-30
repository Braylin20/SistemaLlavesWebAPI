using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shared.Models;
using SistemaLlavesWebAPI.Dal;
using SistemaLlavesWebAPI.Interfaces;
using SistemaLlavesWebAPI.Services;

namespace SistemaLlavesWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriasController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoriasController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        // GET: api/Categorias
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Categorias>>> GetCategorias()
        {
            return await _categoryService.GetAsync();
            
        }

        // GET: api/Categorias/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Categorias>> GetCategoryById(int id)
        {
           
            var categoria = await _categoryService.GetCategoryById(id);
        
            if(categoria == null)
            {
               return BadRequest("No encontrado");
            }
            return Ok(categoria);
            
        }

        // PUT: api/Categorias/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategorias(int id, Categorias categorias)
        {
            if (id != categorias.CategoriaId)
            {
                return BadRequest("El ID de la URL no coincide con el ID del objeto.");
            }


            try
            {
                await _categoryService.PutAsync(categorias);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }


        // POST: api/Categorias
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Categorias>> PostCategorias(Categorias categorias)
        {
            if (categorias == null)
            {
                return BadRequest("La categoria no puede ser nula.");
            }

            var nuevaCategoria = await _categoryService.AddAsync(categorias);

            return CreatedAtAction(nameof(GetCategoryById), new { id = nuevaCategoria?.CategoriaId }, nuevaCategoria);
        }

        // DELETE: api/Categorias/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            try
            {
                var deletedCategoria = await _categoryService.DeleteAsync(id);
                return Ok(deletedCategoria);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        private async Task<bool> CategoriasExists(int id)
        {
            var categorias = await _categoryService.GetCategoryById(id);
            return categorias != null;
        }
    }
}
