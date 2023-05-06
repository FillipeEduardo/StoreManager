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
        if (data is null) throw new DbNotFoundException("Product not found");
        return _mapper.Map<ProductViewModel>(data);

    }

    public async Task<ProductViewModel> Createproduct(ProductInputModel model)
    {
        var product = _mapper.Map<Product>(model);
        await _productRepository.Create(product);
        await _productRepository.Commit();
        var result = _mapper.Map<ProductViewModel>(product);
        return result;
    }

    public async Task<ProductViewModel> UpdateProduct(int id, ProductInputModel model)
    {
        var data = await _productRepository.GetByFunc(x => x.Id == id);
        if (data is null) throw new DbNotFoundException("Product not found");
        data.Name = model.Name;
        _productRepository.Update(data);
        await _productRepository.Commit();
        var result = _mapper.Map<ProductViewModel>(data);
        return result;
    }

    public async Task DeleteProduct(int id)
    {
        var data = await _productRepository.GetByFunc(x => x.Id == id);
        if (data is null) throw new DbNotFoundException("Product not found");
        _productRepository.Delete(data);
        await _productRepository.Commit();
    }

    public async Task<List<ProductViewModel>> SearchByTerm(string term)
    {
        var data = await _productRepository.GetListByFunc(x => x.Name.Contains(term));
        return _mapper.Map<List<ProductViewModel>>(data);
    }
}
