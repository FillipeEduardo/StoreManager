using AutoFixture;
using Moq;
using StoreManager.Abstractions.Repositories;
using StoreManager.Abstractions.Services;
using StoreManager.DTOs.InputModels;
using StoreManager.Exceptions;
using StoreManager.Models;
using StoreManager.Services;
using StoreManagerTest.Extensions;
using StoreManagerTest.Mocks;

namespace StoreManagerTest.ProductTests;

public class SaleTests
{
    [Fact]
    public async Task CreateSale_Success()
    {
        var saleRepository = new Mock<ISaleRepository>();
        var productService = new Mock<IProductService>();
        var saleService = new SaleService(saleRepository.Object, productService.Object);

        var sale = new Fixture().FixCircularReference().Create<Sale>();
        saleRepository.Setup(sr => sr.Create(It.IsAny<Sale>())).Returns(Task.FromResult(sale));
        var saleProductInputModel = sale.SalesProducts.Select(x => new SaleProductInputModel
        {
            ProductId = x.ProductId,
            Quantity = x.Quantity,
        }).ToList();
        var result = await saleService.CreateSale(saleProductInputModel);

        saleRepository.Verify(sr => sr.Create(It.IsAny<Sale>()), Times.Once);
        saleRepository.Verify(sr => sr.Commit(), Times.Once);
        Assert.Equal(result.Id, sale.Id);
        Assert.Equal(result.ItemsSold.Count, sale.SalesProducts.Count);
        Assert.Equal(result.ItemsSold[0].ProductId, sale.SalesProducts[0].ProductId);
        Assert.Equal(result.ItemsSold[0].Quantity, sale.SalesProducts[0].Quantity);

    }

    public async Task CreateSale_ProductNotFound_ThrowException()
    {
        var saleRepository = new Mock<ISaleRepository>();
        var productService = new Mock<IProductService>();
        var saleService = new SaleService(saleRepository.Object, productService.Object);
        var sales = SaleMock.GetSaleProducts();
        productService.Setup(ps => ps.GetProductById(It.IsAny<int>())).Throws(new DbNotFoundException("Product not found"));

        var result = await Assert.ThrowsAsync<DbNotFoundException>(async () =>
        {
            await saleService.CreateSale(sales);
        });

        Assert.Equal("Product not found", result.Message);

    }
}
