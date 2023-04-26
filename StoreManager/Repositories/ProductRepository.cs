using StoreManager.Abstractions.Repositories;
using StoreManager.Data;
using StoreManager.Models;

namespace StoreManager.Repositories
{
    public class ProductRepository : RepositoryBase<Product>, IProductRepository
    {
        public ProductRepository(AppDbContext context) : base(context)
        {
        }
    }
}
