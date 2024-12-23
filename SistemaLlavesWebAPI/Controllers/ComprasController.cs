using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Mvc;
using Shared.Models;
using SistemaLlavesWebAPI.Interfaces;
using SistemaLlavesWebAPI.Services;

namespace SistemaLlavesWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ExcludeFromCodeCoverage]
    public class ComprasController : ControllerBase
    {
        private readonly IPuchaseService _puchaseService;

        public ComprasController(IPuchaseService purchaseService)
        {
            _puchaseService = purchaseService;
        }

        // GET: api/Compras
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Compras>>> GetCompras()
        {
            return await _puchaseService.GetAllAsync();
        }

        // GET: api/Compras/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Compras>> GetCompras(int id)
        {
            var compras = await _puchaseService.GetById(id);

            if (compras == null)
            {
                return NotFound();
            }

            return compras;
        }

        // PUT: api/Compras/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCompras(int id, Compras compra)
        {
            if (id != compra.CompraId) return BadRequest();
            try
            {
                var compraActualizada = await _puchaseService.PutAsync(compra);
                return Ok(compraActualizada);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST: api/Compras
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Compras>> PostCompras(Compras compras)
        {
            if (!await _puchaseService.AddAsync(compras)) return BadRequest();
            return CreatedAtAction("GetProductos", new { id = compras.CompraId }, compras);

        }

        // DELETE: api/Compras/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCompras(int id)
        {
            var puchase = await _puchaseService.DeleteAsync(id);

            if (puchase is null) return NotFound(id);

            return Ok(puchase);
        }
    }
}
