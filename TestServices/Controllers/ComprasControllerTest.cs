using Microsoft.AspNetCore.Mvc;
using Moq;
using Shared.Models;
using SistemaLlavesWebAPI.Abstractions;
using SistemaLlavesWebAPI.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestServices.Controller
{
    public class ComprasControllerTests
    {
        private readonly Mock<IPuchaseService> _mockPurchaseService;
        private readonly ComprasController _controller;

        public ComprasControllerTests()
        {
            _mockPurchaseService = new Mock<IPuchaseService>();
            _controller = new ComprasController(_mockPurchaseService.Object);
        }

        [Fact]
        public async Task GetCompras_ReturnsListOfCompras()
        {
            // Arrange
            var comprasList = new List<Compras>
        {
            new Compras { CompraId = 1, Fecha =DateTime.Now },
            new Compras { CompraId = 2, Fecha = DateTime.Now }
        };

            _mockPurchaseService.Setup(s => s.GetAllAsync()).ReturnsAsync(comprasList);

            // Act
            var result = await _controller.GetCompras();

            // Assert
            var okResult = Assert.IsType<ActionResult<IEnumerable<Compras>>>(result);
            Assert.Equal(comprasList, okResult.Value);
        }

        [Fact]
        public async Task GetCompras_ById_ReturnsCompra()
        {
            // Arrange
            var compra = new Compras { CompraId = 1, Fecha = DateTime.Now };
            _mockPurchaseService.Setup(s => s.GetById(1)).ReturnsAsync(compra);

            // Act
            var result = await _controller.GetCompras(1);

            // Assert
            var okResult = Assert.IsType<ActionResult<Compras>>(result);
            Assert.Equal(compra, okResult.Value);
        }

        [Fact]
        public async Task GetCompras_ById_ReturnsNotFound_WhenCompraDoesNotExist()
        {
            // Arrange
            _mockPurchaseService.Setup(s => s.GetById(1)).ReturnsAsync((Compras?)null);

            // Act
            var result = await _controller.GetCompras(1);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task PostCompras_ReturnsCreatedAtAction_WhenSuccessful()
        {
            // Arrange
            var compra = new Compras { CompraId = 1, Fecha = DateTime.Now };
            _mockPurchaseService.Setup(s => s.AddAsync(compra)).ReturnsAsync(true);

            // Act
            var result = await _controller.PostCompras(compra);

            // Assert
            var createdResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            Assert.Equal("GetCompras", createdResult.ActionName);
        }

        [Fact]
        public async Task PostCompras_ReturnsBadRequest_WhenAddFails()
        {
            // Arrange
            var compra = new Compras { CompraId = 1, Fecha = DateTime.Now };
            _mockPurchaseService.Setup(s => s.AddAsync(compra)).ReturnsAsync(false);

            // Act
            var result = await _controller.PostCompras(compra);

            // Assert
            Assert.IsType<BadRequestResult>(result.Result);
        }

        [Fact]
        public async Task PutCompras_ReturnsOk_WhenSuccessful()
        {
            // Arrange
            var compra = new Compras { CompraId = 1, Fecha = DateTime.Now };
            _mockPurchaseService.Setup(s => s.PutAsync(compra)).ReturnsAsync(compra);

            // Act
            var result = await _controller.PutCompras(1, compra);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(compra, okResult.Value);
        }

        [Fact]
        public async Task PutCompras_ReturnsBadRequest_WhenIdsDoNotMatch()
        {
            // Arrange
            var compra = new Compras { CompraId = 1, Fecha = DateTime.Now };

            // Act
            var result = await _controller.PutCompras(2, compra);

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task DeleteCompras_ReturnsOk_WhenSuccessful()
        {
            // Arrange
            var compra = new Compras { CompraId = 1, Fecha = DateTime.Now };
            _mockPurchaseService.Setup(s => s.DeleteAsync(1)).ReturnsAsync(compra);

            // Act
            var result = await _controller.DeleteCompras(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(compra, okResult.Value);
        }

        [Fact]
        public async Task DeleteCompras_ReturnsNotFound_WhenCompraDoesNotExist()
        {
            // Arrange
            _mockPurchaseService.Setup(s => s.DeleteAsync(1)).ReturnsAsync((Compras?)null);

            // Act
            var result = await _controller.DeleteCompras(1);

            // Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }
    }

}
