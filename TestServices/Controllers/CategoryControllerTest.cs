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
    public class CategoryControllerTest
    {
        private readonly Mock<ICategoryService> _mockCategoryService;
        private readonly CategoriasController _controller;
        public CategoryControllerTest()
        {
            _mockCategoryService = new Mock<ICategoryService>();
            _controller = new CategoriasController(_mockCategoryService.Object);
        }

        [Fact]
        public async Task GetCategorias_Should_Return_List_Categories()
        {
            //arrange
            var categories = new List<Categorias>
            {
                new Categorias { Nombre = "Eliezer", CategoriaId = 1 },
                new Categorias { Nombre = "Blah blah", CategoriaId = 2 }
            };

            _mockCategoryService.Setup(c => c.GetAsync()).ReturnsAsync(categories);

            //act
            var result = await _controller.GetCategorias();

            //assert
            var okResult = Assert.IsType<ActionResult<List<Categorias>>>(result);
            Assert.Equal(categories, okResult.Value);
        }

        [Fact]
        public async Task GetCategoryById_Return_Category()
        {
            //Arrange
            var categoria = new Categorias
            {
                CategoriaId = 1,
                Nombre = "blah blah"
            };

            _mockCategoryService.Setup(c => c.GetCategoryById(categoria.CategoriaId)).ReturnsAsync(categoria);

            //Act
            var result = await _controller.GetCategoryById(categoria.CategoriaId);

            //Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal(categoria, okResult.Value);
        }

        [Fact]
        public async Task GetCategoryById_Should_Return_BadRequest()
        {
            //Act
            var result = await _controller.GetCategoryById(0);

            //Assert
            Assert.IsType<BadRequestObjectResult>(result.Result);
        }

        [Fact]
        public async Task DeleteCategory_Return_OkResult()
        {
            // Arrange
            var category = new Categorias
            {
                CategoriaId = 1,
                Nombre = "blah"
            };

            _mockCategoryService.Setup(c => c.DeleteAsync(1)).ReturnsAsync(true);

            // Act
            var result = await _controller.DeleteAsync(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var isDeleted = Assert.IsType<bool>(okResult.Value);
            Assert.True(isDeleted, "The category should have been successfully deleted.");
        }

        [Fact]
        public async Task DeleteCategory_Return_NotFoundRequest()
        {
            //Arrange
            var category = new Categorias
            {
                CategoriaId = 1,
                Nombre = "blah"
            };

            _mockCategoryService.Setup(c => c.DeleteAsync(1)).ThrowsAsync(new KeyNotFoundException());

            //Act
            var result = await _controller.DeleteAsync(1);

            //Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public async Task PostCategorias_Return_Category()
        {
            //Arrange
            var categoria = new Categorias
            {
                CategoriaId = 1,
                Nombre = "Eliezer"
            };

            _mockCategoryService.Setup(c => c.AddAsync(categoria)).ReturnsAsync(categoria);

            //Act
            var result = await _controller.PostCategorias(categoria);

            //Arrange
            var createAtAction = Assert.IsType<CreatedAtActionResult>(result.Result);
            Assert.Equal("GetCategoryById", createAtAction.ActionName);

        }

        [Fact]
        public async Task PostCategorias_Return_BadRequest()
        {
            //Arrange
            var categoria = new Categorias
            {
                CategoriaId = 1,
                Nombre = "Eliezer"
            };

            _mockCategoryService.Setup(c => c.AddAsync(null!)).ReturnsAsync((Categorias?)null);

            //Act
            var result = await _controller.PostCategorias(null!);

            //Assert
            Assert.IsType<BadRequestObjectResult>(result.Result);

        }

        [Fact]
        public async Task PutCategories_Returns_BadRequest()
        {
            var categoria = new Categorias
            {
                CategoriaId = 1,
                Nombre = "Eliezer"
            };

            //Act
            var result = await _controller.PutCategorias(2, categoria);

            //Assert
            Assert.IsType<BadRequestObjectResult>(result);

        }
     
        [Fact]
        public async Task PutCategories_Returns_NotContent()
        {
            var categoria = new Categorias
            {
                CategoriaId = 1,
                Nombre = "Eliezer"
            };

            _mockCategoryService.Setup(c => c.PutAsync(categoria)).ReturnsAsync(categoria);

            //Act
            var result = await _controller.PutCategorias(1, categoria);

            //Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task PutCategories_Return_ErrorServer()
        {
            //Arrange
            var categoria = new Categorias
            {
                CategoriaId = 1,
                Nombre = "Eliezer"
            };

            _mockCategoryService.Setup(c => c.PutAsync(categoria)).ThrowsAsync(new Exception());

            //Act
            var result = await _controller.PutCategorias(1, categoria);

            //Assert
            var assertResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, assertResult.StatusCode);
           
        }
    }
}