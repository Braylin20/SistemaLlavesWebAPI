using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shared.Models;
using SistemaLlavesWebAPI.Controllers;
using SistemaLlavesWebAPI.Dal;
using SistemaLlavesWebAPI.Services;

namespace TestServices.Controllers;

public class TestClientesController : IDisposable
{
    private readonly Context _context;
    private readonly ClientesController _controller;

    public TestClientesController()
    {
        var options = new DbContextOptionsBuilder<Context>()
            .UseInMemoryDatabase("TestClientControllerDatabase")
            .Options;
        _context = new Context(options);

        var clientService = new ClientService(_context);
        _controller = new ClientesController(clientService);
    }

    public void Dispose()
    {
        _context.Database.EnsureDeleted();
        _context.Dispose();
        GC.SuppressFinalize(this);
    }

    [Fact]
    public async Task GetClientes_ShouldReturnAllClients()
    {
        // Arrange
        _context.Clientes.Add(new Clientes { ClienteId = 1, Nombre = "Cliente 1" });
        _context.Clientes.Add(new Clientes { ClienteId = 2, Nombre = "Cliente 2" });
        await _context.SaveChangesAsync();

        // Act
        var result = await _controller.GetClientes();

        // Assert
        Assert.Equal(2, result.Value?.Count());
    }

    [Fact]
    public async Task GetClientesById_ShouldReturnClient_WhenExists()
    {
        // Arrange
        var client = new Clientes { ClienteId = 1, Nombre = "Cliente Existente" };
        _context.Clientes.Add(client);
        await _context.SaveChangesAsync();

        // Act
        var result = await _controller.GetClientes(1);

        // Assert
        Assert.NotNull(result.Value);
        Assert.Equal(client.ClienteId, result.Value.ClienteId);
    }

    [Fact]
    public async Task GetClientesById_ShouldReturnNotFound_WhenClientDoesNotExist()
    {
        // Act
        var result = await _controller.GetClientes(99);

        // Assert
        Assert.IsType<NotFoundResult>(result.Result);
    }

    [Fact]
    public async Task PostClientes_ShouldAddClient()
    {
        // Arrange
        var client = new Clientes { ClienteId = 1, Nombre = "Nuevo Cliente" };

        // Act
        var result = await _controller.PostClientes(client);

        // Assert
        Assert.IsType<CreatedAtActionResult>(result.Result);
        Assert.Equal(1, await _context.Clientes.CountAsync());
    }

    [Fact]
    public async Task PutClientes_ShouldUpdateClient()
    {
        // Arrange
        var client = new Clientes { ClienteId = 1, Nombre = "Cliente Original" };
        _context.Clientes.Add(client);
        await _context.SaveChangesAsync();

        var updatedClient = new Clientes { ClienteId = 1, Nombre = "Cliente Actualizado" };

        // Act
        var result = await _controller.PutClientes(1, updatedClient);

        // Assert
        Assert.IsType<OkObjectResult>(result);
        var dbClient = await _context.Clientes.FindAsync(1);
        Assert.NotNull(dbClient);
        Assert.Equal(updatedClient.Nombre, dbClient.Nombre);
    }

    [Fact]
    public async Task PutClientes_ShouldReturnNotFound_WhenClientDoesNotExist()
    {
        // Arrange
        var updatedClient = new Clientes { ClienteId = 99, Nombre = "Non-Existent Client" };

        // Act
        var result = await _controller.PutClientes(99, updatedClient);

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task DeleteClientes_ShouldRemoveClient()
    {
        // Arrange
        var client = new Clientes { ClienteId = 1, Nombre = "Cliente a eliminar" };
        _context.Clientes.Add(client);
        await _context.SaveChangesAsync();

        // Act
        var result = await _controller.DeleteClientes(1);

        // Assert
        Assert.IsType<OkObjectResult>(result);
        Assert.Equal(0, await _context.Clientes.CountAsync());
    }

    [Fact]
    public async Task DeleteClientes_ShouldReturnNotFound_WhenClientDoesNotExist()
    {
        // Act
        var result = await _controller.DeleteClientes(99);

        // Assert
        Assert.IsType<NotFoundObjectResult>(result);
    }
    
    [Fact]
    public async Task PutClientes_ShouldReturnBadRequest_WhenIdsMismatch()
    {
        // Arrange
        var client = new Clientes { ClienteId = 2, Nombre = "Cliente Actualizado" };

        // Act
        var result = await _controller.PutClientes(1, client);

        // Assert
        Assert.IsType<BadRequestResult>(result);
    }
    
    [Fact]
    public async Task GetClientes_ShouldReturnEmptyList_WhenNoClientsExist()
    {
        // Act
        var result = await _controller.GetClientes();

        // Assert
        Assert.NotNull(result.Value);
        Assert.Empty(result.Value);
    }
    
    [Fact]
    public async Task GetClientes_ShouldHandleLargeDataSet()
    {
        // Arrange
        for (int i = 1; i <= 1000; i++)
        {
            _context.Clientes.Add(new Clientes { ClienteId = i, Nombre = $"Cliente {i}" });
        }
        await _context.SaveChangesAsync();

        // Act
        var result = await _controller.GetClientes();

        // Assert
        Assert.Equal(1000, result.Value?.Count());
    }
}
