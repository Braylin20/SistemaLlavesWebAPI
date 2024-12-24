using Microsoft.EntityFrameworkCore;
using Shared.Models;
using SistemaLlavesWebAPI.Dal;
using SistemaLlavesWebAPI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestServices.Services
{
    public class TestMetodoPagosService
    {
        private readonly Context _context;
        private readonly MetodoPagosService _service;


        public TestMetodoPagosService()
        {
            var options = new DbContextOptionsBuilder<Context>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            _context = new Context(options);
            _service = new MetodoPagosService(_context);
        }
        [Fact]
        public async Task GetAsync_ShouldReturnAllMetodosDePago()
        {
            // Arrange
            _context.MetodosPagos.Add(new MetodosPagos { MetodoPagoId = 1, TipoMetodoPago = "Tarjeta" });
            _context.MetodosPagos.Add(new MetodosPagos { MetodoPagoId = 2, TipoMetodoPago = "Contado" });
            await _context.SaveChangesAsync();

            // Act
            var result = await _service.GetAsync();

            // Assert
            Assert.Equal(2, result.Count);
        }


        [Fact]
        public async Task AddAsync_ShouldAddMetodoPago()
        {
            // Arrange
            var metodoPago = new MetodosPagos { MetodoPagoId = 1, TipoMetodoPago = "Tarjeta" };

            // Act
            var result = await _service.AddAsync(metodoPago);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task PutAsync_ShouldUpdateProduct()
        {
            // Arrange
            var metodoPago = new MetodosPagos { MetodoPagoId = 1, TipoMetodoPago = "Tarjeta" };
            _context.MetodosPagos.Add(metodoPago);
            await _context.SaveChangesAsync();

            var updatedMetodoPago = new MetodosPagos { MetodoPagoId = 1, TipoMetodoPago = "Contado" };

            // Act
            var result = await _service.PutAsync(updatedMetodoPago);

            // Assert

            Assert.True(result);

            var metodoPagoEnBaseDeDatos = await _context.MetodosPagos.FindAsync(updatedMetodoPago.MetodoPagoId);
            Assert.NotNull(metodoPagoEnBaseDeDatos);
            Assert.Equal("Contado", metodoPagoEnBaseDeDatos.TipoMetodoPago);
        }
        [Fact]
        public async Task DeleteAsync_ShouldRemoveMetodoPago()
        {
            // Arrange
            var metodoPago = new MetodosPagos { MetodoPagoId = 1, TipoMetodoPago = "Tarjeta" };

            _context.MetodosPagos.Add(metodoPago);
            await _context.SaveChangesAsync();

            // Act
            var result = await _service.DeleteAsync(1);

            // Assert
            Assert.True(result);
            Assert.Equal(0, await _context.Productos.CountAsync());
        }
    }
}
