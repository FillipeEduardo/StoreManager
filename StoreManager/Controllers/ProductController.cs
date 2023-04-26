using Microsoft.AspNetCore.Mvc;
using StoreManager.Abstractions.Services;

namespace StoreManager.Controllers;

[ApiController]
[Route("products")]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductController(IProductService productService)
    {
        _productService = productService;
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
}
