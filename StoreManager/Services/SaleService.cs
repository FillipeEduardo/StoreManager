using StoreManager.Abstractions.Repositories;
using StoreManager.Abstractions.Services;
using StoreManager.DTOs.InputModels;
using StoreManager.DTOs.ViewModels;
using StoreManager.Models;

namespace StoreManager.Services;

public class SaleService : ISaleService
{
    private readonly ISaleRepository _saleRepository;
    private readonly IProductService _productService;

    public SaleService(ISaleRepository saleRepository,
        IProductService productService)
    {
        _saleRepository = saleRepository;
        _productService = productService;
    }

    public async Task<SaleProductViewModel> CreateSale(List<SaleProductInputModel> model)
    {
        await ValidListProductInputModel(model);
        var sale = await _saleRepository.Create(new Sale());
        var salesProducts = model.Select(x => new SaleProduct
        {
            ProductId = x.ProductId,
            Quantity = x.Quantity,
            SaleId = sale.Id
        }).ToList();
        sale.SalesProducts = salesProducts;
        await _saleRepository.Commit();
        return new SaleProductViewModel()
        {
            Id = sale.Id,
            ItemsSold = model,
        };
    }

    private async Task ValidListProductInputModel(List<SaleProductInputModel> model)
    {
        foreach (var item in model)
        {
            await _productService.GetProductById(item.ProductId);
        }
    }
}
