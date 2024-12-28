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
    public class VentasController : ControllerBase
    {
        private readonly ISalesService _salesService;

        public VentasController(ISalesService salesService)
        {
            _salesService = salesService;
        }

        // GET: api/Ventas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Ventas>>> GetVentas()
        {
            var ventas = await _salesService.GetAsync();
            if (ventas == null || !ventas.Any())
            {
                return NotFound("No se encontraron ventas.");
            }
            return Ok(ventas);
        }

        // GET: api/Ventas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Ventas>> GetVentasById(int id)
        {
            if (id <= 0)
            {
                return BadRequest("El ID debe ser mayor a 0.");
            }

            var venta = await _salesService.GetVentaById(id);
            if (venta == null)
            {
                return NotFound($"No se encontró la venta con el ID = {id}.");
            }
            return Ok(venta);
        }

        // PUT: api/Ventas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVentas(int id, Ventas ventas)
        {
            if (id != ventas.VentaId)
            {
                return BadRequest("El ID de la URL no coincide con el ID del objeto.");
            }

            if (!await VentasExists(id))
            {
                return NotFound($"No se encontró la venta con el ID = {id}.");
            }

            try
            {
                await _salesService.PutAsync(ventas); 
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        // POST: api/Ventas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Ventas>> PostVentas(Ventas ventas)
        {
            try
            {
                var nuevaVenta = await _salesService.AddAsync(ventas);
                return CreatedAtAction(nameof(GetVentasById), new { id = nuevaVenta.VentaId }, nuevaVenta);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }

        }

        // DELETE: api/Ventas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVentas(int id)
        {
            if (id <= 0)
            {
                return BadRequest("El ID debe ser mayor que 0.");
            }

            if (!await VentasExists(id))
            {
                return NotFound($"No se encontró la venta con el ID = {id}.");
            }

            var resultado = await _salesService.DeleteAsync(id);
            return NoContent();
        }

        private async Task<bool> VentasExists(int id)
        {
            var venta = await _salesService.GetVentaById(id);
            return venta != null;
        }

    }
}
