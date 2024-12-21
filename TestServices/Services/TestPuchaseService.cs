using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Moq;
using SistemaLlavesWebAPI.Services;
using Xunit;
using SistemaLlavesWebAPI.Dal;
using Shared.Models;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;



namespace TestServices.Services
{
    [TestClass]
    public class PurchaseServiceTests
    {
        private Context _context;
        private PurchaseService _service;

        [TestInitialize]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<Context>()
                .UseInMemoryDatabase("TestDatabase")
                .Options;

            _context = new Context(options);
            _service = new PurchaseService(_context);
        }

        [TestCleanup]
        public void Cleanup()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [TestMethod]
        public async Task AddAsync_ShouldAddPurchase()
        {
            // Arrange
            var purchase = new Compras { CompraId = 1, Fecha = DateOnly.FromDateTime(DateTime.Now) };

            // Act
            var result = await _service.AddAsync(purchase);

            // Assert
            Assert.IsTrue(result);
            Assert.AreEqual(1, await _context.Compras.CountAsync());
        }

        [TestMethod]
        public async Task GetAsync_ShouldReturnAllPurchases()
        {
            // Arrange
            _context.Compras.Add(new Compras { CompraId = 1, Fecha = DateOnly.FromDateTime(DateTime.Now) });
            _context.Compras.Add(new Compras { CompraId = 2, Fecha = DateOnly.FromDateTime(DateTime.Now) });
            await _context.SaveChangesAsync();

            // Act
            var result = await _service.GetAsync();

            // Assert
            Assert.AreEqual(2, result.Count);
        }

        [TestMethod]
        public async Task DeleteAsync_ShouldRemovePurchase()
        {
            // Arrange
            var purchase = new Compras { CompraId = 1, Fecha = DateOnly.FromDateTime(DateTime.Now) };
            _context.Compras.Add(purchase);
            await _context.SaveChangesAsync();

            // Act
            var result = await _service.DeleteAsync(1);

            // Assert
            Assert.AreEqual(0, await _context.Compras.CountAsync());
            Assert.AreEqual(purchase, result);
        }

        [TestMethod]

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
            Assert.IsNotNull(dbPurchase);
            Assert.AreEqual(updatedPurchase.Fecha, dbPurchase.Fecha);
        }
    }
}
