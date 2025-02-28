﻿using Microsoft.EntityFrameworkCore;
using SistemaLlavesWebAPI.Services;
using SistemaLlavesWebAPI.Dal;
using Shared.Models;

namespace TestServices.Services
{
    public class PurchaseServiceTests : IDisposable
    {
        private readonly Context _context;
        private readonly PuchaseService _service;

        public PurchaseServiceTests()
        {
            // Configuración inicial
            var options = new DbContextOptionsBuilder<Context>()
                .UseInMemoryDatabase($"TestWarrantyDatabase_{Guid.NewGuid()}")
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
            var purchase = new Compras { CompraId = 5, Fecha = DateTime.Now };

            // Act
            var result = await _service.AddAsync(purchase);

            // Assert
            Assert.True(result); // Verifica que el método regresa true
            var addedPurchase = await _context.Compras.FindAsync(5); // Busca la entidad agregada
            Assert.NotNull(addedPurchase); // Verifica que se agregó correctamente
            Assert.Equal(1, await _context.Compras.CountAsync()); // Verifica el conteo
        }

        [Fact]
        public async Task GetAsync_ShouldReturnAllPurchases()
        {
            // Arrange
            _context.Compras.Add(new Compras { CompraId = 1, Fecha = DateTime.Now });
            _context.Compras.Add(new Compras { CompraId = 2, Fecha = DateTime.Now });
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
            var purchase = new Compras { CompraId = 1, Fecha = DateTime.Now };
            _context.Compras.Add(purchase);
            await _context.SaveChangesAsync();

            // Act
            var result = await _service.DeleteAsync(1);

            // Assert
            Assert.Equal(0, await _context.Compras.CountAsync());
            Assert.Equal(purchase, result);
        }

        [Fact]
public async Task DeleteAsync_ShouldReturnNull_WhenPurchaseNotFound()
{
    // Act
    var result = await _service.DeleteAsync(999); // ID inexistente

    // Assert
    Assert.Null(result); // Verifica que devuelve null
}

        [Fact]
        public async Task PutAsync_ShouldUpdatePurchase()
        {
            // Arrange
            var purchase = new Compras { CompraId = 1, Fecha = DateTime.Now };
            _context.Compras.Add(purchase);
            await _context.SaveChangesAsync();

            var updatedPurchase = new Compras { CompraId = 1, Fecha = DateTime.Now };

            // Act
            var result = await _service.PutAsync(updatedPurchase);

            // Assert
            var dbPurchase = await _context.Compras.FindAsync(1);
            Assert.NotNull(dbPurchase);
            Assert.Equal(updatedPurchase.Fecha, dbPurchase.Fecha);
        }

        [Fact]
        public async Task PutAsync_ShouldReturnNull_WhenPurchaseNotFound()
        {
            // Arrange
            var updatedPurchase = new Compras { CompraId = 999, Fecha = DateTime.Now }; // ID inexistente

            // Act
            var result = await _service.PutAsync(updatedPurchase);

            // Assert
            Assert.Null(result); // Verifica que devuelve null
        }

        [Fact]
        public async Task GetCompra_ById_ReturnsGarantia_WhenIdIsValid()
        {
            // Arrange
            var purchase = new Compras { CompraId = 1, Fecha = DateTime.Now };
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
