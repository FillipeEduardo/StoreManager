using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using StoreManager.Abstractions.Services;
using StoreManager.DTOs.InputModels;
using StoreManager.DTOs.ViewModels;

namespace StoreManager.Controllers;

[ApiController]
[Route("/sales")]
public class SaleController : ControllerBase
{
    private readonly ISaleService _saleService;
    private readonly IValidator<SaleProductInputModel> _validator;


    public SaleController(ISaleService saleService, IValidator<SaleProductInputModel> validator)
    {
        _saleService = saleService;
        _validator = validator;
    }

    [HttpPost]
    public async Task<IActionResult> CreateSale(List<SaleProductInputModel> model)
    {
        foreach (var item in model)
        {
            var validatorResult = await _validator.ValidateAsync(item);
            if (!validatorResult.IsValid)
            {
                return StatusCode(
                    int.Parse(validatorResult.Errors[0].ErrorCode),
                    new ErrorViewModel(validatorResult.Errors[0].ErrorMessage));
            }
        }
        var result = await _saleService.CreateSale(model);
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllSales()
    {
        var data = await _saleService.GetAllSales();
        var result = data.Select(x => new SaleViewModel
        {
            Date = x.Sale.Date,
            ProductId = x.ProductId,
            Quantity = x.Quantity,
            SaleId = x.Sale.Id,
        });
        return Ok(result);
    }
}
