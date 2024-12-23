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
    public class GarantiasController : ControllerBase
    {
        private readonly Context _context;

        public GarantiasController(Context context)
        {
            _context = context;
        }

        // GET: api/Garantias
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Garantias>>> GetGarantias()
        {
            return await _context.Garantias.ToListAsync();
        }

        // GET: api/Garantias/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Garantias>> GetGarantias(int id)
        {
            var garantias = await _context.Garantias.FindAsync(id);

            if (garantias == null)
            {
                return NotFound();
            }

            return garantias;
        }

        // PUT: api/Garantias/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGarantias(int id, Garantias garantias)
        {
            if (id != garantias.GarantiaId)
            {
                return BadRequest();
            }

            _context.Entry(garantias).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GarantiasExists(id))
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

        // POST: api/Garantias
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Garantias>> PostGarantias(Garantias garantias)
        {
            _context.Garantias.Add(garantias);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetGarantias", new { id = garantias.GarantiaId }, garantias);
        }

        // DELETE: api/Garantias/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGarantias(int id)
        {
            var garantias = await _context.Garantias.FindAsync(id);
            if (garantias == null)
            {
                return NotFound();
            }

            _context.Garantias.Remove(garantias);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool GarantiasExists(int id)
        {
            return _context.Garantias.Any(e => e.GarantiaId == id);
        }
    }
}
