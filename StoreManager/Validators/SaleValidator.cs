using FluentValidation;
using StoreManager.DTOs.InputModels;

namespace StoreManager.Validators
{
    public class SaleValidator : AbstractValidator<SaleProductInputModel>
    {
        public SaleValidator()
        {
            RuleFor(x => x.ProductId)
                .NotEmpty()
                .WithErrorCode("400")
                .WithMessage("\"productId\" is required");
            RuleFor(x => x.Quantity)
                .NotEmpty()
                .WithErrorCode("400")
                .WithMessage("\"quantity\" is required")
                .GreaterThanOrEqualTo(1)
                .WithErrorCode("422")
                .WithMessage("\"quantity\" must be greater than or equal to 1");
        }
    }
}
