namespace StoreManager.Models
{
    public class Sale
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public List<SaleProduct>? SalesProducts { get; set; }
    }
}
