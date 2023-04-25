using Microsoft.EntityFrameworkCore;
using StoreManager.Data;

namespace StoreManager.Extensions
{
    public static class DataExtensions
    {
        public static IServiceCollection AddDataBase(this IServiceCollection services, IConfiguration config)
        {
            var connectionString = config.GetConnectionString("Default");

            services.AddDbContext<AppDbContext>(opt =>
            {
                opt.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
            });
            return services;
        }
    }
}
