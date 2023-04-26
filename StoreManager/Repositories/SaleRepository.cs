using StoreManager.Abstractions.Repositories;
using StoreManager.Data;
using StoreManager.Models;

namespace StoreManager.Repositories
{
    public class SaleRepository : RepositoryBase<Sale>, ISaleRepository
    {
        public SaleRepository(AppDbContext context) : base(context)
        {
        }
    }
}
