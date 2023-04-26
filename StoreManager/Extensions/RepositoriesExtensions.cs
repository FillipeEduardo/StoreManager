using StoreManager.Abstractions.Repositories;
using StoreManager.Repositories;

namespace StoreManager.Extensions
{
    public static class RepositoriesExtensions
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ISaleRepository, SaleRepository>();
            services.AddScoped<ISaleProductRepository, SaleProductRepository>();
            return services;
        }
    }
}
