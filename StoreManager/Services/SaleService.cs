using StoreManager.Abstractions.Repositories;
using StoreManager.Abstractions.Services;
using StoreManager.DTOs.InputModels;
using StoreManager.DTOs.ViewModels;
using StoreManager.Models;

namespace StoreManager.Services;

public class SaleService : ISaleService
{
    private readonly ISaleRepository _saleRepository;
    private readonly ISaleProductRepository _saleProductRepository;

    public SaleService(ISaleRepository saleRepository, ISaleProductRepository saleProductRepository)
    {
        _saleRepository = saleRepository;
        _saleProductRepository = saleProductRepository;
    }

    public async Task<SaleProductViewModel> CreateSale(List<SaleProductInputModel> model)
    {
        var sale = await _saleRepository.Create(new Sale());
        await _saleRepository.Commit();
        foreach (var item in model)
        {
            var saleProduct = new SaleProduct
            {
                ProductId = item.ProductId,
                SaleId = sale.Id,
                Quantity = item.Quantity,
            };
            await _saleProductRepository.Create(saleProduct);
        }
        await _saleProductRepository.Commit();
        var result = new SaleProductViewModel()
        {
            Id = sale.Id,
            ItemsSold = model,
        };
        return result;
    }
}
