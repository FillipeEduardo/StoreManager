using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using StoreManager.Abstractions.Repositories;
using StoreManager.Controllers;
using StoreManager.DTOs;
using StoreManager.Services;
using StoreManagerTest.Mocks;

namespace StoreManagerTest.ProductTests;

public class ProductTests
{
    private readonly Mock<IProductRepository> _productRepository;
    private readonly IMapper _mapper;

    public ProductTests()
    {
        _productRepository = new Mock<IProductRepository>();
        _mapper = new MapperConfiguration(mm =>
        {
            mm.AddProfile(new StoreProfile());
        }).CreateMapper();
    }

    [Fact]
    public async Task GetAllProducts_Executed_Success()
    {
        var productsMock = ProductMock.ReturnListProducts();
        _productRepository.Setup(pr => pr.GetAll()).Returns(Task.FromResult(productsMock));

        var productService = new ProductService(_productRepository.Object, _mapper);
        var productController = new ProductController(productService);

        var productResult = (OkObjectResult)await productController.Get();
        Assert.Equivalent(productResult.Value, productsMock);
    }

}
