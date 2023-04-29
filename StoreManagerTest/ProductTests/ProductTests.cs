using AutoFixture;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using StoreManager.Abstractions.Repositories;
using StoreManager.Controllers;
using StoreManager.DTOs;
using StoreManager.DTOs.InputModels;
using StoreManager.DTOs.ViewModels;
using StoreManager.Exceptions;
using StoreManager.Models;
using StoreManager.Services;
using StoreManager.Validators;
using StoreManagerTest.Extensions;
using StoreManagerTest.Mocks;
using System.Linq.Expressions;

namespace StoreManagerTest.ProductTests;

public class ProductTests
{
    private readonly Mock<IProductRepository> _productRepository;
    private readonly IMapper _mapper;
    private readonly ProductService _productService;
    private readonly ProductValidator _productValidator = new ProductValidator();

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
        _productRepository
            .Setup(pr => pr.GetAll())
            .Returns(Task.FromResult(productsMock));

        var productController = new ProductController(_productService, _productValidator);

        var productResult = (OkObjectResult)await productController.Get();
        Assert.Equivalent(productResult.Value, productsMock);
    }

    [Fact]
    public async Task GetProductById_Executed_Success()
    {
        var product = new Fixture()
            .FixCircularReference()
            .Create<Product>();
        _productRepository
            .Setup(pr => pr.GetByFunc(It.IsAny<Expression<Func<Product, bool>>>()))
            .Returns(Task.FromResult(product));

        var productController = new ProductController(_productService, _productValidator);
        var productResult = (OkObjectResult)await productController.GetById(1);

        Assert.Equivalent(productResult.Value, product);
    }

    [Fact]
    public async Task GetProductById_ThrowException()
    {
        _productRepository
            .Setup(pr => pr.GetByFunc(It.IsAny<Expression<Func<Product, bool>>>()));
        var productController = new ProductController(_productService, _productValidator);

        var exception = await Assert.ThrowsAsync<DbNotFoundException>(async () =>
        {
            await productController.GetById(1);
        });

        Assert.Equal("Product not found", exception.Message);
    }

    [Fact]
    public async Task CreateProduct_Success()
    {
        var product = new Fixture()
            .FixCircularReference()
            .Create<Product>();
        _productRepository
            .Setup(pr => pr.Create(It.IsAny<Product>()))
            .Returns(Task.FromResult(product));
        _productRepository.Setup(pr => pr.Commit()).Verifiable();

        var productController = new ProductController(_productService, _productValidator);
        var result = (CreatedResult)await productController
            .CreateProduct(new ProductInputModel { Name = product.Name });
        var productResult = (ProductViewModel)result.Value;
        Assert.Equal(productResult.Name, product.Name);
    }

    [Fact]
    public async Task CreateProduct_WithoutName_Error400()
    {
        _productRepository
            .Setup(pr => pr.Create(It.IsAny<Product>()))
            .Verifiable();
        _productRepository
            .Setup(pr => pr.Commit())
            .Verifiable();
        var productController = new ProductController(_productService, _productValidator);
        var resultNoName = (ObjectResult)await productController.CreateProduct(new ProductInputModel { });
        Assert.Equal(400, resultNoName.StatusCode);
        var resultNamefour = (ObjectResult)await productController
            .CreateProduct(new ProductInputModel { Name = "asd" });
        Assert.Equal(422, resultNamefour.StatusCode);
    }

}
