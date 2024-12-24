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
            // Configuración de la base de datos en memoria
            var options = new DbContextOptionsBuilder<Context>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            _context = new Context(options);
            _service = new MetodoPagosService(_context);
        }

        [Fact]
        public async Task GetAsync_ShouldReturnAllMetodosPagos()
        {
            // Arrange
            _context.MetodosPagos.Add(new MetodosPagos { MetodoPagoId = 1, TipoMetodoPago = "Metodo 1" });
            _context.MetodosPagos.Add(new MetodosPagos { MetodoPagoId = 2, TipoMetodoPago = "Metodo 2" });
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
            var metodoPago = new MetodosPagos { MetodoPagoId = 1, TipoMetodoPago = "Metodo nuevo" };

            // Act
            var result = await _service.AddAsync(metodoPago);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task PutAsync_ShouldUpdateMetodoPago()
        {
            // Arrange
            var metodoPago = new MetodosPagos { MetodoPagoId = 1, TipoMetodoPago = "Metodo Original" };
            _context.MetodosPagos.Add(metodoPago);
            await _context.SaveChangesAsync();

            var updatedProduct = new MetodosPagos { MetodoPagoId = 1, TipoMetodoPago = "Metodo Actualizado" };

            // Act
            var result = await _service.PutAsync(updatedProduct);

            // Assert

            Assert.True(result);

            var productoEnBaseDeDatos = await _context.MetodosPagos.FindAsync(1);
            Assert.NotNull(productoEnBaseDeDatos);
            Assert.Equal("Metodo Actualizado", productoEnBaseDeDatos.TipoMetodoPago);
        }

        [Fact]
        public async Task DeleteAsync_ShouldMetodoPago()
        {
            // Arrange
            var metodoPago = new MetodosPagos { MetodoPagoId = 1, TipoMetodoPago = "Metodo a Eliminar" };
            _context.MetodosPagos.Add(metodoPago);
            await _context.SaveChangesAsync();

            // Act
            var result = await _service.DeleteAsync(1);

            // Assert
            Assert.Equal(metodoPago.TipoMetodoPago, result.TipoMetodoPago);
            Assert.Equal(0, await _context.Productos.CountAsync());
        }
    }
}
