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
    public class GarantiasController : ControllerBase
    {
        private readonly IWarrantyService _warrantyService;

        public GarantiasController(IWarrantyService warrantyService)
        {
            _warrantyService = warrantyService;
        }

        // GET: api/Garantias
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Garantias>>> GetGarantias()
        {
            return await _warrantyService.GetAsync();
        }

        // GET: api/Garantias/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Garantias>> GetGarantias(int id)
        {
            var garantias = await _warrantyService.GetById(id);

            if (garantias == null)
            {
                return NotFound();
            }

            return garantias;
        }


        // POST: api/Garantias
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Garantias>> PostGarantias(Garantias garantias)
        {
           if(!await _warrantyService.AddAsync(garantias)) return BadRequest();
         
            return CreatedAtAction("GetGarantias", new { id = garantias.GarantiaId }, garantias);
        }


        // PUT: api/Garantias/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGarantias(int id, Garantias garantias)
        {
           if(id != garantias.GarantiaId) return BadRequest();
            try
            {
                var updatedWarranty = await _warrantyService.PutAsync(garantias);
                return Ok(updatedWarranty);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        // DELETE: api/Garantias/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGarantias(int id)
        {
            var garantias = await _warrantyService.DeleteAsync(id);
            if (garantias == null)
            {
                return NotFound();
            }
            return NoContent();
        }

    
    }
}
