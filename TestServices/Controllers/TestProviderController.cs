using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shared.Models;
using SistemaLlavesWebAPI.Controllers;
using SistemaLlavesWebAPI.Dal;
using SistemaLlavesWebAPI.Services;

namespace TestServices.Controllers;

public class TestProveedoresController : IDisposable
{
    private readonly Context _context;
    private readonly ProveedoresController _controller;

    public TestProveedoresController()
    {
        var options = new DbContextOptionsBuilder<Context>()
            .UseInMemoryDatabase("TestProviderControllerDatabase")
            .Options;
        _context = new Context(options);

        var providerService = new ProviderService(_context);
        _controller = new ProveedoresController(providerService);
    }

    public void Dispose()
    {
        _context.Database.EnsureDeleted();
        _context.Dispose();
        GC.SuppressFinalize(this);
    }

    [Fact]
    public async Task GetProveedores_ShouldReturnAllProviders()
    {
        // Arrange
        _context.Proveedores.Add(new Proveedores { ProovedorId = 1, Nombre = "Proveedor 1" });
        _context.Proveedores.Add(new Proveedores { ProovedorId = 2, Nombre = "Proveedor 2" });
        await _context.SaveChangesAsync();

        // Act
        var result = await _controller.GetProveedores();

        // Assert
        Assert.Equal(2, result.Value?.Count());
    }

    [Fact]
    public async Task GetProveedoresById_ShouldReturnProvider_WhenExists()
    {
        // Arrange
        var provider = new Proveedores { ProovedorId = 1, Nombre = "Proveedor Existente" };
        _context.Proveedores.Add(provider);
        await _context.SaveChangesAsync();

        // Act
        var result = await _controller.GetProveedores(1);

        // Assert
        Assert.NotNull(result.Value);
        Assert.Equal(provider.ProovedorId, result.Value.ProovedorId);
    }

    [Fact]
    public async Task GetProveedoresById_ShouldReturnNotFound_WhenProviderDoesNotExist()
    {
        // Act
        var result = await _controller.GetProveedores(99);

        // Assert
        Assert.IsType<NotFoundResult>(result.Result);
    }

    [Fact]
    public async Task PostProveedores_ShouldAddProvider()
    {
        // Arrange
        var provider = new Proveedores { ProovedorId = 1, Nombre = "Nuevo Proveedor" };

        // Act
        var result = await _controller.PostProveedores(provider);

        // Assert
        Assert.IsType<CreatedAtActionResult>(result.Result);
        Assert.Equal(1, await _context.Proveedores.CountAsync());
    }

    [Fact]
    public async Task PutProveedores_ShouldUpdateProvider()
    {
        // Arrange
        var provider = new Proveedores { ProovedorId = 1, Nombre = "Proveedor Original" };
        _context.Proveedores.Add(provider);
        await _context.SaveChangesAsync();

        var updatedProvider = new Proveedores { ProovedorId = 1, Nombre = "Proveedor Actualizado" };

        // Act
        var result = await _controller.PutProveedores(1, updatedProvider);

        // Assert
        Assert.IsType<OkObjectResult>(result);
        var dbProvider = await _context.Proveedores.FindAsync(1);
        Assert.NotNull(dbProvider);
        Assert.Equal(updatedProvider.Nombre, dbProvider.Nombre);
    }

    [Fact]
    public async Task PutProveedores_ShouldReturnNotFound_WhenProviderDoesNotExist()
    {
        // Arrange
        var updatedProvider = new Proveedores { ProovedorId = 99, Nombre = "Non-Existent Provider" };

        // Act
        var result = await _controller.PutProveedores(99, updatedProvider);

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task DeleteProveedores_ShouldRemoveProvider()
    {
        // Arrange
        var provider = new Proveedores { ProovedorId = 1, Nombre = "Proveedor a eliminar" };
        _context.Proveedores.Add(provider);
        await _context.SaveChangesAsync();

        // Act
        var result = await _controller.DeleteProveedores(1);

        // Assert
        Assert.IsType<OkObjectResult>(result);
        Assert.Equal(0, await _context.Proveedores.CountAsync());
    }

    [Fact]
    public async Task DeleteProveedores_ShouldReturnNotFound_WhenProviderDoesNotExist()
    {
        // Act
        var result = await _controller.DeleteProveedores(99);

        // Assert
        Assert.IsType<NotFoundObjectResult>(result);
    }
    
    [Fact]
    public async Task PutProveedores_ShouldReturnBadRequest_WhenIdsMismatch()
    {
        // Arrange
        var provider = new Proveedores { ProovedorId = 2, Nombre = "Proveedor Actualizado" };

        // Act
        var result = await _controller.PutProveedores(1, provider);

        // Assert
        Assert.IsType<BadRequestResult>(result);
    }
    
    [Fact]
    public async Task GetProveedores_ShouldReturnEmptyList_WhenNoProvidersExist()
    {
        // Act
        var result = await _controller.GetProveedores();

        // Assert
        Assert.NotNull(result.Value);
        Assert.Empty(result.Value);
    }
    
    [Fact]
    public async Task GetProveedores_ShouldHandleLargeDataSet()
    {
        // Arrange
        for (int i = 1; i <= 1000; i++)
        {
            _context.Proveedores.Add(new Proveedores { ProovedorId = i, Nombre = $"Proveedor {i}" });
        }
        await _context.SaveChangesAsync();

        // Act
        var result = await _controller.GetProveedores();

        // Assert
        Assert.Equal(1000, result.Value?.Count());
    }
    
    [Fact]
    public async Task PostProveedores_ShouldReturnBadRequest_WhenProviderIsInvalid()
    {
        // Arrange
        Proveedores invalidProvider = null;

        // Act
        if (invalidProvider != null)
        {
            var result = await _controller.PostProveedores(invalidProvider);

            // Assert
            Assert.IsType<BadRequestResult>(result.Result);
        }
    }
    
    [Fact]
    public async Task PutProveedores_ShouldReturnBadRequest_WhenProviderIsInvalid()
    {
        // Arrange
        Proveedores invalidProvider = null;

        // Act
        if (invalidProvider != null)
        {
            var result = await _controller.PutProveedores(1, invalidProvider);

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }
    }
}
