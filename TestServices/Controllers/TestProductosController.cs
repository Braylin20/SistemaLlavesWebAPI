using Microsoft.AspNetCore.Mvc;
using Moq;
using Shared.Models;
using SistemaLlavesWebAPI.Controllers;
using SistemaLlavesWebAPI.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestServices.Controller;

public class TestProductosController
{
    private readonly Mock<IProductService> _mockProductService;
    private readonly ProductosController _controller;


    public TestProductosController()
    {
        _mockProductService = new Mock<IProductService>();
        _controller = new ProductosController(_mockProductService.Object);
    }

    [Fact]
    public async Task GetProductos_ShouldReturnListOfProducts()
    {
        var productList = new List<Productos> {
            new() { ProductoId =1 , Cantidad= 10},
            new() { ProductoId =2 , Cantidad= 10}
        };

        _mockProductService.Setup(s => s.GetAsync()).ReturnsAsync(productList);

        var result = await _controller.GetProductos();

        var okResult = Assert.IsType<ActionResult<IEnumerable<Productos>>>(result);
        Assert.Equal(productList, okResult.Value);
    }

    [Fact]
    public async Task GetProductos_ShouldReturnProductById()
    {
        var product = new Productos() { ProductoId = 1, Cantidad = 10 };


        _mockProductService.Setup(s => s.GetById(product.ProductoId)).ReturnsAsync(product);

        var result = await _controller.GetProductos(product.ProductoId);

        var okResult = Assert.IsType<ActionResult<Productos>>(result);
        Assert.Equal(product, okResult.Value);
    }

    [Fact]
    public async Task PostProductos_ShouldCreateAnAction_WhenIsSuccessful()
    {
        // Arrange
        var producto = new Productos { ProductoId = 1, Precio= 2.2 };
        _mockProductService.Setup(s => s.AddAsync(producto)).ReturnsAsync(true);

        // Act
        var result = await _controller.PostProductos(producto);

        // Assert
        var createdResult = Assert.IsType<CreatedAtActionResult>(result.Result);
        Assert.Equal("GetProductos", createdResult.ActionName);
    }

    [Fact]
    public async Task PostProductos_ReturnsBadRequest_WhenAddFails()
    {
        // Arrange
        var producto = new Productos { ProductoId = 1, Precio = 20.2 };
        _mockProductService.Setup(s => s.AddAsync(producto)).ReturnsAsync(false);

        // Act
        var result = await _controller.PostProductos(producto);

        // Assert
        Assert.IsType<BadRequestResult>(result.Result);
    }

    [Fact]
    public async Task PutProductos_ReturnsOk_WhenSuccessful()
    {
        // Arrange
        var producto = new Productos { ProductoId = 1, Precio = 20.2 };

        _mockProductService.Setup(s => s.PutAsync(producto)).ReturnsAsync(true);

        // Act
        var result = await _controller.PutProductos(1, producto);

        // Assert
        Assert.IsType<OkResult>(result);
    }

    [Fact]
    public async Task PutCompras_ReturnsBadRequest_WhenIdsDoNotMatch()
    {
        // Arrange
        var producto = new Productos { ProductoId = 1, Precio = 20.2 };

        // Act
        var result = await _controller.PutProductos(2, producto);

        // Assert
        Assert.IsType<BadRequestResult>(result);
    }

    [Fact]
    public async Task DeleteCompras_ReturnsOk_WhenSuccessful()
    {
        // Arrange
        var producto = new Productos {ProductoId = 1, Precio = 20.2 };
        _mockProductService.Setup(s => s.DeleteAsync(1)).ReturnsAsync(producto);

        // Act
        var result = await _controller.DeleteProductos(1);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(producto, okResult.Value);
    }

    [Fact]
    public async Task DeleteCompras_ReturnsNotFound_WhenCompraDoesNotExist()
    {
        // Arrange
        _mockProductService?.Setup(s => s.DeleteAsync(1)).ReturnsAsync((Productos?)null);

        // Act
        var result = await _controller.DeleteProductos(1);

        // Assert
        Assert.IsType<NotFoundObjectResult>(result);
    }
}

