using AutoMapper;
using StoreManager.Abstractions.Repositories;
using StoreManager.Abstractions.Services;
using StoreManager.DTOs.InputModels;
using StoreManager.DTOs.ViewModels;
using StoreManager.Exceptions;
using StoreManager.Models;

namespace StoreManager.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public ProductService(IProductRepository productRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }

    public async Task<List<ProductViewModel>> GetAllProducts()
    {
        var data = await _productRepository.GetAll();
        var result = _mapper.Map<List<Product>, List<ProductViewModel>>(data);
        return result;
    }

    public async Task<ProductViewModel> GetProductById(int id)
    {
        var data = await _productRepository.GetByFunc(x => x.Id == id);
        var result = _mapper.Map<ProductViewModel>(data);
        if (result is null) throw new DbNotFoundException("Product not found");
        return result;
    }

    public async Task<ProductViewModel> Createproduct(ProductInputModel model)
    {
        var product = _mapper.Map<Product>(model);
        await _productRepository.Create(product);
        await _productRepository.Commit();
        var result = _mapper.Map<ProductViewModel>(product);
        return result;
    }
}
