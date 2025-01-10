using Shared.Models;

namespace SistemaLlavesWebAPI.Abstractions;

public interface IProviderService
{
    public Task<List<Proveedores>> GetAsync();
    public Task<bool> AddAsync(Proveedores proveedores);
    public Task<Proveedores> PutAsync(Proveedores proveedores);
    public Task<Proveedores> DeleteAsync(int proveedorId);
    public Task<Proveedores> GetByIdAsync(int proveedorId);
}