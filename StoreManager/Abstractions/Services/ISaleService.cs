using StoreManager.DTOs.InputModels;
using StoreManager.DTOs.ViewModels;

namespace StoreManager.Abstractions.Services
{
    public interface ISaleService
    {
        Task<SaleProductViewModel> CreateSale(List<SaleProductInputModel> model);
    }
}