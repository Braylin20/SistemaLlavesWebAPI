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

namespace TestServices.Controller
{
    public class TestMetodosPagosController
    {
        private readonly Mock<IMetodoPagosService> _metodoPagosService;
        private readonly MetodosPagosController _controller;


        public TestMetodosPagosController()
        {
            _metodoPagosService = new Mock<IMetodoPagosService>();
            _controller = new MetodosPagosController(_metodoPagosService.Object);
        }

        [Fact]
        public async Task GetMetodoPago_ShouldReturnListOfMetodosPagos()
        {
            var metodoPagosList = new List<MetodosPagos> {
            new() { MetodoPagoId =1 , TipoMetodoPago= "Metodo 1"},
            new() { MetodoPagoId =2 , TipoMetodoPago = "Metodo 2"}
        };

            _metodoPagosService.Setup(s => s.GetAsync()).ReturnsAsync(metodoPagosList);

            var result = await _controller.GetMetodosPagos();

            var okResult = Assert.IsType<ActionResult<IEnumerable<MetodosPagos>>>(result);
            Assert.Equal(metodoPagosList, okResult.Value);
        }
        [Fact]
        public async Task GetMetodoPagoById_ShouldReturnNotFoundWhenIdDoesNotExist()
        {
            var metodoPago = new MetodosPagos() { MetodoPagoId = 1, TipoMetodoPago = "Metodo 1"};


            _metodoPagosService.Setup(s => s.GetById(metodoPago.MetodoPagoId)).ReturnsAsync(metodoPago);

            var result = await _controller.GetMetodosPagos(2);

            Assert.IsType<NotFoundResult>(result.Result);

        }

        [Fact]
        public async Task GetMetodoPago_ShouldReturnProductById()
        {

            var metodoPago = new MetodosPagos() { MetodoPagoId = 1, TipoMetodoPago = "Metodo 1" };

            _metodoPagosService.Setup(s => s.GetById(metodoPago.MetodoPagoId)).ReturnsAsync(metodoPago);

            var result = await _controller.GetMetodosPagos(metodoPago.MetodoPagoId);

            var okResult = Assert.IsType<ActionResult<MetodosPagos>>(result);
            Assert.Equal(metodoPago, okResult.Value);
        }

        [Fact]
        public async Task PostMetodoPago_ShouldCreateAnAction_WhenIsSuccessful()
        {
            // Arrange
            var metodoPago = new MetodosPagos { MetodoPagoId = 1, TipoMetodoPago ="Metodo 1" };
            _metodoPagosService.Setup(s => s.AddAsync(metodoPago)).ReturnsAsync(true);

            // Act
            var result = await _controller.PostMetodosPagos(metodoPago);

            // Assert
            var createdResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            Assert.Equal("GetMetodosPagos", createdResult.ActionName);
        }

        [Fact]
        public async Task PostMetodoPago_ReturnsBadRequest_WhenAddFails()
        {
            // Arrange
            var metodoPago = new MetodosPagos { MetodoPagoId = 1, TipoMetodoPago = "Metodo 1" };
            _metodoPagosService.Setup(s => s.AddAsync(metodoPago)).ReturnsAsync(false);

            // Act
            var result = await _controller.PostMetodosPagos(metodoPago);

            // Assert
            Assert.IsType<BadRequestResult>(result.Result);
        }

        [Fact]
        public async Task PutMetodoPago_ReturnsOk_WhenSuccessful()
        {
            // Arrange
            var metodoPago = new MetodosPagos {  MetodoPagoId = 1, TipoMetodoPago = "Met"};

            _metodoPagosService.Setup(s => s.PutAsync(metodoPago)).ReturnsAsync(true);

            // Act
            var result = await _controller.PutMetodosPagos(1, metodoPago);

            // Assert
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task PutMetodoPago_ReturnsBadRequest_WhenIdsDoNotMatch()
        {
            // Arrange
            var metodoPago = new MetodosPagos { MetodoPagoId = 1, TipoMetodoPago = "Met" };

            // Act
            var result = await _controller.PutMetodosPagos(2, metodoPago);

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task DeleteMetodoPago_ReturnsOk_WhenSuccessful()
        {
            // Arrange
            var metodoPago = new MetodosPagos { MetodoPagoId = 1, TipoMetodoPago = "Met" };
            _metodoPagosService.Setup(s => s.DeleteAsync(1)).ReturnsAsync(metodoPago);

            // Act
            var result = await _controller.DeleteMetodosPagos(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(metodoPago, okResult.Value);
        }

        [Fact]
        public async Task DeleteMetodoPago_ReturnsNotFound_WhenCompraDoesNotExist()
        {
            // Arrange
            _metodoPagosService?.Setup(s => s.DeleteAsync(1)).ReturnsAsync((MetodosPagos?)null);

            // Act
            var result = await _controller.DeleteMetodosPagos(1);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
    }
}
