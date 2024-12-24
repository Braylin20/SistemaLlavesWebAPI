using Microsoft.AspNetCore.Mvc;
using Moq;
using Shared.Models;
using SistemaLlavesWebAPI.Controllers;
using SistemaLlavesWebAPI.Interfaces;

namespace TestServices.Controllers;

public class TestClientesController
{
    private readonly Mock<IClientService> _clientServiceMock;
    private readonly ClientesController _controller;

    public TestClientesController()
    {
        _clientServiceMock = new Mock<IClientService>();
        _controller = new ClientesController(_clientServiceMock.Object);
    }

    [Fact]
    public async Task GetClientes_ShouldReturnAllClients()
    {
        // Arrange
        var clients = new List<Clientes>
        {
            new Clientes { ClienteId = 1, Nombre = "Cliente 1" },
            new Clientes { ClienteId = 2, Nombre = "Cliente 2" }
        };
        _clientServiceMock.Setup(s => s.GetAsync()).ReturnsAsync(clients);

        // Act
        var result = await _controller.GetClientes();

        // Assert
        Assert.NotNull(result.Value);
        Assert.Equal(2, result.Value.Count());
    }

    [Fact]
    public async Task GetClientes_ShouldReturnEmptyList_WhenNoClientsExist()
    {
        // Arrange
        _clientServiceMock.Setup(s => s.GetAsync()).ReturnsAsync(new List<Clientes>());

        // Act
        var result = await _controller.GetClientes();

        // Assert
        Assert.NotNull(result.Value);
        Assert.Empty(result.Value);
    }

    [Fact]
    public async Task GetClientesById_ShouldReturnClient_WhenExists()
    {
        // Arrange
        var client = new Clientes { ClienteId = 1, Nombre = "Cliente Existente" };
        _clientServiceMock.Setup(s => s.GetByIdAsync(1)).ReturnsAsync(client);

        // Act
        var result = await _controller.GetClientes(1);

        // Assert
        Assert.NotNull(result.Value);
        Assert.Equal(client.ClienteId, result.Value.ClienteId);
    }

    [Fact]
    public async Task GetClientesById_ShouldReturnNotFound_WhenClientDoesNotExist()
    {
        // Arrange
        _clientServiceMock.Setup(s => s.GetByIdAsync(It.IsAny<int>())).ThrowsAsync(new KeyNotFoundException());

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
        _clientServiceMock.Setup(s => s.AddAsync(It.IsAny<Clientes>())).ReturnsAsync(true);

        // Act
        var result = await _controller.PostClientes(client);

        // Assert
        Assert.IsType<CreatedAtActionResult>(result.Result);
        _clientServiceMock.Verify(s => s.AddAsync(It.IsAny<Clientes>()), Times.Once);
    }

    [Fact]
    public async Task PostClientes_ShouldReturnBadRequest_WhenAddAsyncFails()
    {
        // Arrange
        var client = new Clientes { ClienteId = 1, Nombre = "Test Client" };
        _clientServiceMock.Setup(s => s.AddAsync(It.IsAny<Clientes>())).ReturnsAsync(false);

        // Act
        var result = await _controller.PostClientes(client);

        // Assert
        Assert.IsType<BadRequestResult>(result.Result);
    }

    [Fact]
    public async Task PutClientes_ShouldUpdateClient()
    {
        // Arrange
        var client = new Clientes { ClienteId = 1, Nombre = "Cliente Actualizado" };
        _clientServiceMock.Setup(s => s.PutAsync(It.IsAny<Clientes>())).ReturnsAsync(client);

        // Act
        var result = await _controller.PutClientes(1, client);

        // Assert
        Assert.IsType<OkObjectResult>(result);
        _clientServiceMock.Verify(s => s.PutAsync(It.IsAny<Clientes>()), Times.Once);
    }

    [Fact]
    public async Task PutClientes_ShouldReturnNotFound_WhenClientDoesNotExist()
    {
        // Arrange
        var client = new Clientes { ClienteId = 99, Nombre = "Non-Existent Client" };
        _clientServiceMock.Setup(s => s.PutAsync(It.IsAny<Clientes>())).ThrowsAsync(new KeyNotFoundException());

        // Act
        var result = await _controller.PutClientes(99, client);

        // Assert
        Assert.IsType<NotFoundResult>(result);
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
    public async Task PutClientes_ShouldReturnBadRequest_WhenExceptionOccurs()
    {
        // Arrange
        _clientServiceMock.Setup(s => s.PutAsync(It.IsAny<Clientes>())).ThrowsAsync(new Exception("Test Exception"));
        var client = new Clientes { ClienteId = 1, Nombre = "Test Client" };

        // Act
        var result = await _controller.PutClientes(1, client);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal("Test Exception", badRequestResult.Value);
    }

    [Fact]
    public async Task DeleteClientes_ShouldRemoveClient()
    {
        // Arrange
        _clientServiceMock.Setup(s => s.DeleteAsync(1)).ReturnsAsync(new Clientes { ClienteId = 1 });

        // Act
        var result = await _controller.DeleteClientes(1);

        // Assert
        Assert.IsType<OkObjectResult>(result);
        _clientServiceMock.Verify(s => s.DeleteAsync(1), Times.Once);
    }

    [Fact]
    public async Task DeleteClientes_ShouldReturnNotFound_WhenClientDoesNotExist()
    {
        // Arrange
        _clientServiceMock.Setup(s => s.DeleteAsync(It.IsAny<int>())).ThrowsAsync(new KeyNotFoundException());

        // Act
        var result = await _controller.DeleteClientes(99);

        // Assert
        Assert.IsType<NotFoundObjectResult>(result);
    }
}