using FluentValidation;
using StoreManager.DTOs.InputModels;
using StoreManager.Validators;

namespace StoreManager.Extensions
{
    public static class ValidatorsExtensions
    {
        public static IServiceCollection AddValidators(this IServiceCollection services)
        {
            services.AddScoped<IValidator<ProductInputModel>, ProductValidator>();
            services.AddScoped<IValidator<SaleProductInputModel>, SaleValidator>();
            return services;
        }
    }
}
