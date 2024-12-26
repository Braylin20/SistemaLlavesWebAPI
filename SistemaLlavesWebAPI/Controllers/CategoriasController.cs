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
    [ExcludeFromCodeCoverage]
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
           var categoria = await _categoryService.GetAsync();
            if(categoria == null)
            {
                return NotFound();
            }
            return Ok(categoria);
        }

        // GET: api/Categorias/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Categorias>> GetCategoryById(int id)
        { 
            if (id <= 0)
            {
                return BadRequest("Error, no es valido el id");
            }
            var categorias = await _categoryService.GetCategoryById(id);
            if (categorias == null)
            {
                NotFound("No se encontro el id");
            }
            return Ok(categorias);
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

            if (!await CategoriasExists(id))
            {
                return NotFound($"No se encontró la categoria con el ID = {id}.");
            }

            try
            {
                var categoriaActualizada = await _categoryService.PutAsync(categorias);
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

            return CreatedAtAction(nameof(GetCategoryById), new { id = nuevaCategoria.CategoriaId }, nuevaCategoria);
        }

        // DELETE: api/Categorias/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategorias(int id)
        {
            if (id <= 0)
            {
                return BadRequest("El ID debe ser mayor que 0.");
            }

            if (!await CategoriasExists(id))
            {
                return NotFound($"No se encontró la categoria con el ID = {id}.");
            }

            var resultado = await _categoryService.DeleteAsync(id);
            return NoContent();
        }

        private async Task<bool> CategoriasExists(int id)
        {
            var categorias = await _categoryService.GetCategoryById(id);
            return categorias != null;
        }
    }
}
