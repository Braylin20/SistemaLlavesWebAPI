using Microsoft.EntityFrameworkCore;
using Shared.Models;
using SistemaLlavesWebAPI.Dal;
using SistemaLlavesWebAPI.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

public class SalesServicesTests
{
    private Context GetInMemoryContext()
    {
        var options = new DbContextOptionsBuilder<Context>()
            .UseInMemoryDatabase(databaseName: "TestDb")
            .Options;

        return new Context(options);
    }

    [Fact]
    public async Task GetAsync_Should_Return_All_Sales()
    {
        // Arrange
        using var context = GetInMemoryContext();
        context.Ventas.AddRange(
            new Ventas { VentaId = 1, ClienteId = 1, MetodoPagoId = 1, Cantidad = 10, VentaDevuelta = false },
            new Ventas { VentaId = 2, ClienteId = 2, MetodoPagoId = 2, Cantidad = 5, VentaDevuelta = true }
        );
        await context.SaveChangesAsync();

        var service = new SalesServices(context);

        // Act
        var result = await service.GetAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count());
    }

    [Fact]
    public async Task AddAsync_Should_Add_New_Sale()
    {
        // Arrange
        using var context = GetInMemoryContext();
        var service = new SalesServices(context);

        var newSale = new Ventas
        {
            VentaId = 1,
            ClienteId = 1,
            MetodoPagoId = 1,
            Cantidad = 10,
            VentaDevuelta = false
        };

        // Act
        var result = await service.AddAsync(newSale);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(1, result.VentaId);
        Assert.Equal(1, context.Ventas.Count());
    }


    [Fact]
    public async Task DeleteAsync_Should_Remove_Sale()
    {
        // Arrange
        using var context = GetInMemoryContext();
        var sale = new Ventas
        {
            VentaId = 1,
            ClienteId = 1,
            MetodoPagoId = 1,
            Cantidad = 10,
            VentaDevuelta = false
        };

        context.Ventas.Add(sale);
        await context.SaveChangesAsync();

        var service = new SalesServices(context);

        // Act
        var result = await service.DeleteAsync(1);

        // Assert
        Assert.True(result);
        Assert.Empty(context.Ventas);
    }
}
