using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Mvc;
using Shared.Models;
using SistemaLlavesWebAPI.Interfaces;

namespace SistemaLlavesWebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
[ExcludeFromCodeCoverage]
public class ClientesController(IClientService clientService) : ControllerBase
{
    // GET: api/Clientes
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Clientes>>> GetClientes()
    {
        return await clientService.GetAsync();
    }

    // GET: api/Clientes/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Clientes>> GetClientes(int id)
    {
        try
        {
            return await clientService.GetByIdAsync(id);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }

    // POST: api/Clientes
    [HttpPost]
    public async Task<ActionResult<Clientes>> PostClientes(Clientes clientes)
    {
        if (!await clientService.AddAsync(clientes))
        {
            return BadRequest();
        }

        return CreatedAtAction("GetClientes", new { id = clientes.ClienteId }, clientes);
    }

    // PUT: api/Clientes/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutClientes(int id, Clientes clientes)
    {
        if (id != clientes.ClienteId)
        {
            return BadRequest();
        }

        try
        {
            var updatedClient = await clientService.PutAsync(clientes);
            return Ok(updatedClient);
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

    // DELETE: api/Clientes/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteClientes(int id)
    {
        try
        {
            var deletedCliente = await clientService.DeleteAsync(id);
            return Ok(deletedCliente); // Return the deleted entity or NoContent() as required.
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }
}