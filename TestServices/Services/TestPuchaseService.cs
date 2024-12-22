using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SistemaLlavesWebAPI.Services;
using Xunit;
using SistemaLlavesWebAPI.Dal;
using Shared.Models;
using SistemaLlavesWebAPI.Interfaces;

namespace TestServices.Services
{
    public class PurchaseServiceTests : IDisposable
    {
        private readonly Context _context;
        private readonly IPuchaseService _service;

        public PurchaseServiceTests()
        {
            // Configuración inicial
            var options = new DbContextOptionsBuilder<Context>()
                .UseInMemoryDatabase("TestDatabase")
                .Options;

            _context = new Context(options);
            _service = new PuchaseService(_context);
        }

        public void Dispose()
        {
            // Limpieza al final de cada prueba
            _context.Database.EnsureDeleted();
            _context.Dispose();
            GC.SuppressFinalize(this);
        }

        [Fact]
        public async Task AddAsync_ShouldAddPurchase()
        {
            // Arrange
            var purchase = new Compras { CompraId = 1, Fecha = DateOnly.FromDateTime(DateTime.Now) };

            // Act
            var result = await _service.AddAsync(purchase);

            // Assert
            Assert.True(result);
            Assert.Equal(1, await _context.Compras.CountAsync());
        }

        [Fact]
        public async Task GetAsync_ShouldReturnAllPurchases()
        {
            // Arrange
            _context.Compras.Add(new Compras { CompraId = 1, Fecha = DateOnly.FromDateTime(DateTime.Now) });
            _context.Compras.Add(new Compras { CompraId = 2, Fecha = DateOnly.FromDateTime(DateTime.Now) });
            await _context.SaveChangesAsync();

            // Act
            var result = await _service.GetAllAsync();

            // Assert
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async Task DeleteAsync_ShouldRemovePurchase()
        {
            // Arrange
            var purchase = new Compras { CompraId = 1, Fecha = DateOnly.FromDateTime(DateTime.Now) };
            _context.Compras.Add(purchase);
            await _context.SaveChangesAsync();

            // Act
            var result = await _service.DeleteAsync(1);

            // Assert
            Assert.Equal(0, await _context.Compras.CountAsync());
            Assert.Equal(purchase, result);
        }

        [Fact]
        public async Task PutAsync_ShouldUpdatePurchase()
        {
            // Arrange
            var purchase = new Compras { CompraId = 1, Fecha = DateOnly.FromDateTime(DateTime.Now) };
            _context.Compras.Add(purchase);
            await _context.SaveChangesAsync();

            var updatedPurchase = new Compras { CompraId = 1, Fecha = DateOnly.FromDateTime(DateTime.Now.AddDays(1)) };

            // Act
            var result = await _service.PutAsync(updatedPurchase);

            // Assert
            var dbPurchase = await _context.Compras.FindAsync(1);
            Assert.NotNull(dbPurchase);
            Assert.Equal(updatedPurchase.Fecha, dbPurchase.Fecha);
        }

        [Fact]
        public async Task GetCompra_ById_ReturnsGarantia_WhenIdIsValid()
        {
            // Arrange
            var purchase = new Compras { CompraId = 1, Fecha = DateOnly.FromDateTime(DateTime.Now) };
            _context.Compras.Add(purchase);
            await _context.SaveChangesAsync();

            // Act
            var result = await _service.GetById(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(purchase.CompraId, result.CompraId);
         
        }

    }
}
