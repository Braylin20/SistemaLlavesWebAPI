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
    public class MetodosPagosController : ControllerBase
    {
        private readonly IMetodoPagosService _metodoPagoService;

        public MetodosPagosController(IMetodoPagosService metodoPagosService)
        {
            _metodoPagoService = metodoPagosService;
        }

        // GET: api/MetodosPagos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MetodosPagos>>> GetMetodosPagos()
        {
            return await _metodoPagoService.GetAsync();
        }

        // GET: api/MetodosPagos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MetodosPagos>> GetMetodosPagos(int id)
        {
            var metodosPagos = await _metodoPagoService.GetById(id);

            if (metodosPagos == null)
            {
                return NotFound();
            }

            return metodosPagos;
        }

        // PUT: api/MetodosPagos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMetodosPagos(int id, MetodosPagos metodosPago)
        {
            if (id != metodosPago.MetodoPagoId)
                return BadRequest();

            try
            {
                await _metodoPagoService.PutAsync(metodosPago);
                return Ok();
            }
            catch (DbUpdateConcurrencyException)
            {
                return BadRequest();
            }

            
        }

        // POST: api/MetodosPagos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<MetodosPagos>> PostMetodosPagos(MetodosPagos metodosPagos)
        {
            if(!await _metodoPagoService.AddAsync(metodosPagos))
                return BadRequest();

            return CreatedAtAction("GetMetodosPagos", new { id = metodosPagos.MetodoPagoId }, metodosPagos);
        }

        // DELETE: api/MetodosPagos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMetodosPagos(int id)
        {
            var metodoPago = await _metodoPagoService.DeleteAsync(id);
            if(!metodoPago) 
                return NotFound(id);

            return Ok(metodoPago);
        }
    }
}
