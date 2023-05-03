using AutoFixture;
using StoreManager.DTOs.InputModels;
using StoreManagerTest.Extensions;

namespace StoreManagerTest.Mocks;

public static class SaleMock
{
    public static List<SaleProductInputModel> GetSaleProducts()
    {
        var products = new List<SaleProductInputModel>();
        var sale1 = new Fixture().FixCircularReference().Create<SaleProductInputModel>();
        var sale2 = new Fixture().FixCircularReference().Create<SaleProductInputModel>();
        products.Add(sale1);
        products.Add(sale2);
        return products;
    }
}
