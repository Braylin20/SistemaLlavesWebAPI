﻿using Shared.Models;

namespace SistemaLlavesWebAPI.Abstractions;

public interface ISalesService
{
    Task<IEnumerable<Ventas>> GetAsync();
    Task<Ventas> AddAsync(Ventas ventas);
    Task<Ventas> PutAsync(Ventas venta);
    Task<bool> DeleteAsync(int ventaId);
    Task<Ventas?> GetVentaById(int id);
}