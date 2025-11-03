using Microsoft.AspNetCore.Mvc;
using TechAndSolve.APPWeb.Products.Application.Products.Requests;
using TechAndSolve.APPWeb.Products.Application.Products.Services;

namespace TechAndSolve.APPWeb.Products.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController(
    IProductsService productsService
    ) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAllProductsAsync()
    {
        var products = await productsService.GetAllAsync();

        return Ok(products);
    }

    [HttpGet("{productId:int}")]
    public async Task<IActionResult> GetProductByIdAsync([FromRoute] int productId)
    {
        var product = await productsService.GetByIdAsync(productId);

        return Ok(product);
    }

    [HttpPost]
    public async Task<IActionResult> CreateProductAsync([FromBody] ProductRegisterRequest productRegisterRequest)
    {
        var productId =  await productsService.CreateAsync(productRegisterRequest);

        return Ok(productId);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateProductAsync([FromBody] ProductUpdateRequest productUpdateRequest)
    {
        await productsService.UpdateAsync(productUpdateRequest);

        return NoContent();
    }

    [HttpDelete("{productId:int}")]
    public async Task<IActionResult> DeleteProductAsync([FromRoute] int productId)
    {
        await productsService.DeleteAsync(productId);

        return NoContent();
    }
}