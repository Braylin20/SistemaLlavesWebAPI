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
    [TestClass]
    public class TestWarrantyService
    {
        private Context _context;
        private WarrantyService _service;

        [TestInitialize]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<Context>()
                .UseInMemoryDatabase("TestWarrantyDatabase")
                .Options;

            _context = new Context(options);
            _service = new WarrantyService(_context);
        }

        [TestCleanup]
        public void Cleanup()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [TestMethod]
        public async Task GetAsync_ShouldReturnAllWarranties()
        {
            // Arrange
            _context.Garantias.Add(new Garantias { GarantiaId = 1, Descripcion = "Garantía 1" });
            _context.Garantias.Add(new Garantias { GarantiaId = 2, Descripcion = "Garantía 2" });
            await _context.SaveChangesAsync();

            // Act
            var result = await _service.GetAsync();

            // Assert
            Assert.AreEqual(2, result.Count);
        }

        [TestMethod]
        public async Task AddAsync_ShouldAddWarranty()
        {
            // Arrange
            var warranty = new Garantias { GarantiaId = 1, Descripcion = "Garantía Nueva" };

            // Act
            var result = await _service.AddAsync(warranty);

            // Assert
            Assert.IsTrue(result);
            Assert.AreEqual(1, await _context.Garantias.CountAsync());
        }

        [TestMethod]
        public async Task DeleteAsync_ShouldRemoveWarranty()
        {
            // Arrange
            var warranty = new Garantias { GarantiaId = 1, Descripcion = "Garantía para eliminar" };
            _context.Garantias.Add(warranty);
            await _context.SaveChangesAsync();

            // Act
            var result = await _service.DeleteAsync(1);

            // Assert
            Assert.AreEqual(0, await _context.Garantias.CountAsync());
            Assert.AreEqual(warranty, result);
        }

        [TestMethod]
        public async Task PutAsync_ShouldUpdateWarranty()
        {
            // Arrange
            var warranty = new Garantias { GarantiaId = 1, Descripcion = "Garantía Original" };
            _context.Garantias.Add(warranty);
            await _context.SaveChangesAsync();

            var updatedWarranty = new Garantias { GarantiaId = 1, Descripcion = "Garantía Actualizada" };

            // Act
            var result = await _service.PutAsync(updatedWarranty);

            // Assert
            var dbWarranty = await _context.Garantias.FindAsync(1);
            Assert.IsNotNull(dbWarranty);
            Assert.AreEqual(updatedWarranty.Descripcion, dbWarranty.Descripcion);
        }


        [TestMethod]
        public async Task DeleteAsync_ShouldThrowKeyNotFoundException_WhenWarrantyNotFound()
        {
            // Arrange
            int invalidId = 999; // ID que no existe

            // Act & Assert
            await Assert.ThrowsExceptionAsync<KeyNotFoundException>(async () =>
            {
                await _service.DeleteAsync(invalidId);
            });
        }
    }
}
