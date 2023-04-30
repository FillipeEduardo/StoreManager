using AutoFixture;
using AutoMapper;
using Moq;
using StoreManager.Abstractions.Repositories;
using StoreManager.DTOs;
using StoreManager.DTOs.InputModels;
using StoreManager.Exceptions;
using StoreManager.Models;
using StoreManager.Services;
using StoreManagerTest.Extensions;
using StoreManagerTest.Mocks;
using System.Linq.Expressions;

namespace StoreManagerTest.ProductTests;

public class ProductTests
{
    private readonly IMapper _mapper;
    private readonly Mock<IProductRepository> _productRepository;
    private readonly ProductService _productService;

    public ProductTests()
    {
        _mapper = new MapperConfiguration(cfg => cfg.AddProfile<StoreProfile>()).CreateMapper();
        _productRepository = new Mock<IProductRepository>();
        _productService = new ProductService(_productRepository.Object, _mapper);
    }

    [Fact]
    public async Task GetAllProducts_WithoutParameters_Success()
    {

        var products = ProductMock.ReturnListProducts();
        _productRepository.Setup(pr => pr.GetAll()).Returns(Task.FromResult(products));

        var result = await _productService.GetAllProducts();

        Assert.Equivalent(products.Select(x => x.Name), result.Select(x => x.Name));
        Assert.Equivalent(products.Select(x => x.Id), result.Select(x => x.Id));

    }

    [Fact]

    public async Task GetProductById_WithValidId_Success()
    {
        var productMock = new Fixture().FixCircularReference().Create<Product>();
        _productRepository
            .Setup(pr => pr.GetByFunc(It.IsAny<Expression<Func<Product, bool>>>()))
            .Returns(Task.FromResult(productMock));
        var result = await _productService.GetProductById(1);

        Assert.Equal(result.Id, productMock.Id);
        Assert.Equal(result.Name, productMock.Name);
    }

    [Fact]
    public async Task GetProductById_ReturnNullFromRepository_ThrowException()
    {
        _productRepository
            .Setup(pr => pr.GetByFunc(It.IsAny<Expression<Func<Product, bool>>>()))
            .Verifiable();
        var result = await Assert.ThrowsAsync<DbNotFoundException>(async () =>
        {
            await _productService.GetProductById(1);
        });
        Assert.Equal("Product not found", result.Message);
    }

    [Fact]
    public async Task CreateProduct_ObjectInsertCorrectly_Success()
    {
        _productRepository
            .Setup(pr => pr.Create(It.IsAny<Product>()))
            .Verifiable();
        _productRepository
            .Setup(pr => pr.Commit())
            .Verifiable();
        var product = new Fixture().Create<ProductInputModel>();

        var result = await _productService.Createproduct(product);

        _productRepository
            .Verify(pr => pr.Create(It.IsAny<Product>()), Times.Once);
        _productRepository
            .Verify(pr => pr.Commit(), Times.Once);
        Assert.Equal(product.Name, result.Name);
    }
}
