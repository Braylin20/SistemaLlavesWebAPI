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

namespace TestServices.Controllers
{
    public class SalesControllerTest
    {
        private readonly Mock<ISalesService> _mockSalesService;
        private readonly VentasController _controller;
        public SalesControllerTest()
        {
            _mockSalesService = new Mock<ISalesService>();
            _controller = new VentasController(_mockSalesService.Object);
        }

        [Fact]
        public async Task GetVentas_Should_Return_List_Ventas()
        {
            // Arrange
            var ventas = new List<Ventas>
        {
            new Ventas { VentaId = 1, MetodoPagoId = 1, ClienteId = 1, Cantidad = 2, Fecha = new DateOnly(2024, 1, 1), Descripcion = "Venta 1", VentaDevuelta = false },
            new Ventas { VentaId = 2, MetodoPagoId = 2, ClienteId = 2, Cantidad = 3, Fecha = new DateOnly(2024, 1, 2), Descripcion = "Venta 2", VentaDevuelta = true }
        };
            _mockSalesService.Setup(s => s.GetAsync()).ReturnsAsync(ventas);

            // Act
            var result = await _controller.GetVentas();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal(ventas, okResult.Value);
        }

        [Fact]
        public async Task GetVentas_Should_Return_NotFound()
        {
            // Arrange
            _mockSalesService.Setup(s => s.GetAsync()).ReturnsAsync(new List<Ventas>());

            // Act
            var result = await _controller.GetVentas();

            // Assert
            Assert.IsType<NotFoundObjectResult>(result.Result);
        }

        [Fact]
        public async Task GetVentasById_Should_Return_Venta()
        {
            // Arrange
            var venta = new Ventas { VentaId = 1, MetodoPagoId = 1, ClienteId = 1, Cantidad = 2, Fecha = new DateOnly(2024, 1, 1), Descripcion = "Venta 1", VentaDevuelta = false };
            _mockSalesService.Setup(s => s.GetVentaById(1)).ReturnsAsync(venta);

            // Act
            var result = await _controller.GetVentasById(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal(venta, okResult.Value);
        }

        [Fact]
        public async Task GetVentasById_Should_Return_BadRequest()
        {
            // Act
            var result = await _controller.GetVentasById(0);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result.Result);
        }

        [Fact]
        public async Task GetVentasById_Should_Return_NotFound()
        {
            // Arrange
            _mockSalesService.Setup(s => s.GetVentaById(1)).ReturnsAsync((Ventas?)null);

            // Act
            var result = await _controller.GetVentasById(1);

            // Assert
            Assert.IsType<NotFoundObjectResult>(result.Result);
        }

        [Fact]
        public async Task PostVentas_Should_Return_CreatedVenta()
        {
            // Arrange
            var venta = new Ventas { VentaId = 1, MetodoPagoId = 1, ClienteId = 1, Cantidad = 2, Fecha = new DateOnly(2024, 1, 1), Descripcion = "Venta 1", VentaDevuelta = false };
            _mockSalesService.Setup(s => s.AddAsync(venta)).ReturnsAsync(venta);

            // Act
            var result = await _controller.PostVentas(venta);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            Assert.Equal("GetVentasById", createdAtActionResult.ActionName);
            Assert.Equal(venta, createdAtActionResult.Value);
        }

        [Fact]
        public async Task PostVentas_Should_Return_ErrorServer()
        {
            // Arrange
            var venta = new Ventas { VentaId = 1, MetodoPagoId = 1, ClienteId = 1, Cantidad = 2, Fecha = new DateOnly(2024, 1, 1), Descripcion = "Venta 1", VentaDevuelta = false };
            _mockSalesService.Setup(s => s.AddAsync(venta)).ThrowsAsync(new System.Exception("Error"));

            // Act
            var result = await _controller.PostVentas(venta);

            // Assert
            var objectResult = Assert.IsType<ObjectResult>(result.Result);
            Assert.Equal(500, objectResult.StatusCode);
        }


        [Fact]
        public async Task PutVentas_Should_Return_BadRequest()
        {
            // Arrange
            var venta = new Ventas { VentaId = 1, MetodoPagoId = 1, ClienteId = 1, Cantidad = 2, Fecha = new DateOnly(2024, 1, 1), Descripcion = "Venta 1", VentaDevuelta = false };

            // Act
            var result = await _controller.PutVentas(2, venta);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task PutVentas_Should_Return_NotFound()
        {
            // Arrange
            var venta = new Ventas { VentaId = 1, MetodoPagoId = 1, ClienteId = 1, Cantidad = 2, Fecha = new DateOnly(2024, 1, 1), Descripcion = "Venta 1", VentaDevuelta = false };
            _mockSalesService.Setup(s => s.PutAsync(venta)).ThrowsAsync(new KeyNotFoundException());

            // Act
            var result = await _controller.PutVentas(1, venta);

            // Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public async Task DeleteVentas_Should_Return_Ok()
        {
            // Arrange
            _mockSalesService.Setup(s => s.DeleteAsync(1)).ReturnsAsync(true);

            // Act
            var result = await _controller.DeleteVentas(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.True((bool?)okResult.Value);
        }
    }
}
