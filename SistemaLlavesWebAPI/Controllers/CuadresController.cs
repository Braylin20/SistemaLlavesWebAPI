using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.Models;
using SistemaLlavesWebAPI.Dal;
using SistemaLlavesWebAPI.Abstractions;
using SistemaLlavesWebAPI.Abstractions.Interfaces;
using SistemaLlavesWebAPI.Services;

namespace SistemaLlavesWebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
[ExcludeFromCodeCoverage]
public class CuadresController : ControllerBase
{
    private readonly ICuadresService cuadresService;

    public CuadresController(ICuadresService cuadresService)
    {
        this.cuadresService = cuadresService;
    }

    // GET: api/Cuadres
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Cuadres>>> GetCuadres()
    {
        return await cuadresService.GetAsync();
    }

    // GET: api/Cuadres/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Cuadres>> GetCuadre(int id)
    {
        var cuadre = await cuadresService.GetByIdAsync(id);

        if (cuadre is null)
        {
            return NotFound();
        }

        return cuadre;
    }

    // PUT: api/Cuadres/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutCuadres(int id, Cuadres cuadres)
    {
        if (id != cuadres.CuadreId) return BadRequest();
        try
        {
            await cuadresService.PutAsync(cuadres);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }

    }

    // POST: api/Cuadres
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<Cuadres>> PostCuadre(Cuadres cuadres)
    {
        if (!await cuadresService.AddAsync(cuadres)) return BadRequest();
        return CreatedAtAction("GetCuadres", new { id = cuadres.CuadreId }, cuadres);
    }

    // DELETE: api/Cuadres/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCuadres(int id)
    {
        var cuadre = await cuadresService.DeleteAsync(id);

        if (cuadre is null) return NotFound(id);

        return Ok(cuadre);
    }
}
