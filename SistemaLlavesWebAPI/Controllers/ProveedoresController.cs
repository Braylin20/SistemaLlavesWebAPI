using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Mvc;
using Shared.Models;
using SistemaLlavesWebAPI.Interfaces;

namespace SistemaLlavesWebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
[ExcludeFromCodeCoverage]
public class ProveedoresController(IProviderService providerService) : ControllerBase
{
    // GET: api/Proveedores
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Proveedores>>> GetProveedores()
    {
        return await providerService.GetAsync();
    }

    // GET: api/Proveedores/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Proveedores>> GetProveedores(int id)
    {
        try
        {
            return await providerService.GetByIdAsync(id);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }

    // POST: api/Proveedores
    [HttpPost]
    public async Task<ActionResult<Proveedores>> PostProveedores(Proveedores proveedores)
    {
        if (!await providerService.AddAsync(proveedores))
        {
            return BadRequest();
        }

        return CreatedAtAction("GetProveedores", new { id = proveedores.ProovedorId }, proveedores);
    }

    // PUT: api/Proveedores/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutProveedores(int id, Proveedores proveedores)
    {
        if (id != proveedores.ProovedorId)
        {
            return BadRequest();
        }

        try
        {
            var updatedProvider = await providerService.PutAsync(proveedores);
            return Ok(updatedProvider);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    // DELETE: api/Proveedores/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProveedores(int id)
    {
        try
        {
            var deletedProvider = await providerService.DeleteAsync(id);
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }
}