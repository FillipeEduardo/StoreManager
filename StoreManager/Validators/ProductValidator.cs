using FluentValidation;
using StoreManager.DTOs.InputModels;

namespace StoreManager.Validators
{
    public class ProductValidator : AbstractValidator<ProductInputModel>
    {
        public ProductValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MinimumLength(5);
        }
    }
}
