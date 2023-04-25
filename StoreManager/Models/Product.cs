namespace StoreManager.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public List<SaleProduct>? SalesProducts { get; set; }
    }
}
