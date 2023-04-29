using AutoFixture;
using StoreManager.Models;
using StoreManagerTest.Extensions;

namespace StoreManagerTest.Mocks;

public static class ProductMock
{
    public static List<Product> ReturnListProducts()
    {
        var products = new List<Product>();
        for (int i = 0; i < 10; i++)
        {
            products.Add(new Fixture()
                .FixCircularReference()
                .Create<Product>());
        }
        return products;
    }
}
