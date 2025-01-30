using Microsoft.EntityFrameworkCore;
using Shared.Models;
using SistemaLlavesWebAPI.Abstractions.Interfaces;
using SistemaLlavesWebAPI.Dal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaLlavesWebAPI.Services.Services;

public class CuadresServices(Context context) : ICuadresService
{
    private readonly Context _context =context;

    public async Task<List<Cuadres>> GetAsync()
    {
        return await _context.Cuadres.ToListAsync();
    }
    public async Task<Cuadres> GetByIdAsync(int clientesId)
    {
        var cuadre = await _context.Cuadres.FindAsync(clientesId);
        if(cuadre is null)
            throw new KeyNotFoundException("Cuadre no encontrado.");

        return cuadre;
    }
    public async Task<bool> AddAsync(Cuadres cuadres)
    {
        await _context.Cuadres.AddAsync(cuadres);
        return _context.SaveChanges() > 0;
    }
    public async Task<bool> PutAsync(Cuadres cuadres)
    {
        var existingEntity = await _context.Cuadres.FindAsync(cuadres.CuadreId);
        if (existingEntity is null) return false;

        _context.Entry(existingEntity).State = EntityState.Detached;

        _context.Update(cuadres);
        return await _context.SaveChangesAsync() > 0;
    }
    public async Task<Cuadres?> DeleteAsync(int clientesId)
    {
        var cuadre = await _context.Cuadres.FindAsync(clientesId);
        if (cuadre != null)
        {
            _context.Remove(cuadre);
            await _context.SaveChangesAsync();
        }

        return cuadre;
    }
}
