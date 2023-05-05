using StoreManager.Abstractions.Repositories;
using StoreManager.Abstractions.Services;
using StoreManager.DTOs.InputModels;
using StoreManager.DTOs.ViewModels;
using StoreManager.Exceptions;
using StoreManager.Models;

namespace StoreManager.Services;

public class SaleService : ISaleService
{
    private readonly ISaleRepository _saleRepository;
    private readonly IProductService _productService;
    private readonly ISaleProductRepository _saleProductRepository;

    public SaleService(ISaleRepository saleRepository,
        IProductService productService,
        ISaleProductRepository saleProductRepository)
    {
        _saleRepository = saleRepository;
        _productService = productService;
        _saleProductRepository = saleProductRepository;
    }

    public async Task<SaleProductViewModel> CreateSale(List<SaleProductInputModel> model)
    {
        await ValidListProductInputModel(model);
        var sale = await _saleRepository.Create(new Sale());
        var salesProducts = mappingSalesProducts(sale.Id, model);
        sale.SalesProducts = salesProducts;
        await _saleRepository.Commit();
        return new SaleProductViewModel()
        {
            Id = sale.Id,
            ItemsSold = model,
        };
    }

    public async Task<List<SaleProduct>> GetAllSales()
    {
        var result = await _saleProductRepository.GetAllWithInclude("Sale");
        return result;
    }

    public async Task<List<SaleProduct>> GetSaleById(int id)
    {
        var result = await _saleProductRepository.GetByFuncWithInclude("Sale", x => x.SaleId == id);
        if (result.Count == 0) throw new DbNotFoundException("Sale not found");
        return result;
    }

    public async Task DeleteSale(int id)
    {
        var data = await _saleRepository.GetByFunc(x => x.Id == id);
        if (data is null) throw new DbNotFoundException("Sale not found");
        _saleRepository.Delete(data);
        await _saleRepository.Commit();
    }

    public async Task<SaleProductViewModel> UpdateSale(int id, IEnumerable<SaleProductInputModel> model)
    {
        var sales = await _saleRepository.GetByFuncWithInclude("SalesProducts", x => x.Id == id);
        if (sales.Count == 0) throw new DbNotFoundException("Sale not found");
        List<SaleProduct> salesProducts = mappingSalesProducts(id, model);
        sales[0].SalesProducts = salesProducts;
        _saleRepository.Update(sales[0]);
        await _saleRepository.Commit();
        var result = new SaleProductViewModel
        {
            Id = sales[0].Id,
            ItemsSold = model.ToList(),
        };
        return result;
    }

    private static List<SaleProduct> mappingSalesProducts(int id, IEnumerable<SaleProductInputModel> model)
    {
        return model.Select(x => new SaleProduct
        {
            ProductId = x.ProductId,
            Quantity = x.Quantity,
            SaleId = id
        }).ToList();
    }

    private async Task ValidListProductInputModel(List<SaleProductInputModel> model)
    {
        foreach (var item in model)
        {
            await _productService.GetProductById(item.ProductId);
        }
    }
}
