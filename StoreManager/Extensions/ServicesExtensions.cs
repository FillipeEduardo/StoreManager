using StoreManager.Abstractions.Services;
using StoreManager.Services;

namespace StoreManager.Extensions
{
    public static class ServicesExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ISaleService, SaleService>();
            return services;
        }
    }
}
