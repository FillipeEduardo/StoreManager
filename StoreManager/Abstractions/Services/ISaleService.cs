using StoreManager.DTOs.InputModels;
using StoreManager.DTOs.ViewModels;
using StoreManager.Models;

namespace StoreManager.Abstractions.Services
{
    public interface ISaleService
    {
        Task<SaleProductViewModel> CreateSale(List<SaleProductInputModel> model);
        Task<List<SaleProduct>> GetAllSales();
        Task<List<SaleProduct>> GetSaleById(int id);
    }
}