using Microsoft.EntityFrameworkCore;
using Shared.Models;
using SistemaLlavesWebAPI.Dal;
using SistemaLlavesWebAPI.Services;

namespace TestServices.Services;

public class TestProviderService : IDisposable
{
    private readonly Context _context;
    private readonly ProviderService _service;

    public TestProviderService()
    {
        var options = new DbContextOptionsBuilder<Context>()
            .UseInMemoryDatabase("TestProviderDatabase")
            .Options;
        _context = new Context(options);
        _service = new ProviderService(_context);
    }

    public void Dispose()
    {
        _context.Database.EnsureDeleted();
        _context.Dispose();
        GC.SuppressFinalize(this);
    }

    [Fact]
    public async Task GetAsync_ShouldReturnAllProviders()
    {
        // Arrange
        _context.Proveedores.Add(new Proveedores { ProovedorId = 1, Nombre = "Proveedor 1" });
        _context.Proveedores.Add(new Proveedores { ProovedorId = 2, Nombre = "Proveedor 2" });
        await _context.SaveChangesAsync();

        // Act
        var result = await _service.GetAsync();

        // Assert
        Assert.Equal(2, result.Count);
    }

    [Fact]
    public async Task AddAsync_ShouldAddProvider()
    {
        // Arrange
        var provider = new Proveedores { ProovedorId = 1, Nombre = "Nuevo Proveedor" };

        // Act
        var result = await _service.AddAsync(provider);

        // Assert
        Assert.True(result);
        Assert.Equal(1, await _context.Proveedores.CountAsync());
    }

    [Fact]
    public async Task DeleteAsync_ShouldRemoveProvider()
    {
        // Arrange
        var provider = new Proveedores { ProovedorId = 1, Nombre = "Proveedor a eliminar" };
        _context.Proveedores.Add(provider);
        await _context.SaveChangesAsync();

        // Act
        var result = await _service.DeleteAsync(1);

        // Assert
        Assert.Equal(0, await _context.Proveedores.CountAsync());
        Assert.Equal(provider.ProovedorId, result.ProovedorId);
    }

    [Fact]
    public async Task PutAsync_ShouldUpdateProvider()
    {
        // Arrange
        var provider = new Proveedores { ProovedorId = 1, Nombre = "Proveedor Original" };
        _context.Proveedores.Add(provider);
        await _context.SaveChangesAsync();

        var updatedProvider = new Proveedores { ProovedorId = 1, Nombre = "Proveedor Actualizado" };

        // Act
        var result = await _service.PutAsync(updatedProvider);

        // Assert
        var dbProvider = await _context.Proveedores.FindAsync(1);
        Assert.NotNull(dbProvider);
        Assert.Equal(updatedProvider.Nombre, dbProvider.Nombre);
    }

    [Fact]
    public async Task DeleteAsync_ShouldThrowException_WhenProviderNotFound()
    {
        // Act & Assert
        await Assert.ThrowsAsync<KeyNotFoundException>(() => _service.DeleteAsync(99));
    }
    
    [Fact]
    public async Task GetByIdAsync_ShouldReturnProvider_WhenProviderExists()
    {
        // Arrange
        var provider = new Proveedores { ProovedorId = 1, Nombre = "Proveedor Existente" };
        _context.Proveedores.Add(provider);
        await _context.SaveChangesAsync();

        // Act
        var result = await _service.GetByIdAsync(1);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(provider.ProovedorId, result.ProovedorId);
        Assert.Equal(provider.Nombre, result.Nombre);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldThrowException_WhenProviderNotFound()
    {
        // Act & Assert
        await Assert.ThrowsAsync<KeyNotFoundException>(() => _service.GetByIdAsync(99));
    }
}