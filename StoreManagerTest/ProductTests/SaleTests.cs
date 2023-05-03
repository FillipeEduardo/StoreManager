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
    private readonly Mock<ISaleRepository> _saleRepository;
    private readonly Mock<IProductService> _productService;
    private readonly Mock<ISaleProductRepository> _saleProductRepository;
    private readonly SaleService _saleService;

    public SaleTests()
    {
        _saleRepository = new Mock<ISaleRepository>();
        _productService = new Mock<IProductService>();
        _saleProductRepository = new Mock<ISaleProductRepository>();
        _saleService = new SaleService(_saleRepository.Object, _productService.Object, _saleProductRepository.Object);
    }

    [Fact]
    public async Task CreateSale_Success()
    {
        var sale = new Fixture().FixCircularReference().Create<Sale>();
        _saleRepository.Setup(sr => sr.Create(It.IsAny<Sale>())).Returns(Task.FromResult(sale));
        var saleProductInputModel = sale.SalesProducts.Select(x => new SaleProductInputModel
        {
            ProductId = x.ProductId,
            Quantity = x.Quantity,
        }).ToList();
        var result = await _saleService.CreateSale(saleProductInputModel);

        _saleRepository.Verify(sr => sr.Create(It.IsAny<Sale>()), Times.Once);
        _saleRepository.Verify(sr => sr.Commit(), Times.Once);
        Assert.Equal(result.Id, sale.Id);
        Assert.Equal(result.ItemsSold.Count, sale.SalesProducts.Count);
        Assert.Equal(result.ItemsSold[0].ProductId, sale.SalesProducts[0].ProductId);
        Assert.Equal(result.ItemsSold[0].Quantity, sale.SalesProducts[0].Quantity);

    }

    public async Task CreateSale_ProductNotFound_ThrowException()
    {
        var sales = SaleMock.GetSaleProducts();
        _productService.Setup(ps => ps.GetProductById(It.IsAny<int>())).Throws(new DbNotFoundException("Product not found"));

        var result = await Assert.ThrowsAsync<DbNotFoundException>(async () =>
        {
            await _saleService.CreateSale(sales);
        });

        Assert.Equal("Product not found", result.Message);

    }
}
