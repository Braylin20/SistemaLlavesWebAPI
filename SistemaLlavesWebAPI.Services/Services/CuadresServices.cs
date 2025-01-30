using Microsoft.EntityFrameworkCore;
using Shared.Models;
using SistemaLlavesWebAPI.Abstractions.Interfaces;
using SistemaLlavesWebAPI.Dal;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaLlavesWebAPI.Services.Services;

[ExcludeFromCodeCoverage]
public class CuadresServices(Context context) : ICuadresService
{
    private readonly Context _context =context;

    public async Task<List<Cuadres>> GetAsync()
    {
        return await _context.Cuadres.ToListAsync();
    }
    public async Task<Cuadres> GetByIdAsync(int cuadresId)
    {
        var cuadre = await _context.Cuadres.FindAsync(cuadresId);
        if(cuadre is null)
            throw new KeyNotFoundException("Cuadre no encontrado.");

        return cuadre;
    }
    public async Task<bool> AddAsync(Cuadres cuadres)
    {
        await _context.Cuadres.AddAsync(cuadres);
        return await _context.SaveChangesAsync() > 0;
    }
    public async Task<bool> PutAsync(Cuadres cuadres)
    {
        var existingEntity = await _context.Cuadres.FindAsync(cuadres.CuadreId);
        if (existingEntity is null) return false;

        _context.Entry(existingEntity).State = EntityState.Detached;

        _context.Update(cuadres);
        return await _context.SaveChangesAsync() > 0;
    }
    public async Task<Cuadres?> DeleteAsync(int cuadresId)
    {
        var cuadre = await _context.Cuadres.FindAsync(cuadresId);
        if (cuadre != null)
        {
            _context.Remove(cuadre);
            await _context.SaveChangesAsync();
        }

        return cuadre;
    }
}
