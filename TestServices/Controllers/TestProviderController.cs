using Microsoft.AspNetCore.Mvc;
using Moq;
using Shared.Models;
using SistemaLlavesWebAPI.Controllers;
using SistemaLlavesWebAPI.Interfaces;

namespace TestServices.Controllers;

public class TestProveedoresController
{
    private readonly Mock<IProviderService> _providerServiceMock;
    private readonly ProveedoresController _controller;

    public TestProveedoresController()
    {
        _providerServiceMock = new Mock<IProviderService>();
        _controller = new ProveedoresController(_providerServiceMock.Object);
    }

    [Fact]
    public async Task GetProveedores_ShouldReturnAllProviders()
    {
        // Arrange
        var providers = new List<Proveedores>
        {
            new Proveedores { ProovedorId = 1, Nombre = "Proveedor 1" },
            new Proveedores { ProovedorId = 2, Nombre = "Proveedor 2" }
        };
        _providerServiceMock.Setup(s => s.GetAsync()).ReturnsAsync(providers);

        // Act
        var result = await _controller.GetProveedores();

        // Assert
        Assert.NotNull(result.Value);
        Assert.Equal(2, result.Value.Count());
    }

    [Fact]
    public async Task GetProveedores_ShouldReturnEmptyList_WhenNoProvidersExist()
    {
        // Arrange
        _providerServiceMock.Setup(s => s.GetAsync()).ReturnsAsync(new List<Proveedores>());

        // Act
        var result = await _controller.GetProveedores();

        // Assert
        Assert.NotNull(result.Value);
        Assert.Empty(result.Value);
    }

    [Fact]
    public async Task GetProveedoresById_ShouldReturnProvider_WhenExists()
    {
        // Arrange
        var provider = new Proveedores { ProovedorId = 1, Nombre = "Proveedor Existente" };
        _providerServiceMock.Setup(s => s.GetByIdAsync(1)).ReturnsAsync(provider);

        // Act
        var result = await _controller.GetProveedores(1);

        // Assert
        Assert.NotNull(result.Value);
        Assert.Equal(provider.ProovedorId, result.Value.ProovedorId);
    }

    [Fact]
    public async Task GetProveedoresById_ShouldReturnNotFound_WhenProviderDoesNotExist()
    {
        // Arrange
        _providerServiceMock.Setup(s => s.GetByIdAsync(It.IsAny<int>())).ThrowsAsync(new KeyNotFoundException());

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
        _providerServiceMock.Setup(s => s.AddAsync(It.IsAny<Proveedores>())).ReturnsAsync(true);

        // Act
        var result = await _controller.PostProveedores(provider);

        // Assert
        Assert.IsType<CreatedAtActionResult>(result.Result);
        _providerServiceMock.Verify(s => s.AddAsync(It.IsAny<Proveedores>()), Times.Once);
    }

    [Fact]
    public async Task PostProveedores_ShouldReturnBadRequest_WhenAddAsyncFails()
    {
        // Arrange
        var provider = new Proveedores { ProovedorId = 1, Nombre = "Test Provider" };
        _providerServiceMock.Setup(s => s.AddAsync(It.IsAny<Proveedores>())).ReturnsAsync(false);

        // Act
        var result = await _controller.PostProveedores(provider);

        // Assert
        Assert.IsType<BadRequestResult>(result.Result);
    }

    [Fact]
    public async Task PutProveedores_ShouldUpdateProvider()
    {
        // Arrange
        var provider = new Proveedores { ProovedorId = 1, Nombre = "Proveedor Actualizado" };
        _providerServiceMock.Setup(s => s.PutAsync(It.IsAny<Proveedores>())).ReturnsAsync(provider);

        // Act
        var result = await _controller.PutProveedores(1, provider);

        // Assert
        Assert.IsType<OkObjectResult>(result);
        _providerServiceMock.Verify(s => s.PutAsync(It.IsAny<Proveedores>()), Times.Once);
    }

    [Fact]
    public async Task PutProveedores_ShouldReturnNotFound_WhenProviderDoesNotExist()
    {
        // Arrange
        var provider = new Proveedores { ProovedorId = 99, Nombre = "Non-Existent Provider" };
        _providerServiceMock.Setup(s => s.PutAsync(It.IsAny<Proveedores>())).ThrowsAsync(new KeyNotFoundException());

        // Act
        var result = await _controller.PutProveedores(99, provider);

        // Assert
        Assert.IsType<NotFoundResult>(result);
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
    public async Task PutProveedores_ShouldReturnBadRequest_WhenExceptionOccurs()
    {
        // Arrange
        _providerServiceMock.Setup(s => s.PutAsync(It.IsAny<Proveedores>())).ThrowsAsync(new Exception("Test Exception"));
        var provider = new Proveedores { ProovedorId = 1, Nombre = "Test Provider" };

        // Act
        var result = await _controller.PutProveedores(1, provider);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal("Test Exception", badRequestResult.Value);
    }

    [Fact]
    public async Task DeleteProveedores_ShouldRemoveProvider()
    {
        // Arrange
        _providerServiceMock.Setup(s => s.DeleteAsync(1)).ReturnsAsync(new Proveedores { ProovedorId = 1 });

        // Act
        var result = await _controller.DeleteProveedores(1);

        // Assert
        Assert.IsType<OkObjectResult>(result);
        _providerServiceMock.Verify(s => s.DeleteAsync(1), Times.Once);
    }

    [Fact]
    public async Task DeleteProveedores_ShouldReturnNotFound_WhenProviderDoesNotExist()
    {
        // Arrange
        _providerServiceMock.Setup(s => s.DeleteAsync(It.IsAny<int>())).ThrowsAsync(new KeyNotFoundException());

        // Act
        var result = await _controller.DeleteProveedores(99);

        // Assert
        Assert.IsType<NotFoundObjectResult>(result);
    }
}