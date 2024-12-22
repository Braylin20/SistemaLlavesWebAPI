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

namespace SistemaLlavesWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ExcludeFromCodeCoverage]
    public class MetodosPagosController : ControllerBase
    {
        private readonly Context _context;

        public MetodosPagosController(Context context)
        {
            _context = context;
        }

        // GET: api/MetodosPagos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MetodosPagos>>> GetMetodosPagos()
        {
            return await _context.MetodosPagos.ToListAsync();
        }

        // GET: api/MetodosPagos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MetodosPagos>> GetMetodosPagos(int id)
        {
            var metodosPagos = await _context.MetodosPagos.FindAsync(id);

            if (metodosPagos == null)
            {
                return NotFound();
            }

            return metodosPagos;
        }

        // PUT: api/MetodosPagos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMetodosPagos(int id, MetodosPagos metodosPagos)
        {
            if (id != metodosPagos.MetodoPagoId)
            {
                return BadRequest();
            }

            _context.Entry(metodosPagos).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MetodosPagosExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/MetodosPagos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<MetodosPagos>> PostMetodosPagos(MetodosPagos metodosPagos)
        {
            _context.MetodosPagos.Add(metodosPagos);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMetodosPagos", new { id = metodosPagos.MetodoPagoId }, metodosPagos);
        }

        // DELETE: api/MetodosPagos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMetodosPagos(int id)
        {
            var metodosPagos = await _context.MetodosPagos.FindAsync(id);
            if (metodosPagos == null)
            {
                return NotFound();
            }

            _context.MetodosPagos.Remove(metodosPagos);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MetodosPagosExists(int id)
        {
            return _context.MetodosPagos.Any(e => e.MetodoPagoId == id);
        }
    }
}
