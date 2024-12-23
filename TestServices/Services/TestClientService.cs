using Microsoft.EntityFrameworkCore;
using Shared.Models;
using SistemaLlavesWebAPI.Dal;
using SistemaLlavesWebAPI.Services;

namespace TestServices.Services;

public class TestClientService : IDisposable
{
    private readonly Context _context;
    private readonly ClientService _service;

    public TestClientService()
    {
        var options = new DbContextOptionsBuilder<Context>()
            .UseInMemoryDatabase("TestClientDatabase")
            .Options;
        _context = new Context(options);
        _service = new ClientService(_context);
    }

    public void Dispose()
    {
        _context.Database.EnsureDeleted();
        _context.Dispose();
        GC.SuppressFinalize(this);
    }

    [Fact]
    public async Task GetAsync_ShouldReturnAllClients()
    {
        // Arrange
        _context.Clientes.Add(new Clientes { ClienteId = 1, Nombre = "Cliente 1" });
        _context.Clientes.Add(new Clientes { ClienteId = 2, Nombre = "Cliente 2" });
        await _context.SaveChangesAsync();

        // Act
        var result = await _service.GetAsync();

        // Assert
        Assert.Equal(2, result.Count);
    }

    [Fact]
    public async Task AddAsync_ShouldAddClient()
    {
        // Arrange
        var client = new Clientes { ClienteId = 1, Nombre = "Nuevo Cliente" };

        // Act
        var result = await _service.AddAsync(client);

        // Assert
        Assert.True(result);
        Assert.Equal(1, await _context.Clientes.CountAsync());
    }

    [Fact]
    public async Task DeleteAsync_ShouldRemoveClient()
    {
        // Arrange
        var client = new Clientes { ClienteId = 1, Nombre = "Cliente a eliminar" };
        _context.Clientes.Add(client);
        await _context.SaveChangesAsync();

        // Act
        var result = await _service.DeleteAsync(1);

        // Assert
        Assert.Equal(0, await _context.Clientes.CountAsync());
        Assert.Equal(client.ClienteId, result.ClienteId);
    }

    [Fact]
    public async Task PutAsync_ShouldUpdateClient()
    {
        // Arrange
        var client = new Clientes { ClienteId = 1, Nombre = "Cliente Original" };
        _context.Clientes.Add(client);
        await _context.SaveChangesAsync();

        var updatedClient = new Clientes { ClienteId = 1, Nombre = "Cliente Actualizado" };

        // Act
        var result = await _service.PutAsync(updatedClient);

        // Assert
        var dbClient = await _context.Clientes.FindAsync(1);
        Assert.NotNull(dbClient);
        Assert.Equal(updatedClient.Nombre, dbClient.Nombre);
    }

    [Fact]
    public async Task DeleteAsync_ShouldThrowException_WhenClientNotFound()
    {
        // Act & Assert
        await Assert.ThrowsAsync<KeyNotFoundException>(() => _service.DeleteAsync(99));
    }
}