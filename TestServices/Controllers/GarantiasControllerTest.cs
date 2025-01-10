using Microsoft.AspNetCore.Mvc;
using Moq;
using Shared.Models;
using SistemaLlavesWebAPI.Controllers;
using SistemaLlavesWebAPI.Abstractions;

namespace TestServices.Controller
{

    public class GarantiasControllerTest
    {
        private readonly Mock<IWarrantyService> _mockWarrantyService;
        private readonly GarantiasController _controller;

        public GarantiasControllerTest()
        {
            _mockWarrantyService = new Mock<IWarrantyService>();
            _controller = new GarantiasController(_mockWarrantyService.Object);
        }

        [Fact]  
        public async Task GetGarantias_ReturnsListOfGarantias()
        {
            //Arrange
            var garantiasList = new List<Garantias>
            {
                new Garantias {GarantiaId= 1, Descripcion = "Garantia 1" },
                new Garantias {GarantiaId= 2, Descripcion = "Garantia 2" }
            };
            _mockWarrantyService.Setup(s => s.GetAsync()).ReturnsAsync(garantiasList);

            //Act
            var result = await _controller.GetGarantias();

            //Assert
            var okResult = Assert.IsType<ActionResult<IEnumerable<Garantias>>>(result);
            Assert.Equal(garantiasList, okResult.Value);
        }

        [Fact]
        public async Task GetById_ReturnsGarantia()
        {
            //Arrange
            var garantia = new Garantias
            {
                GarantiaId = 1,
                Descripcion = "Garantia 2"

            };
            _mockWarrantyService.Setup(s => s.GetById(garantia.GarantiaId)).ReturnsAsync(garantia);

            //Act
            var result = await _controller.GetGarantias(garantia.GarantiaId);

            //Assert
            var okResult = Assert.IsType<ActionResult<Garantias>>(result);
            Assert.Equal(garantia, okResult.Value);
      
        }

        [Fact]
        public async Task GetGarantias_ById_ReturnsNotFound_WhenGarantiaDoesNotExist()
        {
            // Arrange
            _mockWarrantyService.Setup(s => s.GetById(1)).ReturnsAsync((Garantias?)null);

            // Act
            var result = await _controller.GetGarantias(1);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }


        [Fact]
        public async Task PostGarantias_ReturnsCreateResult_WhenSussesful()
        {
            //Arrange
            var garantia = new Garantias
            {
                GarantiaId = 1,
                Descripcion = "Garantia 2"

            };

            _mockWarrantyService.Setup(s => s.AddAsync(garantia)).ReturnsAsync(true);

            //Act
            var result = await _controller.PostGarantias(garantia);

            //Assert
            var createResult =  Assert.IsType<CreatedAtActionResult>(result.Result);
            Assert.Equal("GetGarantias", createResult.ActionName);

        }

        [Fact]
        public async Task PostGarantias_ReturnsCreateResult_WhenFailed()
        {
            //Arrange
            var garantia = new Garantias
            {
                GarantiaId = 1,
                Descripcion = "Garantia 2"

            };

            _mockWarrantyService.Setup(s => s.AddAsync(garantia)).ReturnsAsync(false);

            //Act
            var result = await _controller.PostGarantias(garantia);

            //Assert
            Assert.IsType<BadRequestResult>(result.Result);
        }

        [Fact]
        public async Task PutGarantias_ReturnsOkResult_WhenSuccessful()
        {
            //Arrange
            var garantia = new Garantias
            {
                GarantiaId = 1,
                Descripcion = "Garantia 2"

            };

            _mockWarrantyService.Setup(s => s.PutAsync(garantia)).ReturnsAsync(garantia);

            //Act
            var result = await _controller.PutGarantias(1,garantia);

            //Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(garantia, okResult.Value);
        }

        [Fact]
        public async Task PutGarantias_ReturnsBadRequest_WhenFaile()
        {
            //Arrange
            var garantia = new Garantias
            {
                GarantiaId = 1,
                Descripcion = "Garantia 2"

            };
            _mockWarrantyService.Setup(s => s.PutAsync(garantia)).ReturnsAsync(garantia);

            //Act
            var result = await _controller.PutGarantias(2, garantia);

            //Arrange
            Assert.IsType<BadRequestResult>(result);

        }

        [Fact]

        public async Task DeleteGarantias_ReturnsNoContentResult_WhenSuccessful()
        {
            //Arrange
            var garantia = new Garantias
            {
                GarantiaId = 1,
                Descripcion = "Garantia 2"

            };

            _mockWarrantyService.Setup(s => s.DeleteAsync(1)).ReturnsAsync(garantia);

            //Act
            var result = await _controller.DeleteGarantias(1);

            //Assert
             Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task DeleteGarantias_ReturnsNoFound_WhenWarrantyNull() 
        {
            //Arrenge
            _mockWarrantyService.Setup(s => s.DeleteAsync(1)).ReturnsAsync((Garantias?)null);
            //Act
            var result = await _controller.DeleteGarantias(1);

            //Assert
            Assert.IsType<NotFoundResult>(result);
        }




    }
}
