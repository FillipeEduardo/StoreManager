using StoreManager.Models;

namespace StoreManagerTest.Mocks;

public static class ProductMock
{
    public static List<Product> ReturnListProducts()
    {
        var products = new List<Product>();
        products.Add(new Product { Id = 1, Name = "Martelo de Thor" });
        products.Add(new Product { Id = 2, Name = "Traje de encolhimento" });
        products.Add(new Product { Id = 3, Name = "'Escudo do Capitão América'" });
        return products;
    }

    public static Product GetProduct()
    {
        return new Product { Id = 1, Name = "Martelo de Thor" };
    }
}
