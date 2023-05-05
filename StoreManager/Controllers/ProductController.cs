using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using StoreManager.Abstractions.Services;
using StoreManager.DTOs.InputModels;
using StoreManager.DTOs.ViewModels;

namespace StoreManager.Controllers;

[ApiController]
[Route("products")]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;
    private readonly IValidator<ProductInputModel> _validator;

    public ProductController(IProductService productService, IValidator<ProductInputModel> validator)
    {
        _productService = productService;
        _validator = validator;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        return Ok(await _productService.GetAllProducts());
    }
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        return Ok(await _productService.GetProductById(id));
    }

    [HttpPost]
    public async Task<IActionResult> CreateProduct(ProductInputModel model)
    {
        var validation = await _validator.ValidateAsync(model);
        if (!validation.IsValid)
        {
            return StatusCode(int.Parse(validation.Errors[0].ErrorCode),
                new ErrorViewModel(validation.Errors[0].ErrorMessage));
        }
        var result = await _productService.Createproduct(model);
        return Created($"products/{result.Id}", result);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateProduct(int id, ProductInputModel model)
    {
        var validation = await _validator.ValidateAsync(model);
        if (!validation.IsValid)
        {
            return StatusCode(int.Parse(validation.Errors[0].ErrorCode),
                new ErrorViewModel(validation.Errors[0].ErrorMessage));
        }
        var result = await _productService.UpdateProduct(id, model);
        return Ok(result);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteProduct(int id)
    {
        await _productService.DeleteProduct(id);
        return NoContent();
    }
}
