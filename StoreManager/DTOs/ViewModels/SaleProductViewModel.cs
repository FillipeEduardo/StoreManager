using StoreManager.DTOs.InputModels;

namespace StoreManager.DTOs.ViewModels
{
    public class SaleProductViewModel
    {
        public int Id { get; set; }
        public List<SaleProductInputModel>? ItemsSold { get; set; }
    }
}
