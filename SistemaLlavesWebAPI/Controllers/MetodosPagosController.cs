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
using SistemaLlavesWebAPI.Abstractions;

namespace SistemaLlavesWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MetodosPagosController : ControllerBase
    {
        private readonly IMetodoPagosService metodoPagosService;


        public MetodosPagosController(IMetodoPagosService metodoPagosService)
        {
            this.metodoPagosService = metodoPagosService;
        }



        // GET: api/MetodosPagos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MetodosPagos>>> GetMetodosPagos()
        {
            return await metodoPagosService.GetAsync();
        }

        // GET: api/MetodosPagos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MetodosPagos>> GetMetodosPagos(int id)
        {
            var metodosPagos = await metodoPagosService.GetById(id);

            if (metodosPagos == null) return NotFound();

            return metodosPagos;
        }

        // PUT: api/MetodosPagos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMetodosPagos(int id, MetodosPagos metodosPagos)
        {
            if (id != metodosPagos.MetodoPagoId) return BadRequest();

            try
            {
                await metodoPagosService.PutAsync(metodosPagos);
                return Ok();
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        // POST: api/MetodosPagos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<MetodosPagos>> PostMetodosPagos(MetodosPagos metodosPagos)
        {
            if(!await metodoPagosService.AddAsync(metodosPagos)) return BadRequest();

            return CreatedAtAction("GetMetodosPagos", new { id = metodosPagos.MetodoPagoId }, metodosPagos);
        }

        // DELETE: api/MetodosPagos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMetodosPagos(int id)
        {
            var metodoPago = await metodoPagosService.DeleteAsync(id);
            if (metodoPago is null) return NotFound();

            return Ok(metodoPago);
        }
    }
}
