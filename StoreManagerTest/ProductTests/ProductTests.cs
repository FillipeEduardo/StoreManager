using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using StoreManager.Abstractions.Repositories;
using StoreManager.Controllers;
using StoreManager.DTOs;
using StoreManager.Exceptions;
using StoreManager.Models;
using StoreManager.Services;
using StoreManagerTest.Mocks;
using System.Linq.Expressions;

namespace StoreManagerTest.ProductTests;

public class ProductTests
{
    private readonly Mock<IProductRepository> _productRepository;
    private readonly IMapper _mapper;
    private readonly ProductService _productService;

    public ProductTests()
    {
        _productRepository = new Mock<IProductRepository>();
        _mapper = new MapperConfiguration(mm =>
        {
            mm.AddProfile(new StoreProfile());
        }).CreateMapper();
        _productService = new ProductService(_productRepository.Object, _mapper);
    }

    [Fact]
    public async Task GetAllProducts_Executed_Success()
    {
        var productsMock = ProductMock.ReturnListProducts();
        _productRepository.Setup(pr => pr.GetAll()).Returns(Task.FromResult(productsMock));

        var productController = new ProductController(_productService);

        var productResult = (OkObjectResult)await productController.Get();
        Assert.Equivalent(productResult.Value, productsMock);
    }

    [Fact]
    public async Task GetProductById_Executed_Success()
    {
        var product = ProductMock.GetProduct();
        _productRepository
            .Setup(pr => pr.GetByFunc(It.IsAny<Expression<Func<Product, bool>>>()))
            .Returns(Task.FromResult(product));

        var productController = new ProductController(_productService);
        var productResult = (OkObjectResult)await productController.GetById(1);

        Assert.Equivalent(productResult.Value, product);
    }

    [Fact]
    public async Task GetProductById_ThrowException()
    {
        _productRepository
            .Setup(pr => pr.GetByFunc(It.IsAny<Expression<Func<Product, bool>>>()));
        var productController = new ProductController(_productService);

        var exception = await Assert.ThrowsAsync<DbNotFoundException>(async () =>
        {
            await productController.GetById(1);
        });

        Assert.Equal("Product not found", exception.Message);
    }

}
