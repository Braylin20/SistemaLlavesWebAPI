using Shared.Models;

namespace SistemaLlavesWebAPI.Interfaces;

public interface IProviderService
{
    Task<List<Proveedores>> GetAsync();
    Task<bool> AddAsync(Proveedores proveedores);
    Task<Proveedores> PutAsync(Proveedores proveedores);
    Task<Proveedores> DeleteAsync(int proveedorId);
}