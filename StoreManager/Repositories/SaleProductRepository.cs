using StoreManager.Abstractions.Repositories;
using StoreManager.Data;
using StoreManager.Models;

namespace StoreManager.Repositories
{
    public class SaleProductRepository : RepositoryBase<SaleProduct>, ISaleProductRepository
    {
        public SaleProductRepository(AppDbContext context) : base(context)
        {
        }
    }
}
