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
                .WithErrorCode("400")
                .WithMessage("\"name\" is required")
                .MinimumLength(5)
                .WithErrorCode("422")
                .WithMessage("\"name\" length must be at least 5 characters long");
        }
    }
}
