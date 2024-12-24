using Shared.Models;
using SistemaLlavesWebAPI.Dal;
using SistemaLlavesWebAPI.Services;
using Microsoft.EntityFrameworkCore;
using Xunit;
using System.Threading.Tasks;
using System.Linq;

namespace TestServices.Services
{
    public class CategoryServicesTests
    {
        private DbContextOptions<Context> GetInMemoryDbOptions()
        {
            return new DbContextOptionsBuilder<Context>()
                .UseInMemoryDatabase(databaseName: "TestCategoryDatabase")
                .Options;
        }


        [Fact]
        public async Task DeleteAsync_Should_Remove_Category()
        {
            // Arrange
            var options = GetInMemoryDbOptions();
            using var context = new Context(options);
            await context.Database.EnsureDeletedAsync(); 
            await context.Database.EnsureCreatedAsync(); 
            var category = new Categorias { CategoriaId = 1, Nombre = "Categoria 1" };
            context.Categorias.Add(category);
            await context.SaveChangesAsync();

            var service = new CategoryServices(context);

            // Act
            var result = await service.DeleteAsync(1);

            // Assert
            Assert.True(result);
            Assert.Empty(await context.Categorias.ToListAsync());
        }



        [Fact]
        public async Task PutAsync_Should_Update_Category()
        {
            // Arrange
            var options = GetInMemoryDbOptions();
            using var context = new Context(options);
            var category = new Categorias { CategoriaId = 1, Nombre = "Categoria 1" };
            context.Categorias.Add(category);
            await context.SaveChangesAsync();

            var service = new CategoryServices(context);
            var updatedCategory = new Categorias { CategoriaId = 1, Nombre = "Categoria Actualizada" };

            // Act
            var result = await service.PutAsync(updatedCategory);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Categoria Actualizada", result.Nombre);
        }

        [Fact]
        public async Task AddAsync_Should_Throw_Exception_If_Category_Is_Null()
        {
            // Arrange
            var options = GetInMemoryDbOptions();
            using var context = new Context(options);
            var service = new CategoryServices(context);

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => service.AddAsync(null));
        }
    }
}