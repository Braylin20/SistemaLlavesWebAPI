using Microsoft.AspNetCore.Mvc;
using Moq;
using Shared.Models;
using SistemaLlavesWebAPI.Controllers;
using SistemaLlavesWebAPI.Interfaces;
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
}

